using Models;

namespace SellFlowWeb.Models.ApiRequest
{
    public class CategoriaApiRequest : Model
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public string imagemdiretorio { get; set; }
        public bool ativo { get; set; }
    }
}
