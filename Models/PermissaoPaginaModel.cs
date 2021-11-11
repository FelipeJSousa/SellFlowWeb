namespace Models
{
    public class PermissaoPaginaModel : Model
    {
        public long permissao { get; set; }

        public PermissaoModel permissaoObj { get; set; }

        public long pagina { get; set; }

        public PaginaModel paginaObj { get; set; }
    }
}
