using System;

namespace DUDS.Models
{
    public class PosicaoClientePassivoModel
    {
        public Guid Id { get; set; }

        public int CodInvestidorDistribuidor { get; set; }

        public int CodFundo { get; set; }

        public DateTime DataRef { get; set; }

        public int CodAdministrador { get; set; }

        public double QtdeCota { get; set; }

        public double ValorCota { get; set; }

        public double ValorBruto { get; set; }

        public double Irrf { get; set; }

        public double Iof { get; set; }

        public double ValorLiquido  { get; set; }

        public double PercRendimento { get; set; }

        public double QtdeCotasBloqueado { get; set; }

        public double ValorLiqBloqueado { get; set; }

        public double ValorBrutoBloqueado { get; set; }

        // Campos adicionais para facilitar acompanhar os respectivos codigos
        public string NomeInvestidor { get; set; }

        public string NomeFundo { get; set; }

        public string NomeAdministradorInvestidor { get; set; }
        
        public string NomeAdministradorDados { get; set; }

        public string NomeDistribuidor { get; set; }

        public string NomeGestor { get; set; }
    }
}
