namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoRentabilidadeModel : PosicaoGeralModel
    {
        public string Indexador { get; set; }

        public decimal Benchmarck { get; set; } = 0;

        public decimal RentReal { get; set; } = 0;

        public decimal VariacaoDiaria { get; set; } = 0;

        public decimal VariacaoMensal { get; set; } = 0;

        public decimal VariacaoAnual { get; set; } = 0;

        public decimal VariacaoSeisMeses { get; set; } = 0;

        public decimal VariacaoDozeMeses { get; set; } = 0;

        public decimal VariacaoVinteQuatroMeses { get; set; } = 0;

        public decimal VariacaoTrintaSeisMeses { get; set; } = 0;
    }
}
