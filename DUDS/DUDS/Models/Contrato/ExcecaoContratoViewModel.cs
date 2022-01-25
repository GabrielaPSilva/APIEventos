
namespace DUDS.Models.Contrato
{
    public class ExcecaoContratoViewModel : ExcecaoContratoModel
    {
        public string NomeInvestidor { get; set; }

        public string NomeFundo { get; set; }

        public int CodInvestidorDistribuidor { get; set; }

        public int CodContrato { get; set; }

        public int CodContratoAlocador { get; set; }

        public int CodContratoFundo { get; set; }
        
        public string CodInvestAdministrador { get; set; }

    }
}
