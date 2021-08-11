using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_calculo_pgto_adm_pfee")]
    public partial class TblCalculoPgtoAdmPfee
    {
        [Key]
        [Column("competencia")]
        [StringLength(6)]
        public string Competencia { get; set; }
        [Key]
        [Column("cod_investidor")]
        public long CodInvestidor { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Column("valor_adm", TypeName = "decimal(22, 10)")]
        public decimal ValorAdm { get; set; }
        [Column("valor_pfee_resgate", TypeName = "decimal(22, 10)")]
        public decimal ValorPfeeResgate { get; set; }
        [Column("valor_pfee_sementre", TypeName = "decimal(22, 10)")]
        public decimal ValorPfeeSementre { get; set; }

        [ForeignKey(nameof(CodInvestidor))]
        [InverseProperty(nameof(TblCliente.TblCalculoPgtoAdmPfee))]
        public virtual TblCliente CodInvestidorNavigation { get; set; }
    }
}
