namespace Models
{
    public class EnderecoModel : Model
    {
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public bool ativo { get; set; }
    }
}
