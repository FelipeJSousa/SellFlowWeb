using Models;
using System;

namespace SellFlowWeb.Models.ApiRequest
{
    public class AnuncioApiRequest : Model
    {
        public string nome { get; set; }
        public int qtdeDisponivel { get; set; }
        public string descricao { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataEncerramento { get; set; }
        public bool ativo { get; set; }
        public long? produto { get; set; }
        public long? anunciosituacao { get; set; }

    }
}
