using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public int CodFundo { get; set; }
        public int CodTipoConta { get; set; }

        //[Required]
        public string Banco { get; set; }

        //[Required]
        public string Agencia { get; set; }

        //[Required]
        public string Conta { get; set; }
        public DateTime DataModificacao { get; set; }
        public string UsuarioModificacao { get; set; }
    }
}
