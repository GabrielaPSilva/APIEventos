using System;

namespace DUDS.Models.Distribuidor
{
    public class DistribuidorAdministradorModel
    {
        public int Id { get; set; }

        public int CodDistribuidor { get; set; }

        public int CodAdministrador { get; set; }

        public string CodDistrAdm { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

    }
}
