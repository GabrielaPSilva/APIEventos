using System;

namespace DUDS.Models.Rebate
{
    public class CalculoServicoModel
    {
        public Guid Id { get; set; }

        public string Competencia { get; set; }

        public int CodFundo { get; set; }

        public int? CodDistribuidor { get; set; }

        public int? CodCustodiante { get; set; }

        public int? CodAdministrador { get; set; }

        public decimal Valor { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
