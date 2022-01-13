namespace DUDS.Models.PgtoTaxaAdmPfee
{
    public class PgtoAdmPfeeInvestidorViewModel : PgtoTaxaAdmPfeeViewModel
    {
        public int SourceAdministrador { get; set; }

        public string NomeSourceAdministrador { get; set; }
        
        public string CodInvestidorAdministrador { get; set; }
        
        public int CodInvestidor { get; set; }

        public int CodDistribuidorInvestidor { get; set; }

        public string NomeDistribuidorInvestidor { get; set; }
        
        public int CodAdministradorCodigoInvestidor { get; set; }

        public string NomeAdministradorCodigoInvestidor { get; set; }
        
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
