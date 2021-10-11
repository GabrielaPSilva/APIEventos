using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class ContratoRemuneracaoModel
    {
        public int Id { get; set; }
        public int CodContratoFundo { get; set; }
        public double PercentualAdm { get; set; }
        public double PercentualPfee { get; set; }

        [StringLength(100)]
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
