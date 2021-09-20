using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class DescricaoCalculoPgtoAdmPfeeModel
    {
        public string TipoContrato { get; set; }

        public string ParceiroDistribuidor { get; set; }

        public string VersaoContrato { get; set; }

        public string StatusContrato { get; set; }

        public string IdDocusign { get; set; }

        public DateTime? DataVigenciaInicio { get; set; }

        public DateTime? DataVigenciaFim { get; set; }

        public DateTime? DataRetroatividade { get; set; }

        public string NomeFundo { get; set; }

        public string TipoCondicao { get; set; }

        public double PercentualAdm { get; set; }

        public double PercentualPfee { get; set; }
    }
}
