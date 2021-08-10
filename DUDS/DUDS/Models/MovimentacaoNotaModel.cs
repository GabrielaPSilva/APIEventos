using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class MovimentacaoNotaModel
    {
        public int CodFundo { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public DateTime DataCotizacao { get; set; }
        public long CdCotista { get; set; }
        public int CodMovimentacao { get; set; }

        [Required]
        [StringLength(2)]
        public string TipoMovimentacao { get; set; }
        public decimal QtdeCotas { get; set; }
        public decimal ValorCota { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal Irrf { get; set; }
        public decimal Iof { get; set; }
        public decimal ValorLiquido { get; set; }
        public int NotaAplicacao { get; set; }
        public decimal RendimentoBruto { get; set; }
        public decimal ValorPerformance { get; set; }
        public int NumOrdem { get; set; }

        [Required]
        [StringLength(15)]
        public string TipoTransferencia { get; set; }
        public int CodDistribuidor { get; set; }

        [Required]
        [StringLength(10)]
        public string Operador { get; set; }
        public int CodGestor { get; set; }
        public int CodOrdemMae { get; set; }

        [Required]
        [StringLength(1)]
        public string Penalty { get; set; }
        public int CodAdm { get; set; }

        [Required]
        [StringLength(100)]
        public string ClassTributaria { get; set; }
        public int CodCustodiante { get; set; }
    }
}
