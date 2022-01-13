using System;

namespace DUDS.Models.Tipos
{
    public class TipoContratoModel
    {
        public int Id { get; set; }

        public string TipoContrato { get; set; }

        public bool Ativo { get; set; }
        
        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }
    }
}
