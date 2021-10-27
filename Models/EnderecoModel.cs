using System.Collections.Generic;

namespace Models
{
    public class EnderecoModel : Model
    {
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string municipio { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public bool ativo { get; set; }
        public long pessoa { get; set; }

        public PessoaModel pessoaModel { get; set; }
        public List<EnderecoModel> enderecoList { get; set; }
    }
}
