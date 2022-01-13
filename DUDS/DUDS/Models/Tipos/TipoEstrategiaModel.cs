using System;

namespace DUDS.Models.Tipos
{
    public class TipoEstrategiaModel
    {
        public int Id { get; set; }

        public string Estrategia { get; set; }

        public bool Ativo { get; set; }
        
        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }
    }
}
