using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class InvestidorModel
    {
        public int Id { get; set; }

        //[Required]
        [StringLength(200)]
        public string NomeCliente { get; set; }

        [StringLength(14)]
        public string Cnpj { get; set; }

        //[Required]
        [StringLength(20)]
        public string TipoCliente { get; set; }
        public int? CodAdministrador { get; set; }
        public int? CodGestor { get; set; }
        public DateTime DataModificacao { get; set; }

        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public bool? Ativo { get; set; }
    }
}
