﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class PagamentoAdminPfeeModel
    {
        public int id { get; set; }

        [StringLength(7)]
        public string Competencia { get; set; }
        public long CodCliente { get; set; }
        public int CodFundo { get; set; }
        public decimal TaxaPerformanceApropriada { get; set; }
        public decimal TaxaPerformanceResgate { get; set; }
        public decimal TaxaAdministracao { get; set; }
        public decimal TaxaGestao { get; set; }
    }
}
