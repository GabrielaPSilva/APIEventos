using System;

namespace DUDS.Models.Tipos
{
    public class TipoContaModel
    {
        public int Id { get; set; }
        
        public string TipoConta { get; set; }
        
        public string DescricaoConta { get; set; }
        
        public DateTime DataCriacao { get; set; }
        
        public string UsuarioCriacao { get; set; }
    }
}
