using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_cliente")]
    [Index(nameof(CodCliente), Name = "IX_tbl_posicao_cliente_cod_cliente")]
    [Index(nameof(CodFundo), Name = "IX_tbl_posicao_cliente_cod_fundo")]
    [Index(nameof(DataRef), Name = "IX_tbl_posicao_cliente_data_ref")]
    public partial class TblPosicaoCliente
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
        [Key]
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Column("qtde_cota", TypeName = "decimal(22, 10)")]
        public decimal QtdeCota { get; set; }
        [Column("valor_cota", TypeName = "decimal(22, 10)")]
        public decimal ValorCota { get; set; }
        [Column("valor_bruto", TypeName = "decimal(22, 10)")]
        public decimal ValorBruto { get; set; }
        [Column("irrf", TypeName = "decimal(22, 10)")]
        public decimal Irrf { get; set; }
        [Column("iof", TypeName = "decimal(22, 10)")]
        public decimal Iof { get; set; }
        [Column("valor_liquido", TypeName = "decimal(22, 10)")]
        public decimal ValorLiquido { get; set; }
        [Column("perc_rendimento", TypeName = "decimal(22, 10)")]
        public decimal PercRendimento { get; set; }
        [Column("qtde_cotas_bloqueado", TypeName = "decimal(22, 10)")]
        public decimal QtdeCotasBloqueado { get; set; }
        [Column("valor_liq_bloqueado", TypeName = "decimal(22, 10)")]
        public decimal ValorLiqBloqueado { get; set; }
        [Column("valor_bruto_bloqueado", TypeName = "decimal(22, 10)")]
        public decimal ValorBrutoBloqueado { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(TblCliente.TblPosicaoCliente))]
        public virtual TblCliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblPosicaoCliente))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPosicaoCliente))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
