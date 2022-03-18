﻿using System;

namespace DUDS.Models.Contrato
{
    public class EstruturaContratoViewModel
    {
        public double PercentualAdm { get; set; }
        
        public double PercentualPfee { get; set; }

        public int CodTipoContratoContrato { get; set; }

        public string TipoContratoContrato { get; set; }

        public int? CodGestor { get; set; }

        public int? CodDistribuidor { get; set; }

        public string Versao { get; set; }
        
        public string Status { get; set; }
        
        public bool ClausulaRetroatividade { get; set; }
        
        public DateTime? DataRetroatividade { get; set; }

        public DateTime? DataVigenciaInicio { get; set; }

        public DateTime? DataVigenciaFim { get; set; }
        
        public int? CodInvestidorContrato { get; set; }
        
        public int CodFundo { get; set; }
        
        public int CodTipoCondicao { get; set; }

        public string CodInvestAdministrador { get; set; }

        public int? AdministradorCodigoInvestidor { get; set; }

        public int? DistribuidorCodigoInvestidor { get; set; }

        public int CodContrato { get; set; }

        public int CodSubContrato { get; set; }

        public int CodContratoFundo { get; set; }

        public int CodContratoRemuneracao { get; set; }

        public int? CodGrupoRebateContrato { get; set; }

        public string NomeGrupoRebateContrato { get; set; }

    }
}
