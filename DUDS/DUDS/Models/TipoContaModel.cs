using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class TipoContaModel
    {
        public int Id { get; set; }

        //[Required]
        public string TipoConta { get; set; }

        //[Required]
        public string DescricaoConta { get; set; }
        public DateTime DataModificacao { get; set; }
        public string UsuarioModificacao { get; set; }
    }
}
