using System;

namespace DUDS.Models.Rebate
{
    public class PgtoRebateModel
    {
        public DateTime DataAgendamento { get; set; }

        public bool Efetivado { get; set; }

        public bool Importado { get; set; }

        public int? CodTipoContrato { get; set; }

        public string Competencia { get; set; } 

        public string UsuarioCriacao { get; set; }
    }
}
