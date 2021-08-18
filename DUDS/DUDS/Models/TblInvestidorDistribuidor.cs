using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_investidor_distribuidor")]
    public partial class TblInvestidorDistribuidor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_invest_custodia")]
        [StringLength(50)]
        public string CodInvestCustodia { get; set; }
        [Column("cod_investidor")]
        public int CodInvestidor { get; set; }
        [Column("cod_distribuidor")]
        public int CodDistribuidor { get; set; }
        [Column("cod_custodiante")]
        public int CodCustodiante { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }

        [ForeignKey(nameof(CodCustodiante))]
        [InverseProperty(nameof(TblCustodiante.TblInvestidorDistribuidor))]
        public virtual TblCustodiante CodCustodianteNavigation { get; set; }
        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblInvestidorDistribuidor))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
        [ForeignKey(nameof(CodInvestidor))]
        [InverseProperty(nameof(TblInvestidor.TblInvestidorDistribuidor))]
        public virtual TblInvestidor CodInvestidorNavigation { get; set; }
    }
}
