namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoContaCorrenteModel : PosicaoAtivoComumModel
    {
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string Instituicao { get; set; }

        public decimal Valor { get; set; } = 0;

        public decimal ValorOrigem { get; set; } = 0;

        public string MoedaOrigem { get; set; }
    }
}
