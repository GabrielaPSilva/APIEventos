using System;

namespace DUDS.Models.Rebate
{
    public class CalculoRebateModel
    {
        public Guid CodPgtoAdmPfee { get; set; }
        
        public int CodContrato { get; set; }
        
        public int CodSubContrato { get; set; }
        
        public int CodContratoFundo { get; set; }
        
        public int CodContratoRemuneracao { get; set; }
        
        public decimal RebateAdm { get; set; }
        
        public decimal RebatePfeeResgate { get; set; }
        
        public decimal RebatePfeeSemestre { get; set; }
        
        public DateTime DataCriacao { get; set; }
        
        public string UsuarioCriacao { get; set; }
    }
}
