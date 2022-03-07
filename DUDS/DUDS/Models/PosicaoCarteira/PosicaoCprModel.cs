namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoCprModel : PosicaoGeralModel
    {
        public string Operacao { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; } = 0;
    }
}
