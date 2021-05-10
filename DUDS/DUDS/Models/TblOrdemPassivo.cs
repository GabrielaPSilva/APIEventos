using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_ordem_passivo")]
    [Index(nameof(CdCotista), Name = "cd_cotista_tbl_ordem_passivo")]
    [Index(nameof(CodDistribuidor), Name = "cod_distr_tbl_ordem_passivo")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_ordem_passivo")]
    [Index(nameof(DtAgendamento), Name = "dt_agend_tbl_ordem_passivo")]
    [Index(nameof(DtCompensacao), Name = "dt_comp_tbl_ordem_passivo")]
    [Index(nameof(DtCotizacao), Name = "dt_cot_tbl_ordem_passivo")]
    [Index(nameof(DtEntrada), Name = "dt_entrada_tbl_ordem_passivo")]
    [Index(nameof(OrdemMae), Name = "ordem_mae_tbl_ordem_passivo")]
    public partial class TblOrdemPassivo
    {
        [Key]
        [Column("num_ordem")]
        public int NumOrdem { get; set; }
        [Column("cd_cotista")]
        public long CdCotista { get; set; }
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Required]
        [Column("ds_operacao")]
        [StringLength(2)]
        public string DsOperacao { get; set; }
        [Column("vl_valor", TypeName = "decimal(22, 10)")]
        public decimal VlValor { get; set; }
        [Column("id_nota")]
        public int IdNota { get; set; }
        [Required]
        [Column("ds_liquidacao")]
        [StringLength(5)]
        public string DsLiquidacao { get; set; }
        [Column("id_banco")]
        public int IdBanco { get; set; }
        [Column("cd_agencia_externa")]
        public int CdAgenciaExterna { get; set; }
        [Column("nu_conta_externa")]
        public int? NuContaExterna { get; set; }
        [Column("dv_conta_externa")]
        public int? DvContaExterna { get; set; }
        [Column("ic_enviada")]
        public int IcEnviada { get; set; }
        [Column("dt_envio", TypeName = "date")]
        public DateTime DtEnvio { get; set; }
        [Column("ic_processada")]
        public int IcProcessada { get; set; }
        [Column("ic_cancelada")]
        public int IcCancelada { get; set; }
        [Column("ic_autorizada")]
        public int IcAutorizada { get; set; }
        [Column("cd_desenquadramento")]
        public int? CdDesenquadramento { get; set; }
        [Column("ds_desenquadramento")]
        [StringLength(500)]
        public string DsDesenquadramento { get; set; }
        [Required]
        [Column("id_usuario")]
        [StringLength(100)]
        public string IdUsuario { get; set; }
        [Column("dt_entrada", TypeName = "date")]
        public DateTime DtEntrada { get; set; }
        [Column("dt_processamento", TypeName = "date")]
        public DateTime DtProcessamento { get; set; }
        [Column("dt_compensacao", TypeName = "date")]
        public DateTime DtCompensacao { get; set; }
        [Column("ds_comentarios")]
        [StringLength(500)]
        public string DsComentarios { get; set; }
        [Column("nu_sequencial")]
        public int? NuSequencial { get; set; }
        [Required]
        [Column("sn_bloqueio")]
        [StringLength(5)]
        public string SnBloqueio { get; set; }
        [Column("dt_agendamento", TypeName = "date")]
        public DateTime DtAgendamento { get; set; }
        [Column("dt_cotizacao", TypeName = "date")]
        public DateTime DtCotizacao { get; set; }
        [Required]
        [Column("nm_operador")]
        [StringLength(20)]
        public string NmOperador { get; set; }
        [Column("cod_distribuidor")]
        public int CodDistribuidor { get; set; }
        [Required]
        [Column("cpf_cnpj_cotista")]
        [StringLength(14)]
        public string CpfCnpjCotista { get; set; }
        [Column("ordem_mae")]
        public int OrdemMae { get; set; }
        [Required]
        [Column("penalty")]
        [StringLength(1)]
        public string Penalty { get; set; }

        [ForeignKey(nameof(CdCotista))]
        [InverseProperty(nameof(TblCliente.TblOrdemPassivo))]
        public virtual TblCliente CdCotistaNavigation { get; set; }
        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblOrdemPassivo))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblOrdemPassivo))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
