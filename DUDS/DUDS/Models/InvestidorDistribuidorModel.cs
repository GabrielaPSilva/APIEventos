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
        public DateTime DataModificacao { get; set; }

        //[Required]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
    }
}
