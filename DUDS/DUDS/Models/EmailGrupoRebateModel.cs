﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class EmailGrupoRebateModel
    {
        public int Id { get; set; }
        public int CodGrupoRebate { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }

        public string GrupoRebate { get; set; }
    }
}
