using System;

namespace DUDS.Models.Tipos
{
    public class TipoClassificacaoModel
    {
        public int Id { get; set; }

        public string Classificacao { get; set; }
        
        public bool Ativo { get; set; }
        
        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }

    }
}
