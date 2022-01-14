using System;

namespace DUDS.Models.Contrato
{
    public class ContratoRemuneracaoModel
    {
        public int Id { get; set; }

        public int CodContratoFundo { get; set; }

        public double PercentualAdm { get; set; }

        public double PercentualPfee { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        
    }
}
