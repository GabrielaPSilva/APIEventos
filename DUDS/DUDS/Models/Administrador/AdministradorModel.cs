using System;

namespace DUDS.Models.Administrador
{
    public class AdministradorModel
    {
        public int Id { get; set; }

        public string NomeAdministrador { get; set; }

        public string Cnpj { get; set; }

        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }

        public bool Ativo { get; set; }

    }
}
