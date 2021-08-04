using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class AcordoRemuneracaoModel
    {
        public int Id { get; set; }
        public int CodContratoDistribuicao { get; set; }
        public double Inicio { get; set; }
        public double Fim { get; set; }
        public double Percentual { get; set; }

        [StringLength(15)]
        public string TipoTaxa { get; set; }

        [StringLength(15)]
        public string TipoRange { get; set; }
        public DateTime DataVigenciaInicio { get; set; }
        public DateTime DataVigenciaFim { get; set; }

        //[Required]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public DateTime DataModificacao { get; set; }

        //[Required]
        public bool? Ativo { get; set; }
    }
}
