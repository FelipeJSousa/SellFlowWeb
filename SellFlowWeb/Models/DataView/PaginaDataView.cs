using Models;
using System.ComponentModel;

namespace SellFlowWeb.Models.DataView
{
    public class PaginaDataView : Model
    {
        [DisplayName("Nome da Página")]
        public string nome { get; set; }

        [DisplayName("Caminho da Página")]
        public string caminho { get; set; }

        [DisplayName("Ativo")]
        public bool ativo { get; set; }
    }
}
