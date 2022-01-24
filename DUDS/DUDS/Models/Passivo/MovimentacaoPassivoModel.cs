using System;

namespace DUDS.Models.Passivo
{
    public class MovimentacaoPassivoModel
    {
        public Guid Id { get; set; }

        public int CodFundo { get; set; }

        public DateTime DataMovimentacao { get; set; }

        public DateTime DataCotizacao { get; set; }

        public int CodInvestidorDistribuidor { get; set; }

        public int CodMovimentacao { get; set; }

        public string TipoMovimentacao { get; set; }

        public decimal QtdeCotas { get; set; }

        public decimal ValorCota { get; set; }

        public decimal ValorBruto { get; set; }

        public decimal Irrf { get; set; }

        public decimal Iof { get; set; }

        public decimal ValorLiquido { get; set; }

        public int NotaAplicacao { get; set; }

        public decimal RendimentoBruto { get; set; }

        public decimal ValorPerformance { get; set; }

        public string NumOrdem { get; set; }

        public string TipoTransferencia { get; set; }

        public int CodOrdemMae { get; set; }

        public int CodAdministrador { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

    }
}
