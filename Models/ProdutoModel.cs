using System.ComponentModel;

namespace Models
{
    public class ProdutoModel
    {
        [DisplayName("Id")]
        public long? id { get; set; }
        [DisplayName("Nome do Produto")]
        public string nome { get; set; }
        [DisplayName("Descrição do Produto")]
        public string descricao { get; set; }
        [DisplayName("Caminho Imagem Destaque")]
        public string imagemdestaque { get; set; }
        [DisplayName("Ativo")]
        public bool ativo { get; set; }
        [DisplayName("Curtidas")]
        public long curtidas { get; set; }
        [DisplayName("Categoria do Produto")]
        public long categoria { get; set; }
        [DisplayName("Vendedor")]
        public long usuariovendedor { get; set; }
    }
}
