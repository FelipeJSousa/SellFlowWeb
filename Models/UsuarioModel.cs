namespace Models
{
    public class UsuarioModel : Model
    {
        public string senha { get; set; }
        public string email { get; set; }
        public bool ativo { get; set; }
        public long? permissao { get; set; }
        public PermissaoModel permissaoObj { get; set; }
    }
}
