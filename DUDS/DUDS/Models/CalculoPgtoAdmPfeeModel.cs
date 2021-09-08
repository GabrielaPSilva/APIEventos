using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class CalculoPgtoAdmPfeeModel
    {
        public string Competencia { get; set; }
        
        public int CodInvestidor { get; set; }
        
        public int CodFundo { get; set; }
        
        public int CodContrato { get; set; }
        
        public int CodSubContrato { get; set; }
        
        public int CodContratoFundo { get; set; }
        
        public int CodContratoRemuneracao { get; set; }
        
        public int? CodCondicaoRemuneracao { get; set; }
        
        public int CodAdministrador { get; set; }
        
        public decimal ValorAdm { get; set; }
        
        public decimal ValorPfeeResgate { get; set; }
        
        public decimal ValorPfeeSementre { get; set; }
        
        public decimal RebateAdm { get; set; }
        
        public decimal RebatePfeeResgate { get; set; }
        
        public decimal RebatePfeeSementre { get; set; }
        
        public string UsuarioModificacao { get; set; }
        
    }
}
