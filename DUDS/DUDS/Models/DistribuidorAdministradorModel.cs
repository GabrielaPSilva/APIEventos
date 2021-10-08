using System;
using System.ComponentModel.DataAnnotations;

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
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }

        public string NomeDistribuidor { get; set; }
        public string NomeAdministrador { get; set; }
    }
}
