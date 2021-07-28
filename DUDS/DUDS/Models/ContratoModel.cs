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

        [Required]
        [StringLength(20)]
        public string TipoContrato { get; set; }

        [Required]
        [StringLength(20)]
        public string Versao { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(50)]
        public string IdDocusign { get; set; }

        [Required]
        [StringLength(20)]
        public string DirecaoPagamento { get; set; }
        public bool ClausulaRetroatividade { get; set; }
        public DateTime? DataRetroatividade { get; set; }
        public DateTime? DataAssinatura { get; set; }
        public bool? Ativo { get; set; }

        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public DateTime? DataModificacao { get; set; }
    }
}
