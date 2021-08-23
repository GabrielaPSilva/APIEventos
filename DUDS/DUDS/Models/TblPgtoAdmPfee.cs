using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_pgto_adm_pfee")]
    public partial class TblPgtoAdmPfee
    {
        [Key]
        [Column("competencia")]
        [StringLength(7)]
        public string Competencia { get; set; }
        [Key]
        [Column("cod_investidor_distribuidor")]
        public int CodInvestidorDistribuidor { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Key]
        [Column("cod_administrador")]
        public int CodAdministrador { get; set; }
        [Column("taxa_performance_apropriada", TypeName = "decimal(27, 15)")]
        public decimal TaxaPerformanceApropriada { get; set; }
        [Column("taxa_performance_resgate", TypeName = "decimal(27, 15)")]
        public decimal TaxaPerformanceResgate { get; set; }
        [Column("taxa_administracao", TypeName = "decimal(27, 15)")]
        public decimal TaxaAdministracao { get; set; }
        [Column("taxa_gestao", TypeName = "decimal(27, 15)")]
        public decimal TaxaGestao { get; set; }

        [ForeignKey(nameof(CodAdministrador))]
        [InverseProperty(nameof(TblAdministrador.TblPgtoAdmPfee))]
        public virtual TblAdministrador CodAdministradorNavigation { get; set; }
        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPgtoAdmPfee))]
        public virtual TblFundo CodFundoNavigation { get; set; }
        public virtual TblInvestidorDistribuidor CodInvestidorDistribuidorNavigation { get; set; }
    }
}
