using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class EstruturaContratoValidoModel
    {
        public double PercentualAdm { get; set; }
        
        public double PercentualPfee { get; set; }

        public string TipoContrato { get; set; }
        
        public int? Parceiro { get; set; }

        public int? CodDistribuidor { get; set; }

        public string Versao { get; set; }
        
        public string Status { get; set; }
        
        public bool ClausulaRetroatividade { get; set; }
        
        public DateTime? DataRetroatividade { get; set; }

        public DateTime? DataVigenciaInicio { get; set; }

        public DateTime? DataVigenciaFim { get; set; }
        
        public int? IdInvestidor { get; set; }
        
        public int CodFundo { get; set; }
        
        public int CodTipoCondicao { get; set; }

    }
}
