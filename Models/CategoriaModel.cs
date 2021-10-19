namespace Models
{
    public class CategoriaModel
    {
        public long id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string imagemdiretorio { get; set; }
        public bool ativo { get; set; }
    }
}
