using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class ContratoModel
    {
        public int Id { get; set; }
        public int? CodDistribuidor { get; set; }

        //[Required]
        [StringLength(20)]
        public string TipoContrato { get; set; }

        //[Required]
        public bool? Ativo { get; set; }

        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public int? Parceiro { get; set; }
    }
}
