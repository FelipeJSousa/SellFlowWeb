using Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SellFlowWeb.Models.DataView
{
    public class PessoaDataView : Model
    {
        [Required]
        [DisplayName("Nome")]
        public string nome { get; set; }

        [Required]
        [DisplayName("Sobrenome")]
        public string sobrenome { get; set; }

        [Required]
        [DisplayName("CPF")]
        public string cpf { get; set; }

        [Required]
        [DisplayName("Usuario")]
        public UsuarioDataView usuarioObj { get; set; }

        [Required]
        [DisplayName("Data de Nascimento")]
        public DateTime dataNascimento { get; set; }

        [DisplayName("Ativo")]
        public bool ativo { get; set; }
    }
}
