using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoComApiC.Model
{
    class ConsertoModel
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("data")]
        public DateTime Data { get; set; }

        [JsonProperty("Produto_id")]
        public ProdutoModel Produto_id { get; set; }
    }
}
