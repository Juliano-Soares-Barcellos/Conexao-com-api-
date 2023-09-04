using ConexaoComApiC.Get;
using ConexaoComApiC.Model;
using ConexaoComApiC.POST;
using ConexaoComApiC.ProdutoTeste;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoComApiC
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabela tabela = new Tabela();
            ProdutoPost ProdutoPos = new ProdutoPost();
            ProdutoModel ProdutoModel = new ProdutoModel();
            ProdutoService ProdutoServic = new ProdutoService();
            List<ProdutoModel> listaProduto = ProdutoServic.ProdutoBuscaIds();
            ProdutoPos.Insercao("pqpe", "12012", 80);
            List<ObjetoModel> dados = tabela.PegaTabela();





            DataTable pivotTable = new DataTable();
            try
            {
                // Adiciona as colunas iniciais (Nome, Numero, quantidade_conserto, Garantia, Dias de Garantia)
                pivotTable.Columns.Add("Nome", typeof(string));
                pivotTable.Columns.Add("Numero", typeof(string));
                pivotTable.Columns.Add("quantidade_conserto", typeof(int));
                pivotTable.Columns.Add("Garantia", typeof(string));
                pivotTable.Columns.Add("Dias de Garantia", typeof(string));

                // Cria um dicionário para armazenar as datas de conserto de cada produto
                Dictionary<int, List<DateTime>> datasPorProduto = new Dictionary<int, List<DateTime>>();

                // Percorre os dados para agrupar as datas de conserto de cada produto
                foreach (ObjetoModel objeto in dados)
                {
                    int id = objeto.Id;
                    string nome = objeto.Nome;
                    string numero = objeto.Numero;
                    List<DateTime> datasConserto = objeto.Datas;

                    // Adiciona as datas de conserto do produto ao dicionário
                    datasPorProduto[id] = datasConserto;
                }

                // Encontra o maior número de datas de conserto entre os produtos
                int maxDatasPorProduto = datasPorProduto.Values.Max(d => d.Count);

                // Adiciona as colunas de datas de conserto para cada produto
                for (int i = 1; i <= maxDatasPorProduto; i++)
                {
                    DataColumn dataColumn = new DataColumn($"DataConserto{i}", typeof(DateTime));
                    dataColumn.DateTimeMode = DataSetDateTime.Unspecified;

                    pivotTable.Columns.Add(dataColumn);
                }

                // Pivotar os dados e preencher as colunas de datas de conserto
                foreach (ObjetoModel objeto in dados)
                {
                    int id = objeto.Id;
                    string nome = objeto.Nome;
                    string numero = objeto.Numero;
                    int quantidadeConserto = objeto.Datas.Count; // Use a contagem de datas diretamente

                    DataRow newRow = pivotTable.NewRow();
                    newRow["Nome"] = nome;
                    newRow["Numero"] = numero;
                    newRow["quantidade_conserto"] = quantidadeConserto;

                    // Preenche as colunas de datas de conserto
                    List<DateTime> datasConserto = datasPorProduto[id];
                    datasConserto.Sort(); // Ordena as datas em ordem crescente

                    for (int i = 0; i < datasConserto.Count; i++)
                    {
                        newRow[$"DataConserto{i + 1}"] = datasConserto[i].ToString("dd/MM/yyyy"); // Formatando a data no formato desejado
                    }
                    DateTime ultimaDataConserto = datasConserto.Count > 0 ? datasConserto.Last() : DateTime.MinValue;

                    pivotTable.Rows.Add(newRow);
                }
                foreach (DataRow row in pivotTable.Rows)
                {
                    Console.WriteLine($"Nome: {row["Nome"]}, Numero: {row["Numero"]}, Quantidade de Conserto: {row["quantidade_conserto"]}, Garantia: {row["Garantia"]}, Dias de Garantia: {row["Dias de Garantia"]}");
                    for (int i = 1; i <= maxDatasPorProduto; i++)
                    {
                        Console.WriteLine($"DataConserto{i}: {row[$"DataConserto{i}"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }


        }
    }
}
