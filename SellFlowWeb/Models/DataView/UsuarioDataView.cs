using Models;

namespace SellFlowWeb.Models.DataView
{
    public class UsuarioDataView : Model
    {
        public string senha { get; set; }
        public string email { get; set; }
        public bool ativo { get; set; }
        public long? permissao { get; set; }
    }
}
