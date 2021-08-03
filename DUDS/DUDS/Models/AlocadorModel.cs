using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class AlocadorModel
    {
        public int Id { get; set; }
        public int CodContratoFundo { get; set; }
        public long CodCliente { get; set; }

        //[Required]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public DateTime DataModificacao { get; set; }
    }
}
