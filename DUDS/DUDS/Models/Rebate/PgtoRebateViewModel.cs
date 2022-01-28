using System;

namespace DUDS.Models.Rebate
{
    public class PgtoRebateViewModel
    {
        public DateTime DataAgendamento { get; set; }

        public string CodFundo { get; set; }

        public int TipoDespesa { get; set; }

        public decimal ValorBruto { get; set; }

        public string CnpjCpfFavorecido { get; set; }

        public string NomeFavorecido { get; set; }

        public string MesCompetencia { get; set; }

        public string AnoCompetencia { get; set; }

        public string Observacao { get; set; }

        public string CnpjFundoFavorecido { get; set; }
    }
}
