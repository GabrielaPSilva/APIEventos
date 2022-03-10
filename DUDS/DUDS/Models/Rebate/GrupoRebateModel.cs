using System;

namespace DUDS.Models.Rebate
{
    public class GrupoRebateModel
    {
        public int Id { get; set; }

        public string NomeGrupoRebate { get; set; }

        public string UsuarioCriacao { get; set; }

        public bool EnviarMemoriaCalculo { get; set; }
        
        public DateTime DataCriacao { get; set; }

        public bool Ativo { get; set; }
    }
}
