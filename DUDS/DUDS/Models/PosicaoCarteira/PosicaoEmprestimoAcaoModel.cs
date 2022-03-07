using System;

namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoEmprestimoAcaoModel : PosicaoAtivoComumModel
    {
        public string TipoOperacao { get; set; }

        public string CodOperacao { get; set; }

        public DateTime DataOperacao { get; set; }

        public DateTime DataVcto { get; set; }

        public string Ativo { get; set; }

        public string Papel { get; set; }

        public decimal Qtde { get; set; } = 0;

        public decimal Preco { get; set; } = 0;

        public decimal Financeiro { get; set; } = 0;

        public decimal PercSobreRemuneracao { get; set; } = 0;

        public decimal PercSobreMercado { get; set; } = 0;

        public decimal Remuneracao { get; set; } = 0;

        public decimal Taxas { get; set; } = 0;

        public decimal Total { get; set; } = 0;
    }
}
