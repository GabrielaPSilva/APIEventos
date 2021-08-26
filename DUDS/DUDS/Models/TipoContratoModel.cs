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

        [Required]
        [StringLength(50)]
        public string TipoContrato { get; set; }

        [Required]
        public bool? Ativo { get; set; }
        public DateTime DataModificacao { get; set; }

        [Required]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }
    }
}
