namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoTesourariaModel : PosicaoGeralModel
    {
        public string Descricao { get; set; }

        public decimal Valor { get; set; } = 0;
    }
}
