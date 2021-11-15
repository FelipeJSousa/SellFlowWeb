using Microsoft.AspNetCore.Http;
using Models;

namespace SellFlowWeb.Models.ApiRequest
{
    public class ProdutoApiRequest : Model
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public string imagemdestaque { get; set; }
        public IFormFile imagemArquivo { get; set; }
        public bool ativo { get; set; }
        public long curtidas { get; set; }
        public long categoria { get; set; }
        public double valor { get; set; }
        public long usuario { get; set; }
    }
}
