namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoOpcaoAcaoModel : PosicaoOpcaoModel
    {
        public string Praca { get; set; }

        public decimal Irrf { get; set; } = 0;

        public decimal ValorMercadoLiquido { get; set; } = 0;
    }
}
