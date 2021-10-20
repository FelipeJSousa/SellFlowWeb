using Models;
using System;

namespace SellFlowWeb.Models.DataView
{
    public class PessoaDataView : Model
    {
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string cpf { get; set; }
        public UsuarioDataView usuarioObj { get; set; }
        public DateTime dataNascimento { get; set; }
        public bool ativo { get; set; }
    }
}
