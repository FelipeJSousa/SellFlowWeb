using Models;

namespace SellFlowWeb.Models.ApiRequest
{
    public class PaginaApiRequest : Model
    {
        public string nome { get; set; }
        public string caminho { get; set; }
        public bool ativo { get; set; }
    }
}
