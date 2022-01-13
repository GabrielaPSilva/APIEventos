using System;

namespace DUDS.Models.SubContrato
{
    public class SubContratoModel
    {
        public int Id { get; set; }
        
        public int CodContrato { get; set; }

        public string Versao { get; set; }

        public string Status { get; set; }

        public string IdDocusign { get; set; }
        
        public DateTime DataInclusaoContrato { get; set; }
        
        public bool ClausulaRetroatividade { get; set; }
        
        public DateTime? DataRetroatividade { get; set; }
        
        public DateTime? DataAssinatura { get; set; }
        
        public DateTime? DataVigenciaInicio { get; set; }
        
        public DateTime? DataVigenciaFim { get; set; }

        public string UsuarioCriacao { get; set; }
        
        public DateTime DataCriacao { get; set; }

    }
}
