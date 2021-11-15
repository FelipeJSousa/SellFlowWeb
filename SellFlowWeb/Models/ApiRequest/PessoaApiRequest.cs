using System;
using Models;

namespace SellFlowWeb.Models.ApiRequest
{
    public class PessoaApiRequest : Model
    {
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string cpf { get; set; }
        public long usuario { get; set; }
        public DateTime dataNascimento { get; set; }
        public bool ativo { get; set; }
    }
}
