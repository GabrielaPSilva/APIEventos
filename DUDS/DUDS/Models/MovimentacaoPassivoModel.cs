using System;

namespace DUDS.Models
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

        public double QtdeCotas { get; set; }

        public double ValorCota { get; set; }

        public double Irrf { get; set; }

        public double Iof { get; set; }

        public double ValorLiquido { get; set; }

        public int NotaAplicacao { get; set; }

        public double RendimentoBruto { get; set; }

        public double ValorPerformance { get; set; }

        public int NumOrdem { get; set; }

        public string TipoTransferencia { get; set; }

        public int CodOrdemMae { get; set; }

        public int CodAdministrador { get; set; }

        public string NomeFundo { get; set; }

        public string NomeInvestidor { get; set; }

        public string NomeAdministrador { get; set; }
    }
}
