using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class DescricaoCalculoRebateModel
    {
        public int CodTipoContrato { get; set; }
        
        public string TipoContrato { get; set; }

        public int? CodGestor { get; set; }
        
        public string NomeGestor { get; set; }

        public int? CodDistribuidor { get; set; }
        
        public string NomeDistribuidor { get; set; }

        public string VersaoContrato { get; set; }

        public string StatusContrato { get; set; }

        public string IdDocusign { get; set; }

        public DateTime? DataVigenciaInicio { get; set; }

        public DateTime? DataVigenciaFim { get; set; }

        public DateTime? DataRetroatividade { get; set; }

        public int CodFundo { get; set; }
        
        public string NomeFundo { get; set; }
        
        public int CodTipoCondicao { get; set; }
        
        public string TipoCondicao { get; set; }
        
        public double PercentualAdm { get; set; }
        
        public double PercentualPfee { get; set; }

        public int CodContrato { get; set; }

        public int CodSubContrato { get; set; }

        public int CodContratoFundo { get; set; }

        public int CodContratoRemuneracao { get; set; }

        public CondicaoRemuneracaoModel CondicaoRemuneracao { get; set; }
    }
}
