using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class SubContratoModel
    {
        public int Id { get; set; }
        public int CodContrato { get; set; }

        //[Required]
        [StringLength(20)]
        public string Versao { get; set; }

        //[Required]
        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(50)]
        public string IdDocusign { get; set; }
        public bool ClausulaRetroatividade { get; set; }
        public DateTime? DataRetroatividade { get; set; }
        public DateTime? DataAssinatura { get; set; }
        public DateTime? DataVigenciaInicio { get; set; }

        [StringLength(10)]
        public string DataVigenciaFim { get; set; }

        //[Required]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public DateTime DataModificacao { get; set; }
    }
}
