using Models;
using System;
using System.ComponentModel;

namespace SellFlowWeb.Models.DataView
{
    public class AnuncioDataView : Model
    {
        [DisplayName("Nome do Anuncio")]
        public string nome { get; set; }

        [DisplayName("Quantidade Disponível")]
        public int qtdeDisponivel { get; set; }

        [DisplayName("Descrição do Anúncio")]
        public string descricao { get; set; }

        [DisplayName("Data da Criação")]
        public DateTime dataCriacao { get; set; }

        [DisplayName("Data de Encerramento")]
        public DateTime dataEncerramento { get; set; }
        
        [DisplayName("Ativo")]
        public bool ativo { get; set; }

        [DisplayName("Produto")]
        public ProdutoDataView produtoObj { get; set; }

        [DisplayName("Situacao")]
        public AnuncioSituacaoDataView anuncioSituacaoObj { get; set; }

    }
}
