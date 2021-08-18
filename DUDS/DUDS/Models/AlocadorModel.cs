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
        public int CodInvestidor { get; set; }
        public int CodContratoDistribuicao { get; set; }

        //[Required]
        [StringLength(20)]
        public string DirecaoPagamento { get; set; }

        //[Required]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public DateTime DataModificacao { get; set; }
    }
}
