using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class PagamentoServicoModel
    {
        public int Id { get; set; }

        [StringLength(7)]
        public string Competencia { get; set; }
        public int CodFundo { get; set; }
        public decimal TaxaAdm { get; set; }
        public decimal AdmFiduciaria { get; set; }
        public decimal Servico { get; set; }
        public decimal SaldoParcial { get; set; }
        public decimal SaldoGestor { get; set; }
        public string NomeFundo { get; set; }
    }
}
