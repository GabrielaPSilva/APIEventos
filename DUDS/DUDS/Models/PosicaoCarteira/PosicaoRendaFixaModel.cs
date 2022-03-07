using System;

namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoRendaFixaModel : PosicaoAtivoComumModel
    {
        public string Papel { get; set; }

        public string Codigo { get; set; }

        public DateTime DataCompra { get; set; }

        public string Emitente { get; set; }

        public string Ativo { get; set; }

        public decimal MtMAa { get; set; } = 0;

        public decimal TaxaOver { get; set; } = 0;

        public decimal TaxaAa { get; set; } = 0;

        public string Indexador { get; set; }

        public DateTime? DataEmissao { get; set; }

        public DateTime DataVencimento { get; set; }

        public decimal Qtde { get; set; } = 0;

        public decimal Pu { get; set; } = 0;

        public decimal ValorAplicacao { get; set; } = 0;

        public decimal ValorResgate { get; set; } = 0;

        public decimal ValorBruto { get; set; } = 0;

        public decimal Imposto { get; set; } = 0;

        public decimal ValorLiquido { get; set; } = 0;


    }
}
