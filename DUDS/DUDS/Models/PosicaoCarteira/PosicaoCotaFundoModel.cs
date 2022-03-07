namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoCotaFundoModel : PosicaoAtivoComumModel
    {
        public string CodigoAtivo { get; set; }

        public string NomeAtivo { get; set; }

        public string Instituicao { get; set; }

        public string Cnpj { get; set; }

        public string Segmento { get; set; }

        public decimal QtdeCota { get; set; } = 0;

        public decimal QtdeBloqueada { get; set; } = 0;

        public decimal ValorCota { get; set; } = 0;

        public decimal AplicResg { get; set; } = 0;

        public decimal ValorAtual { get; set; } = 0;

        public decimal Imposto { get; set; } = 0;

        public decimal ValorLiquido { get; set; } = 0;
    }
}
