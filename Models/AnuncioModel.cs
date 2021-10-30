using System;

namespace Models
{
    public class AnuncioModel : Model
    {
        public string nome { get; set; }
        public int qtdeDisponivel { get; set; }
        public string descricao { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime dataEncerramento { get; set; }
        public bool ativo { get; set; }
        public long? produto { get; set; }
        public long? anuncioSituacao { get; set; }
        public double valor { get; set; }
        public string percentPromocao { get; set; }

        public ProdutoModel produtoObj { get; set; }
        public AnuncioSituacaoModel anuncioSituacaoObj { get; set; }
    }
}
