using System;
using System.Collections.Generic;

namespace Models
{
    public class PessoaModel : Model
    {
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string cpf { get; set; }
        public long? usuario { get; set; }
        public DateTime dataNascimento { get; set; }

        public bool ativo { get; set; }

        public UsuarioModel usuarioObj { get; set; }
        public ICollection<EnderecoModel> enderecoList { get; set; }
    }
}
