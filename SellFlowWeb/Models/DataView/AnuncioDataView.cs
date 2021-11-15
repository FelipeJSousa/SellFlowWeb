using Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public ProdutoDisplayDataView produtoObj { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Valor do Anuncio")]
        public string valorDecimal { get; set; }

        [DisplayName("Desconto")]
        public string percentPromocao { get; set; }

        [DisplayName("Situacao")]
        public AnuncioSituacaoDataView anuncioSituacaoObj { get; set; }

        [DisplayName("Vendedor")]
        public string vendedor { get; set; }


    }
}
