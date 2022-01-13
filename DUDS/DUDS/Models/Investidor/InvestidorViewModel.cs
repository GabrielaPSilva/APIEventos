using System.Collections.Generic;

namespace DUDS.Models.Investidor
{
    public class InvestidorViewModel : InvestidorModel
    {
        public string NomeAdministrador { get; set; }

        public string NomeGestor { get; set; }

        public List<InvestidorDistribuidorModel> ListaInvestDistribuidor { get; set; }
    }
}
