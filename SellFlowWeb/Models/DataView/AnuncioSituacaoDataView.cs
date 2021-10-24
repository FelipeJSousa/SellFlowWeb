using Models;
using System.ComponentModel;

namespace SellFlowWeb.Models.DataView
{
    public class AnuncioSituacaoDataView : Model
    {
        [DisplayName("Nome Situação")]
        public string nome { get; set; }

        [DisplayName("Descrição")]
        public string descricao { get; set; }

        [DisplayName("Ativo")]
        public bool ativo { get; set; }
    }
}
