using System;

namespace DUDS.Models.Passivo
{
    public class PosicaoClienteMaiorValorAlocadoModel
    {
        public DateTime DataMaiorPosicao { get; set; }

        public decimal MaiorValorPosicao { get; set; }

        public int? CodDistribuidor { get; set; }

        public int? CodGestor { get; set; }

        public int? CodInvestidorDistribuidor { get; set; }

        public int? CodFundo { get; set; }

        public DateTime DataPosicao { get; set; }
    }
}
