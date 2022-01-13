using System;

namespace DUDS.Models.PgtoTaxaAdmPfee
{
    public class PgtoTaxaAdmPfeeModel
    {
        public Guid Id { get; set; }

        public string Competencia { get; set; }
        
        public int CodInvestidorDistribuidor { get; set; }
        
        public int CodFundo { get; set; }
        
        public int CodAdministrador { get; set; }
        
        public decimal TaxaPerformanceApropriada { get; set; }
        
        public decimal TaxaPerformanceResgate { get; set; }
        
        public decimal TaxaAdministracao { get; set; }
        
        public decimal TaxaGestao { get; set; }

    }
}
