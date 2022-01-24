using System;

namespace DUDS.Models.Passivo
{
    public class PosicaoClienteModel
    {
        public Guid Id { get; set; }

        public int CodInvestidorDistribuidor { get; set; }

        public int CodFundo { get; set; }

        public DateTime DataRef { get; set; }

        public int CodAdministrador { get; set; }

        public decimal QtdeCota { get; set; }

        public decimal ValorCota { get; set; }

        public decimal ValorBruto { get; set; }

        public decimal Irrf { get; set; }

        public decimal Iof { get; set; }

        public decimal ValorLiquido  { get; set; }

        public decimal PercRendimento { get; set; }

        public decimal QtdeCotasBloqueado { get; set; }

        public decimal ValorLiqBloqueado { get; set; }

        public decimal ValorBrutoBloqueado { get; set; }

        public decimal TaxaPerformance { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

    }
}
