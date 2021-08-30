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
        public int? CodFundo { get; set; }
        public int? CodInvestidor { get; set; }
        public int CodTipoConta { get; set; }

        [StringLength(4)]
        public string Banco { get; set; }

        [StringLength(10)]
        public string Agencia { get; set; }

        [StringLength(15)]
        public string Conta { get; set; }
        public DateTime DataModificacao { get; set; }

        [StringLength(50)]
        public string UsuarioModificacao { get; set; }

        public bool? Ativo { get; set; }
    }
}
