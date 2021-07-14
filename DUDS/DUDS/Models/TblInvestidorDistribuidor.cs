using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Keyless]
    [Table("tbl_investidor_distribuidor")]
    public partial class TblInvestidorDistribuidor
    {
        [Column("cod_invest_custodia")]
        [StringLength(50)]
        public string CodInvestCustodia { get; set; }
        [Column("cod_investidor")]
        public int CodInvestidor { get; set; }
        [Column("cod_distribuidor")]
        public int CodDistribuidor { get; set; }
        [Column("cod_custodiante")]
        public int CodCustodiante { get; set; }

        [ForeignKey(nameof(CodCustodiante))]
        public virtual TblCustodiante CodCustodianteNavigation { get; set; }
        [ForeignKey(nameof(CodDistribuidor))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
        [ForeignKey(nameof(CodInvestidor))]
        public virtual TblInvestidor CodInvestidorNavigation { get; set; }
    }
}
