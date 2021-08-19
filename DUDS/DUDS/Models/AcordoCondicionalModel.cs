using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class AcordoCondicionalModel
    {
        public int Id { get; set; }
        public int CodAcordoRemuneracao { get; set; }
        public int CodTipoCondicao { get; set; }
        public double PercentualAdm { get; set; }
        public double PercentualPfee { get; set; }
        public DateTime DataModificacao { get; set; }

        //[Required]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }

        //[Required]
        public bool? Ativo { get; set; }
    }
}
