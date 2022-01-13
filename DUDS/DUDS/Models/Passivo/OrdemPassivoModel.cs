using System;

namespace DUDS.Models.Passivo
{
    public class OrdemPassivoModel
    {
        public Guid Id { get; set; }

        public string NumOrdem { get; set; }

        public int CodInvestidorDistribuidor { get; set; }

        public int CodFundo { get; set; }

        public string DescricaoOperacao { get; set; }

        public decimal ValorOrdem { get; set; }

        public int IdNota { get; set; }

        public DateTime DataEnvio { get; set; }

        public DateTime DataEntrada { get; set; }

        public DateTime DataProcessamento { get; set; }

        public DateTime DataCompensacao { get; set; }

        public DateTime DataAgendamento { get; set; }

        public DateTime DataCotizacao { get; set; }

        public int OrdemMae { get; set; }

        public int CodAdministrador { get; set; }

    }
}
