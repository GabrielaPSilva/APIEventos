using System;

namespace DUDS.Models.Investidor
{
    public class InvestidorDistribuidorModel
    {
        public int Id { get; set; }

        public string CodInvestAdministrador { get; set; }
        
        public int CodInvestidor { get; set; }
        
        public int CodDistribuidorAdministrador { get; set; }
        
        public int CodAdministrador { get; set; }

        public int CodTipoContrato { get; set; }
        
        public int CodGrupoRebate { get; set; }

        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }

    }
}
