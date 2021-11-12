using Microsoft.AspNetCore.Http;

namespace Models
{
    public class ProdutoModel : Model
    {
        public string nome { get; set; }

        public string descricao { get; set; }

        public string imagemdestaque { get; set; }

        public IFormFile imagemArquivo { get; set; }

        public bool ativo { get; set; }

        public long curtidas { get; set; }

        public long categoria { get; set; }

        public long usuario { get; set; }
        
        public double valor { get; set; }

        public UsuarioModel usuarioObj { get; set; }
        public CategoriaModel categoriaObj { get; set; }
    }
}
