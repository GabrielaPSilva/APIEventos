using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_acordo_distribuicao")]
    public partial class TblAcordoDistribuicao
    {
        [Key]
        [Column("cod_cliente")]
        public long CodCliente { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Key]
        [Column("cod_distribuidor")]
        public int CodDistribuidor { get; set; }
        [Required]
        [Column("destinatario")]
        [StringLength(50)]
        public string Destinatario { get; set; }
        [Column("perc_rebate_adm", TypeName = "decimal(7, 4)")]
        public decimal PercRebateAdm { get; set; }
        [Column("perc_rebate_pfee", TypeName = "decimal(7, 4)")]
        public decimal PercRebatePfee { get; set; }
        [Required]
        [Column("direcao_pagamento")]
        [StringLength(15)]
        public string DirecaoPagamento { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Column("cadastro_excluido")]
        public bool CadastroExcluido { get; set; }

        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblAcordoDistribuicao))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
    }
}
