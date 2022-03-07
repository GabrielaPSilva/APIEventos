namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoFuturoModel : PosicaoAtivoComumModel
    {
        public string Ativo { get; set; }

        public string Vencimento { get; set; }

        public string Corretora { get; set; }

        public decimal Qtde { get; set; } = 0;

        public decimal AjusteEqualizacao { get; set; } = 0;

        public decimal AjusteValorizacao { get; set; } = 0;

        public decimal PrecoMercado { get; set; } = 0;

        public decimal ValorMercado { get; set; } = 0;
    }
}
