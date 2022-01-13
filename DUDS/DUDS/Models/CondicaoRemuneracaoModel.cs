using System;

namespace DUDS.Models
{
    public class CondicaoRemuneracaoModel
    {
        public int Id { get; set; }
        
        public int CodContratoRemuneracao { get; set; }
        
        public int CodFundo { get; set; }
        
        public DateTime? DataInicio { get; set; }
        
        public DateTime? DataFim { get; set; }
        
        public double? ValorPosicaoInicio { get; set; }
        
        public double? ValorPosicaoFim { get; set; }
        
        public double? ValorPgtoFixo { get; set; }
        
        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }

        public string NomeFundo { get; set; }
    }
}
