using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoComApiC.Bases
{
    class FuncoesBase
    {

        // private const string _BASE_URL_API = "http://localhost:8039/Api";
        private const string _BASE_URL_API = "http://localhost:7019/Api";


        public static string Autenticacao => $"{_BASE_URL_API}/Api/buscarProduto/Juliano/10568";
        public static string PostProduto => $"{_BASE_URL_API}/salvarProduto";

        public static string UrlTabela => $"{_BASE_URL_API}/produtos";


    }
}
