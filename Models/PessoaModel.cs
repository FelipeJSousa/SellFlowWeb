using System;

namespace Models
{
    public class PessoaModel : Model
    {
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string cpf { get; set; }
        public long? usuario { get; set; }
        public UsuarioModel usuarioObj { get; set; }
        public DateTime dataNascimento { get; set; }
        public bool ativo { get; set; }
    }
}
