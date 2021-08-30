﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class ContratoAlocadorModel
    {
        public int Id { get; set; }
        public int CodInvestidor { get; set; }
        public int CodSubContrato { get; set; }
        public DateTime? DataTransferencia { get; set; }

        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public DateTime DataModificacao { get; set; }
    }
}
