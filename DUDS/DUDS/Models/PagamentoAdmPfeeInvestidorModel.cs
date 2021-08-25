using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class PagamentoAdmPfeeInvestidorModel
    {
        public string Competencia { get; set; }
        
        public int CodFundo { get; set; }

        public int SourceAdministrador { get; set; }
        
        public decimal TaxaAdministracao { get; set; }
        
        public decimal TaxaPerformanceApropriada { get; set; }
        
        public decimal TaxaPerformanceResgate { get; set; }
        
        public string CodigoInvestidorAdministrador { get; set; }
        
        public int CodigoInvestidor { get; set; }
        
        public int CodigoDistribuidorInvestidor { get; set; }
        
        public int CodigoAdministradorCodigoInvestidor { get; set; }
        
        public string NomeInvestidor { get; set; }
        
        public string Cnpj { get; set; }
        
        public string TipoCliente { get; set; }
        
        public string DirecaoPagamento { get; set; }
        
        public int? CodigoAdministradorInvestidor { get; set; }
        
        public int? CodigoGestorInvestidor { get; set; }

    }
}
