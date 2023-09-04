using ConexaoComApiC.Bases;
using ConexaoComApiC.Model;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConexaoComApiC.ProdutoTeste
{
    class ProdutoService
    {
        public List<ProdutoModel> ProdutoBuscaIds()
        {
            List<ProdutoModel> Produto = null;
            ProdutoModel ProdutoModel = new ProdutoModel();
            string status = "";
            string requisicao = string.Empty;
            string url = FuncoesBase.Autenticacao;


            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);


            int i = 10;
            while (i >= 0)
            {
                var response = client.Execute(request);
                status = response.StatusCode.ToString();

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorMessage = response.Content;
                    Console.WriteLine($"Erro na solicitação: credito  8 : {status}");
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    JsonDeserializer deserial = new JsonDeserializer();
                    Produto = deserial.Deserialize<List<ProdutoModel>>(response);

                    var errorMessage = response.Content;
                    Console.WriteLine($"Cancelamento do pagamento no mesmo dia - credito Status: 8 : {status}");
                    break;
                }

                else
                {
                    Console.WriteLine($"Status de resposta inesperado: credito  8:  {status}");
                }


                Thread.Sleep(3000);
                i--;
            }
            return Produto;
        }
    }
}
