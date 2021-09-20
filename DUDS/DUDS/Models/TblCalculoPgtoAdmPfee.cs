using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_calculo_pgto_adm_pfee")]
    [Index(nameof(CodCondicaoRemuneracao), nameof(CodContrato), nameof(CodContratoFundo), nameof(CodContratoRemuneracao), nameof(CodFundo), nameof(CodInvestidor), nameof(CodSubContrato), Name = "IX_tbl_calculo_pgto_adm_pfee")]
    [Index(nameof(Competencia), Name = "IX_tbl_calculo_pgto_adm_pfee_1")]
    public partial class TblCalculoPgtoAdmPfee
    {
        [Key]
        [Column("competencia")]
        [StringLength(7)]
        public string Competencia { get; set; }
        [Key]
        [Column("cod_investidor")]
        public int CodInvestidor { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Column("cod_contrato")]
        public int CodContrato { get; set; }
        [Column("cod_sub_contrato")]
        public int CodSubContrato { get; set; }
        [Column("cod_contrato_fundo")]
        public int CodContratoFundo { get; set; }
        [Column("cod_contrato_remuneracao")]
        public int CodContratoRemuneracao { get; set; }
        [Column("cod_condicao_remuneracao")]
        public int? CodCondicaoRemuneracao { get; set; }
        [Column("cod_administrador")]
        public int CodAdministrador { get; set; }
        [Column("valor_adm", TypeName = "decimal(22, 10)")]
        public decimal ValorAdm { get; set; }
        [Column("valor_pfee_resgate", TypeName = "decimal(22, 10)")]
        public decimal ValorPfeeResgate { get; set; }
        [Column("valor_pfee_sementre", TypeName = "decimal(22, 10)")]
        public decimal ValorPfeeSementre { get; set; }
        [Column("rebate_adm", TypeName = "decimal(22, 10)")]
        public decimal RebateAdm { get; set; }
        [Column("rebate_pfee_resgate", TypeName = "decimal(22, 10)")]
        public decimal RebatePfeeResgate { get; set; }
        [Column("rebate_pfee_sementre", TypeName = "decimal(22, 10)")]
        public decimal RebatePfeeSementre { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }
        [Column("ativo")]
        public int Ativo { get; set; }

        [ForeignKey(nameof(CodAdministrador))]
        [InverseProperty(nameof(TblAdministrador.TblCalculoPgtoAdmPfee))]
        public virtual TblAdministrador CodAdministradorNavigation { get; set; }
        [ForeignKey(nameof(CodCondicaoRemuneracao))]
        [InverseProperty(nameof(TblCondicaoRemuneracao.TblCalculoPgtoAdmPfee))]
        public virtual TblCondicaoRemuneracao CodCondicaoRemuneracaoNavigation { get; set; }
        [ForeignKey(nameof(CodContratoFundo))]
        [InverseProperty(nameof(TblContratoFundo.TblCalculoPgtoAdmPfee))]
        public virtual TblContratoFundo CodContratoFundoNavigation { get; set; }
        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(TblContrato.TblCalculoPgtoAdmPfee))]
        public virtual TblContrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodContratoRemuneracao))]
        [InverseProperty(nameof(TblContratoRemuneracao.TblCalculoPgtoAdmPfee))]
        public virtual TblContratoRemuneracao CodContratoRemuneracaoNavigation { get; set; }
        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblCalculoPgtoAdmPfee))]
        public virtual TblFundo CodFundoNavigation { get; set; }
        [ForeignKey(nameof(CodInvestidor))]
        [InverseProperty(nameof(TblInvestidor.TblCalculoPgtoAdmPfee))]
        public virtual TblInvestidor CodInvestidorNavigation { get; set; }
        [ForeignKey(nameof(CodSubContrato))]
        [InverseProperty(nameof(TblSubContrato.TblCalculoPgtoAdmPfee))]
        public virtual TblSubContrato CodSubContratoNavigation { get; set; }
    }
}
