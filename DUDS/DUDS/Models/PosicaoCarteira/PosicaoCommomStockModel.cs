namespace DUDS.Models.PosicaoCarteira
{
    // Acao, ADR e BDR
    public class PosicaoCommomStockModel : PosicaoAtivoComumModel
    {
        public string Ativo { get; set; }

        public string Papel { get; set; }

        public decimal QtdeDisponivel { get; set; } = 0;

        public decimal QtdeBloqueada { get; set; } = 0;

        public decimal QtdeTotal { get; set; } = 0;

        public decimal CustoCorretagem { get; set; } = 0;

        public decimal Cotacao { get; set; } = 0;

        public decimal CustoTotal { get; set; } = 0;

        public decimal Resultado { get; set; } = 0;

        public decimal ValorMercado { get; set; } = 0;

        public decimal Irrf { get; set; } = 0;

        public decimal ValorMercadoLiquido { get; set; } = 0;
    }
}
