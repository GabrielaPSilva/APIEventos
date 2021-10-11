using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class TipoContratoModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string TipoContrato { get; set; }

        public bool? Ativo { get; set; }
        public DateTime DataCriacao { get; set; }

        [StringLength(50)]
        public string UsuarioCriacao { get; set; }
    }
}
