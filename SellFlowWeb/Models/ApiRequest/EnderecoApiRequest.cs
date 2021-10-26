using Models;
using System.ComponentModel;

namespace SellFlowWeb.Models.ApiRequest
{
    public class EnderecoApiRequest : Model
    {
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string municipio { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
        public bool ativo { get; set; }
        public long pessoa { get; set; }
    }
}
