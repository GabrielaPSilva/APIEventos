namespace DUDS.Models.Rebate
{
    public class CalculoRebateViewModel : CalculoRebateModel
    {
        public string NomeInvestidor { get; set; }

        public string Competencia { get; set; }
        
        public string CNPJ { get; set; }
        
        public string CNPJFundo { get; set; }
        
        public int CodGrupoRebate { get; set; }
        
        public string NomeGrupoRebate { get; set; }
        
        public int CodTipoContrato { get; set; }
        
        public string NomeTipoContrato { get; set; }
        
        public string NomeFundo { get; set; }
        
        public string CodMellon { get; set; }
        
        public string NomeAdministrador { get; set; }
        
        public string NomeDistribuidor { get; set; }
        
        public decimal ValorAdm { get; set; }
        
        public decimal ValorPfeeResgate { get; set; }
        
        public decimal ValorPfeeSemestre { get; set; }
        
        public decimal PercAdm { get; set; }
        
        public decimal PercPfee { get; set; }
    }
}
