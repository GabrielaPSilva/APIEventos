using System;

namespace DUDS.Models.GrupoRebate
{
    public class EmailGrupoRebateModel
    {
        public int Id { get; set; }

        public int CodGrupoRebate { get; set; }

        public string Email { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool Ativo { get; set; }

    }
}
