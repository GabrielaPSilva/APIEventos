using System.Collections.Generic;

namespace DUDS.Models.Contrato
{
    public class ContratoViewModel : ContratoModel
    {
        public string NomeDistribuidor { get; set; }

        public string NomeGestor { get; set; }

        public string TipoContrato { get; set; }

        public string NomeGrupoRebate { get; set; }

        public List<SubContratoModel> ListaSubContrato { get; set; }

        public ContratoViewModel()
        {
            ListaSubContrato = new List<SubContratoModel>();
        }
    }
}
