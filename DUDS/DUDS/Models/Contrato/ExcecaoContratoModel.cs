using System;

namespace DUDS.Models.Contrato
{
    public class ExcecaoContratoModel
    {
        public int Id { get; set; }

        public int CodSubContrato { get; set; }

        public int CodInvestidor { get; set; }

        public int CodFundo { get; set; }

        public decimal PercAdm { get; set; }

        public decimal PercPfee { get; set; }

        public decimal ValorFixo { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool Ativo { get; set; }
    }
}
