using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Models;

namespace SellFlowWeb.Models.DataView
{
    public class ProdutoDisplayDataView : Model
    {
        [Required]
        [DisplayName("Nome do Produto")]
        public string nome { get; set; }

        [Required]
        [DisplayName("Descrição do Produto")]
        public string descricao { get; set; }

        [DisplayName("Caminho Imagem Destaque")]
        public string imagemdestaque { get; set; }

        [DisplayName("Ativo")]
        public bool ativo { get; set; }

        [DisplayName("Curtidas")]
        public long curtidas { get; set; }

        [Required]
        [DisplayName("Categoria")]
        public CategoriaDataView categoriaObj { get; set; }

        [DisplayName("Valor do Produto")]
        public string valorDecimal { get; set; }

        [Required]
        [DisplayName("Vendedor")]
        public UsuarioDataView usuariovendedorObj { get; set; }
    }
}
