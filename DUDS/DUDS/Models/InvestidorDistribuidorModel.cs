using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class InvestidorDistribuidorModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string CodInvestAdministrador { get; set; }
        public int CodInvestidor { get; set; }
        public int CodDistribuidor { get; set; }
        public int CodAdministrador { get; set; }

        public int CodTipoContrato { get; set; }
        public int CodGrupoRebate { get; set; }

        public DateTime DataCriacao { get; set; }

        [StringLength(100)]
        public string UsuarioCriacao { get; set; }

        public string NomeDistribuidor { get; set; }
        public string NomeAdministrador { get; set; }
        public string NomeInvestidor { get; set; }
        public string TipoContrato { get; set; }
        public string GrupoRebate { get; set; }
    }
}
