using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class InvestidorModel
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string NomeInvestidor { get; set; }

        [StringLength(14)]
        public string Cnpj { get; set; }

        [StringLength(20)]
        public string TipoInvestidor { get; set; }
        public int CodTipoContrato { get; set; }
        public int CodGrupoRebate { get; set; }
        public int? CodAdministrador { get; set; }
        public int? CodGestor { get; set; }
        public DateTime DataModificacao { get; set; }

        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public bool? Ativo { get; set; }
        public List<InvestidorDistribuidorModel> ListaInvestDistribuidor { get; set; }
    }
}
