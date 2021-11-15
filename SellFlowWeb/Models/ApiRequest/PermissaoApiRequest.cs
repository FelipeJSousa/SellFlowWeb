using Models;

namespace SellFlowWeb.Models.ApiRequest
{
    public class PermissaoApiRequest : Model
    {
        public string nome { get; set; }
        public bool ativo { get; set; }
    }
}
