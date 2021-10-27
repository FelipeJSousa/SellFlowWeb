using Models;
using System.ComponentModel;

namespace SellFlowWeb.Models.DataView
{
    public class EnderecoDataView : Model
    {
        [DisplayName("Logradouro")]
        public string logradouro { get; set; }

        [DisplayName("Bairro")]
        public string bairro { get; set; }

        [DisplayName("Cidade")]
        public string cidade { get; set; }

        [DisplayName("Município")]
        public string municipio { get; set; }

        [DisplayName("Estado")]
        public string estado { get; set; }

        [DisplayName("CEP")]
        public string cep { get; set; }

        [DisplayName("Ativo")]
        public bool ativo { get; set; }
    }
}
