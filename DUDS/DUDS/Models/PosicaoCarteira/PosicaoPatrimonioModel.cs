namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoPatrimonioModel : PosicaoGeralModel
    {
        public decimal QtdeCotas { get; set; } = 0;

        public decimal ValorCotaBruta { get; set; } = 0;

        public decimal ValorCotaLiquida { get; set; } = 0;

        public decimal Patrimonio { get; set; } = 0;
    }
}
