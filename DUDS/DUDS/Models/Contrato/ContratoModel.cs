using System;

namespace DUDS.Models.Contrato
{
    public class ContratoModel
    {
        public int Id { get; set; }
        
        public int? CodDistribuidor { get; set; }
        
        public int? CodGestor { get; set; }
        
        public int CodTipoContrato { get; set; }
        
        public bool Ativo { get; set; }

        public string UsuarioCriacao { get; set; }
        
        public DateTime DataCriacao { get; set; }

    }
}
