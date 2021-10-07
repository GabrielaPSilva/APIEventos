using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class ErrosPagamentoModel
    {
        public int Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public int CodFundo { get; set; }

        public string NomeFundo { get; set; }

        [StringLength(50)]
        public string TipoDespesa { get; set; }
        public double ValorBruto { get; set; }

        [StringLength(14)]
        public string CpfCnpjFavorecido { get; set; }

        [StringLength(50)]
        public string Favorecido { get; set; }

        [StringLength(100)]
        public string ContaFavorecida { get; set; }

        [StringLength(10)]
        public string Competencia { get; set; }

        [StringLength(30)]
        public string Status { get; set; }

        [StringLength(14)]
        public string CnpjFundoInvestidor { get; set; }

        [StringLength(100)]
        public string MensagemErro { get; set; }
    }
}
