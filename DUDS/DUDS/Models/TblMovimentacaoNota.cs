using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_movimentacao_nota")]
    [Index(nameof(CdCotista), Name = "cd_cotista_tbl_movimentacao_nota")]
    [Index(nameof(CodAdm), Name = "cod_adm_tbl_movimentacao_nota")]
    [Index(nameof(CodDistribuidor), Name = "cod_distr_tbl_movimentacao_nota")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_movimentacao_nota")]
    [Index(nameof(CodGestor), Name = "cod_gestor_tbl_movimentacao_nota")]
    [Index(nameof(DataCotizacao), Name = "data_cotizacao_tbl_movimentacao_nota")]
    [Index(nameof(DataMovimentacao), Name = "data_movim_tbl_movimentacao_nota")]
    public partial class TblMovimentacaoNota
    {
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Column("data_movimentacao", TypeName = "date")]
        public DateTime DataMovimentacao { get; set; }
        [Column("data_cotizacao", TypeName = "date")]
        public DateTime DataCotizacao { get; set; }
        [Column("cd_cotista")]
        public long CdCotista { get; set; }
        [Key]
        [Column("cod_movimentacao")]
        public int CodMovimentacao { get; set; }
        [Required]
        [Column("tipo_movimentacao")]
        [StringLength(2)]
        public string TipoMovimentacao { get; set; }
        [Column("qtde_cotas", TypeName = "decimal(22, 10)")]
        public decimal QtdeCotas { get; set; }
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
        [Key]
        [Column("nota_aplicacao")]
        public int NotaAplicacao { get; set; }
        [Column("rendimento_bruto", TypeName = "decimal(22, 10)")]
        public decimal RendimentoBruto { get; set; }
        [Column("valor_performance", TypeName = "decimal(22, 10)")]
        public decimal ValorPerformance { get; set; }
        [Key]
        [Column("num_ordem")]
        public int NumOrdem { get; set; }
        [Required]
        [Column("tipo_transferencia")]
        [StringLength(15)]
        public string TipoTransferencia { get; set; }
        [Column("cod_distribuidor")]
        public int CodDistribuidor { get; set; }
        [Required]
        [Column("operador")]
        [StringLength(10)]
        public string Operador { get; set; }
        [Column("cod_gestor")]
        public int CodGestor { get; set; }
        [Key]
        [Column("cod_ordem_mae")]
        public int CodOrdemMae { get; set; }
        [Required]
        [Column("penalty")]
        [StringLength(1)]
        public string Penalty { get; set; }
        [Column("cod_adm")]
        public int CodAdm { get; set; }
        [Required]
        [Column("class_tributaria")]
        [StringLength(100)]
        public string ClassTributaria { get; set; }

        [ForeignKey(nameof(CdCotista))]
        [InverseProperty(nameof(TblCliente.TblMovimentacaoNota))]
        public virtual TblCliente CdCotistaNavigation { get; set; }
        [ForeignKey(nameof(CodAdm))]
        [InverseProperty(nameof(TblAdministrador.TblMovimentacaoNota))]
        public virtual TblAdministrador CodAdmNavigation { get; set; }
        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblMovimentacaoNota))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblMovimentacaoNota))]
        public virtual TblFundo CodFundoNavigation { get; set; }
        [ForeignKey(nameof(CodGestor))]
        [InverseProperty(nameof(TblGestor.TblMovimentacaoNota))]
        public virtual TblGestor CodGestorNavigation { get; set; }
    }
}
