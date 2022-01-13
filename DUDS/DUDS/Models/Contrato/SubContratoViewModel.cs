using System.Collections.Generic;

namespace DUDS.Models.Contrato
{
    public class SubContratoViewModel : SubContratoModel
    {
        public List<ContratoAlocadorModel> ListaContratoAlocador { get; set; }

        public List<ContratoFundoModel> ListaContratoFundo { get; set; }

        public SubContratoViewModel()
        {
            ListaContratoFundo = new List<ContratoFundoModel>();
            ListaContratoAlocador = new List<ContratoAlocadorModel>();
        }
    }
}
