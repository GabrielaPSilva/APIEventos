using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_fundo")]
    [Index(nameof(CdFundoAdm), Name = "cd_fundo_and_tbl_fundo")]
    [Index(nameof(CdFundoAdm), Name = "cnpj_tbl_fundo")]
    [Index(nameof(Mnemonico), Name = "mnemonico_tbl_fundo", IsUnique = true)]
    [Index(nameof(NomeReduzido), Name = "nome_reduzido_distr_tbl_fundo", IsUnique = true)]
    public partial class TblFundo
    {
        public TblFundo()
        {
            InverseMaster = new HashSet<TblFundo>();
            TblCondicaoRemuneracao = new HashSet<TblCondicaoRemuneracao>();
            TblContas = new HashSet<TblContas>();
            TblContratoFundo = new HashSet<TblContratoFundo>();
            TblErrosPagamento = new HashSet<TblErrosPagamento>();
            TblOrdemPassivo = new HashSet<TblOrdemPassivo>();
            TblPagamentoServico = new HashSet<TblPagamentoServico>();
            TblPgtoAdmPfee = new HashSet<TblPgtoAdmPfee>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nome_reduzido")]
        [StringLength(70)]
        public string NomeReduzido { get; set; }
        [Required]
        [Column("nome_fundo")]
        [StringLength(150)]
        public string NomeFundo { get; set; }
        [Column("mnemonico")]
        [StringLength(30)]
        public string Mnemonico { get; set; }
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Column("performance_fee")]
        public float? PerformanceFee { get; set; }
        [Column("adm_fee")]
        public float? AdmFee { get; set; }
        [Required]
        [Column("tipo_fundo")]
        [StringLength(6)]
        public string TipoFundo { get; set; }
        [Column("classificacao_anbima")]
        [StringLength(40)]
        public string ClassificacaoAnbima { get; set; }
        [Column("classificacao_cvm")]
        [StringLength(30)]
        public string ClassificacaoCvm { get; set; }
        [Column("master_id")]
        public int? MasterId { get; set; }
        [Column("data_encerramento", TypeName = "date")]
        public DateTime? DataEncerramento { get; set; }
        [Column("data_cota_inicial", TypeName = "date")]
        public DateTime? DataCotaInicial { get; set; }
        [Column("valor_cota_inicial")]
        public float? ValorCotaInicial { get; set; }
        [Column("cod_anbima")]
        [StringLength(10)]
        public string CodAnbima { get; set; }
        [Column("cod_cvm")]
        [StringLength(10)]
        public string CodCvm { get; set; }
        [Column("tipo_cota")]
        [StringLength(1)]
        public string TipoCota { get; set; }
        [Column("cod_administrador")]
        public int CodAdministrador { get; set; }
        [Column("cod_custodiante")]
        public int CodCustodiante { get; set; }
        [Column("cod_gestor")]
        public int? CodGestor { get; set; }
        [Column("ativo_cetip")]
        [StringLength(15)]
        public string AtivoCetip { get; set; }
        [Column("isin")]
        [StringLength(12)]
        public string Isin { get; set; }
        [Column("numero_giin")]
        [StringLength(20)]
        public string NumeroGiin { get; set; }
        [Column("moeda_fundo")]
        [StringLength(3)]
        public string MoedaFundo { get; set; }
        [Column("cd_fundo_adm")]
        public int? CdFundoAdm { get; set; }
        [Column("estrategia")]
        [StringLength(10)]
        public string Estrategia { get; set; }
        [Column("dias_cotizacao_aplicacao")]
        public int? DiasCotizacaoAplicacao { get; set; }
        [Column("contagem_dias_cotizacao_aplicacao")]
        [StringLength(2)]
        public string ContagemDiasCotizacaoAplicacao { get; set; }
        [Column("dias_cotizacao_resgate")]
        public int? DiasCotizacaoResgate { get; set; }
        [Column("contagem_dias_cotizacao_resgate")]
        [StringLength(2)]
        public string ContagemDiasCotizacaoResgate { get; set; }
        [Column("dias_liquidacao_aplicacao")]
        public int? DiasLiquidacaoAplicacao { get; set; }
        [Column("contagem_dias_liquidacao_aplicacao")]
        [StringLength(2)]
        public string ContagemDiasLiquidacaoAplicacao { get; set; }
        [Column("dias_liquidacao_resgate")]
        public int? DiasLiquidacaoResgate { get; set; }
        [Column("contagem_dias_liquidacao_resgate")]
        [StringLength(2)]
        public string ContagemDiasLiquidacaoResgate { get; set; }
        [Column("data_modificacao", TypeName = "smalldatetime")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }

        [ForeignKey(nameof(CodAdministrador))]
        [InverseProperty(nameof(TblAdministrador.TblFundo))]
        public virtual TblAdministrador CodAdministradorNavigation { get; set; }
        [ForeignKey(nameof(CodCustodiante))]
        [InverseProperty(nameof(TblCustodiante.TblFundo))]
        public virtual TblCustodiante CodCustodianteNavigation { get; set; }
        [ForeignKey(nameof(CodGestor))]
        [InverseProperty(nameof(TblGestor.TblFundo))]
        public virtual TblGestor CodGestorNavigation { get; set; }
        [ForeignKey(nameof(MasterId))]
        [InverseProperty(nameof(TblFundo.InverseMaster))]
        public virtual TblFundo Master { get; set; }
        [InverseProperty(nameof(TblFundo.Master))]
        public virtual ICollection<TblFundo> InverseMaster { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblCondicaoRemuneracao> TblCondicaoRemuneracao { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblContas> TblContas { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblContratoFundo> TblContratoFundo { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblErrosPagamento> TblErrosPagamento { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblOrdemPassivo> TblOrdemPassivo { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPagamentoServico> TblPagamentoServico { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPgtoAdmPfee> TblPgtoAdmPfee { get; set; }
    }
}
