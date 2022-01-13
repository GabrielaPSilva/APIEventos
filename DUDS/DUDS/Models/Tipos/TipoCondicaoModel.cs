using System;

namespace DUDS.Models.Tipos
{
    public class TipoCondicaoModel
    {
        public int Id { get; set; }

        public string TipoCondicao { get; set; }
        
        public bool Ativo { get; set; }
        
        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }
    }
}
