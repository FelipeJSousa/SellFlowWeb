using Models;

namespace SellFlowWeb.Models.ApiRequest
{
    public class UsuarioApiRequest : Model
    {
        public string senha { get; set; }
        public string email { get; set; }
        public bool ativo { get; set; }
        public long permissao { get; set; }
    }
}
