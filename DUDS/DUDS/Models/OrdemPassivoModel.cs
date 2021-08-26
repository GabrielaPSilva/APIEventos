using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class OrdemPassivoModel
    {
        [StringLength(15)]
        public string NumOrdem { get; set; }
        public long CodInvestidor { get; set; }
        public int CodFundo { get; set; }

        [Required]
        [StringLength(2)]
        public string DsOperacao { get; set; }
        public decimal VlValor { get; set; }
        public int IdNota { get; set; }
        public DateTime DtEnvio { get; set; }
        public DateTime DtEntrada { get; set; }
        public DateTime DtProcessamento { get; set; }
        public DateTime DtCompensacao { get; set; }
        public DateTime DtAgendamento { get; set; }
        public DateTime DtCotizacao { get; set; }
        public int OrdemMae { get; set; }
        public int CodAdministrador { get; set; }
    }
}
