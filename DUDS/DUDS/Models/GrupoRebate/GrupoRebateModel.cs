using System;

namespace DUDS.Models.GrupoRebate
{
    public class GrupoRebateModel
    {
        public int Id { get; set; }

        public string NomeGrupoRebate { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool Ativo { get; set; }
    }
}
