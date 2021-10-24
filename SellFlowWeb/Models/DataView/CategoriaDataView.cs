using Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SellFlowWeb.Models.DataView
{
    public class CategoriaDataView : Model
    {
        [DisplayName("Nome da Categoria")]
        public string nome { get; set; }

        [DisplayName("Descrição")]
        public string descricao { get; set; }

        [DisplayName("Caminho Imagem")]
        public string imagemdiretorio { get; set; }

        [DisplayName("Ativo")]
        public bool ativo { get; set; }
    }
}
