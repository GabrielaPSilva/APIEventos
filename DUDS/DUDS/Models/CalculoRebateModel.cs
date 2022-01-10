using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class CalculoRebateModel
    {
        public Guid Id { get; set; }
        public string Competencia { get; set; }
        
        public int CodInvestidorDistribuidor { get; set; }

        public int CodFundo { get; set; }

        public int CodContrato { get; set; }
        
        public int CodSubContrato { get; set; }
        
        public int CodContratoFundo { get; set; }
        
        public int CodContratoRemuneracao { get; set; }
        
        public int CodAdministrador { get; set; }
        
        public decimal ValorAdm { get; set; }
        
        public decimal ValorPfeeResgate { get; set; }
        
        public decimal ValorPfeeSemestre { get; set; }

        public decimal PercAdm { get; set; }

        public decimal PercPfee { get; set; }

        public decimal RebateAdm { get; set; }
        
        public decimal RebatePfeeResgate { get; set; }
        
        public decimal RebatePfeeSemestre { get; set; }
        
        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }

        public bool Ativo { get; set; }

        public string NomeInvestidor { get; set; }
        public string CNPJ { get; set; }
        public string CNPJFundo { get; set; }
        public int CodGrupoRebate { get; set; }
        public string NomeGrupoRebate { get; set; }
        public int CodTipoContrato { get; set; }
        public string NomeTipoContrato { get; set; }
        public string NomeFundo { get; set; }
        public string CodMellon { get; set; }
        public string NomeAdministrador { get; set; }
        public string NomeDistribuidor { get; set; }
    }
}
