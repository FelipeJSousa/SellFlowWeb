using Models;
using System.ComponentModel;

namespace SellFlowWeb.Models.DataView
{
    public class PermissaoDataView : Model
    {
        [DisplayName("Nome da Permissao")]
        public string nome { get; set; }

        [DisplayName("Ativo")]
        public bool ativo { get; set; }
    }
}
