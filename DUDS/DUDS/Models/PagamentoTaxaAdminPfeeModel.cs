using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class PagamentoTaxaAdminPfeeModel
    {
        [StringLength(7)]
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
