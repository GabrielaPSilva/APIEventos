using System;

namespace DUDS.Models.PgtoServico
{
    public class PgtoServicoModel
    {
        public int Id { get; set; }

        public string Competencia { get; set; }
        
        public int CodFundo { get; set; }
        
        public decimal TaxaAdm { get; set; }
        
        public decimal AdmFiduciaria { get; set; }
        
        public decimal Servico { get; set; }
        
        public decimal SaldoParcial { get; set; }
        
        public decimal SaldoGestor { get; set; }
        
        public int CodAdministrador { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }
        
    }
}
