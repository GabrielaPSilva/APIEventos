using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class CalculoRebateModel
    {
        public Guid Id { get; set; }
        public Guid CodPgtoAdmPfee { get; set; }
        public int CodInvestidorDistribuidor { get; set; }
        public int CodContrato { get; set; }
        public int CodSubContrato { get; set; }
        public int CodContratoFundo { get; set; }
        public int CodContratoRemuneracao { get; set; }
        public decimal RebateAdm { get; set; }
        public decimal RebatePfeeResgate { get; set; }
        public decimal RebatePfeeSemestre { get; set; }
        public DateTime DataCriacao { get; set; }
        public string UsuarioCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
