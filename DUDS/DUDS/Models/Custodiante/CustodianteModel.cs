using System;

namespace DUDS.Models
{
    public class CustodianteModel
    {
        public int Id { get; set; }

        public string NomeCustodiante { get; set; }

        public string Cnpj { get; set; }
        
        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }

        public bool Ativo { get; set; }
    }
}
