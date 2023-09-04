using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoComApiC.Model
{
    class ObjetoModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Nome")]
        public string Nome { get; set; }

        [JsonProperty("Numero")]
        public string Numero { get; set; }

        [JsonProperty("Datas")]
        public List<DateTime> Datas { get; set; }
    }
}
