using Models;

namespace SellFlowWeb.Models.DataView
{
    public class CategoriaDataView : Model
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public string imagemdiretorio { get; set; }
        public bool ativo { get; set; }
    }
}
