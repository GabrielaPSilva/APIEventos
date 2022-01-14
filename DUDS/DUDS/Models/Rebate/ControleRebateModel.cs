using System;

namespace DUDS.Models.Rebate
{
    public class ControleRebateModel
    {
        public int Id { get; set; }
        
        public int CodGrupoRebate { get; set; }
        
        public string Competencia { get; set; }
        
        public bool Validado { get; set; }
        
        public bool Enviado { get; set; }
        
        public string UsuarioCriacao { get; set; }
        
        public DateTime DataCriacao { get; set; }

        
    }
}
