using System;

namespace DUDS.Models.Rebate
{
    public class PgtoRebateModel
    {
        public Guid Id { get; set; }

        public DateTime DataAgendamento { get; set; }

        public int CodFundo { get; set; }

        public int CodTipoContrato { get; set; }

        public decimal ValorBruto { get; set; }

        public int CodDadosFavorecido { get; set; }

        public string SourceFavorecido { get; set; }

        public string Competencia { get; set; }

        public string Observacao { get; set; }

        public bool Efetivado { get; set; }

        public bool Importado { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
