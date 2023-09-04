using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoComApiC.Model
{
    class ProdutoModel
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("Nome")]
        public string nome { get; set; }

        [JsonProperty("Numero")]
        public string Numero { get; set; }

        [JsonProperty("QuantidadeConserto")]
        public int QuantidadeConserto { get; set; }
    }
}
