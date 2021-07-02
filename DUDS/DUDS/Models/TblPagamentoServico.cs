using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_pagamento_servico")]
    public partial class TblPagamentoServico
    {
        [Key]
        [Column("competencia")]
        [StringLength(7)]
        public string Competencia { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Column("taxa_adm", TypeName = "decimal(22, 10)")]
        public decimal TaxaAdm { get; set; }
        [Column("adm_fiduciaria", TypeName = "decimal(22, 10)")]
        public decimal AdmFiduciaria { get; set; }
        [Column("servico", TypeName = "decimal(22, 10)")]
        public decimal Servico { get; set; }
        [Column("saldo_parcial", TypeName = "decimal(22, 10)")]
        public decimal SaldoParcial { get; set; }
        [Column("saldo_gestor", TypeName = "decimal(22, 10)")]
        public decimal SaldoGestor { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPagamentoServico))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
