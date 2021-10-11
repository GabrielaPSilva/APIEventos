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
        public int? CodGestor { get; set; }
        public int CodTipoContrato { get; set; }
        public bool? Ativo { get; set; }

        [StringLength(100)]
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }

        public string NomeDistribuidor { get; set; }

        public string NomeGestor { get; set; }

        public string TipoContrato { get; set; }
    }
}
