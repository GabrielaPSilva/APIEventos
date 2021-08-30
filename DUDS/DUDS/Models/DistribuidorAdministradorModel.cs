using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class DistribuidorAdministradorModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string CodDistrAdm { get; set; }
        public int CodDistribuidor { get; set; }
        public int CodAdministrador { get; set; }

        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public DateTime DataModificacao { get; set; }
    }
}
