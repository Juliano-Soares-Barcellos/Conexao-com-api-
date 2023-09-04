using ConexaoComApiC.Bases;
using ConexaoComApiC.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConexaoComApiC.Get
{
    class Tabela
    {
        public List<ObjetoModel> PegaTabela()
        {
            List<ObjetoModel> dadosTabela = new List<ObjetoModel>();
            string status = "";
            try
            {
                string url = FuncoesBase.UrlTabela;
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);

                int i = 5;
                while (i >= 0)
                {
                    var response = client.Execute(request);
                    status = response.StatusCode.ToString();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var content = response.Content;
                        var jsonArray = JsonConvert.DeserializeObject<JArray>(content);
                        Dictionary<int, List<DateTime>> datasPorProduto = new Dictionary<int, List<DateTime>>();
                        foreach (JArray item in jsonArray)
                        {
                            int id = (int)item[0];
                            string datass = item[3].ToString();
                            string datas = datass.ToString();
                            string[] datasArray = datas.Split(',');


                            List<DateTime> datasConserto = new List<DateTime>();
                            foreach (string data in datasArray)
                            {
                                DateTime dataConserto;
                                if (DateTime.TryParse(data.Trim(), out dataConserto))
                                {
                                    datasConserto.Add(dataConserto);
                                }
                            }

                            datasPorProduto[id] = datasConserto;
                        }


                        int maxDatasPorProduto = datasPorProduto.Values.Max(d => d.Count);

                        foreach (var item in jsonArray)
                        {
                            int id = (int)item[0];
                            string nome = item[1].ToString();
                            string numero = item[2].ToString();

                            List<DateTime> datasConserto = datasPorProduto[id];
                            datasConserto.Sort();


                            ObjetoModel objetoModel = new ObjetoModel
                            {
                                Id = id,
                                Nome = nome,
                                Numero = numero,
                                Datas = datasConserto
                            };

                            dadosTabela.Add(objetoModel);
                        }

                        Console.WriteLine($"Testando a tabela: {status},{response.StatusCode}");

                        break;
                    }

                    else
                    {
                        var errorMessage = response.Content;
                        Console.WriteLine($"Erro na solicitação da tabela : {status}");
                    }

                    Thread.Sleep(3000);
                    i--;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }

            return dadosTabela;
        }
    }
}
