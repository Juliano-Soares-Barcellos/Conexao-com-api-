using ConexaoComApiC.Bases;
using ConexaoComApiC.Model;
using RestSharp;
using System;
using System.Net;
using System.Threading;

namespace ConexaoComApiC.POST
{
    internal class ProdutoPost
    {
        public void Insercao(String nome, String numero, int quantidade_conserto)
        {
            ProdutoModel ProdutoModel = new ProdutoModel();
            string status = "";
            string requisicao = string.Empty;

            var client = new RestClient(FuncoesBase.PostProduto);
            var request = new RestRequest(Method.POST);

            var body = new
            {
                nome = nome,
                numero = numero,
                quantidade_conserto = quantidade_conserto
            };

            int i = 10;



            request.AddJsonBody(body);

            var response = client.Execute(request);
            status = response.StatusCode.ToString();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine($"Produto foi inserido ? :  {status}");
            }


            else
            {
                var errorMessage = response.Content;
                Console.WriteLine($"Erro na solicitação: 7 : {errorMessage}");

            }

            i--;
            Thread.Sleep(3000);

        }
    }
}
