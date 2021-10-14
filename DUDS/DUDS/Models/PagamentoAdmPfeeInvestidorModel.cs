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

        public string NomeFundo { get; set; }

        public int SourceAdministrador { get; set; }

        public string NomeSourceAdministrador { get; set; }
        
        public decimal TaxaAdministracao { get; set; }
        
        public decimal TaxaPerformanceApropriada { get; set; }
        
        public decimal TaxaPerformanceResgate { get; set; }
        
        public string CodigoInvestidorAdministrador { get; set; }
        
        public int CodInvestidor { get; set; }
        
        public int CodDistribuidorInvestidor { get; set; }

        public string NomeDistribuidorInvestidor { get; set; }
        
        public int CodAdministradorCodigoInvestidor { get; set; }

        public string NomeAdministradorCodigoInvestidor { get; set; }
        
        public string NomeInvestidor { get; set; }
        
        public string Cnpj { get; set; }
        
        public string TipoCliente { get; set; }
        
        public int CodTipoContrato { get; set; }

        public string TipoContrato { get; set; }

        public int CodGrupoRebate { get; set; }

        public string NomeGrupoRebate { get; set; }

        public int? CodAdministradorInvestidor { get; set; }

        public string NomeAdministradorInvestidor { get; set; }
        
        public int? CodGestorInvestidor { get; set; }

        public string NomeGestorInvestidor { get; set; }

    }
}
