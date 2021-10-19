namespace Models
{
    public class UsuarioModel
    {
        public long id { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        public bool ativo { get; set; }
        public long permissao { get; set; }
    }
}
