using System.Collections.Generic;

namespace DUDS.Models.Distribuidor
{
    public class DistribuidorViewModel : DistribuidorModel
    {
        public List<DistribuidorAdministradorModel> ListaDistribuidorAdministrador { get; set; }

        public string Classificacao { get; set; }


        public DistribuidorViewModel()
        {
            ListaDistribuidorAdministrador = new List<DistribuidorAdministradorModel>();
        }
    }
}
