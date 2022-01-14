using System;

namespace DUDS.Models.Rebate
{
    public class ErrosPagamentoModel
    {
        public int Id { get; set; }
        
        public DateTime DataAgendamento { get; set; }
        
        public int CodFundo { get; set; }

        public string TipoDespesa { get; set; }
        
        public double ValorBruto { get; set; }

        public string CpfCnpjFavorecido { get; set; }

        public string Favorecido { get; set; }

        public string ContaFavorecida { get; set; }

        public string Competencia { get; set; }

        public string Status { get; set; }

        public string CnpjFundoInvestidor { get; set; }

        public string MensagemErro { get; set; }

        public string NomeFundo { get; set; }
    }
}
