using System;

namespace DUDS.Models.Contrato
{
    public class ContratoAlocadorModel
    {
        public int Id { get; set; }
        
        public int CodInvestidor { get; set; }
        
        public int CodSubContrato { get; set; }
        
        public DateTime? DataTransferencia { get; set; }

        public string UsuarioCriacao { get; set; }
        
        public DateTime DataCriacao { get; set; }

    }
}
