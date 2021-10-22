using Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SellFlowWeb.Models.DataView
{
    public class UsuarioDataView : Model
    {
        [Required]
        [DisplayName("Senha")]
        public string senha { get; set; }

        [Required]
        [DisplayName("E-mail")]
        public string email { get; set; }

        [DisplayName("Ativo")]
        public bool ativo { get; set; }

        [Required]
        [DisplayName("Id Permissão")]
        public long? permissao { get; set; }
    }
}
