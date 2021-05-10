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
    [Index(nameof(CodAtivoSma), Name = "cod_ativo_sma_tbl_fundo")]
    [Index(nameof(Mnemonico), Name = "mnemonico_tbl_fundo", IsUnique = true)]
    [Index(nameof(NomeReduzido), Name = "nome_reduzido_distr_tbl_fundo", IsUnique = true)]
    public partial class TblFundo
    {
        public TblFundo()
        {
            InverseMaster = new HashSet<TblFundo>();
            TblContas = new HashSet<TblContas>();
            TblMovimentacaoNota = new HashSet<TblMovimentacaoNota>();
            TblOrdemPassivo = new HashSet<TblOrdemPassivo>();
            TblPosicaoAcao = new HashSet<TblPosicaoAcao>();
            TblPosicaoAdr = new HashSet<TblPosicaoAdr>();
            TblPosicaoBdr = new HashSet<TblPosicaoBdr>();
            TblPosicaoCambio = new HashSet<TblPosicaoCambio>();
            TblPosicaoCliente = new HashSet<TblPosicaoCliente>();
            TblPosicaoContacorrente = new HashSet<TblPosicaoContacorrente>();
            TblPosicaoCotaFundo = new HashSet<TblPosicaoCotaFundo>();
            TblPosicaoCpr = new HashSet<TblPosicaoCpr>();
            TblPosicaoEmprAcao = new HashSet<TblPosicaoEmprAcao>();
            TblPosicaoFuturo = new HashSet<TblPosicaoFuturo>();
            TblPosicaoOpcaoAcao = new HashSet<TblPosicaoOpcaoAcao>();
            TblPosicaoOpcaoFuturo = new HashSet<TblPosicaoOpcaoFuturo>();
            TblPosicaoPatrimonio = new HashSet<TblPosicaoPatrimonio>();
            TblPosicaoRendafixa = new HashSet<TblPosicaoRendafixa>();
            TblPosicaoRentabilidade = new HashSet<TblPosicaoRentabilidade>();
            TblPosicaoTesouraria = new HashSet<TblPosicaoTesouraria>();
            TblXmlAnbimaAcoes = new HashSet<TblXmlAnbimaAcoes>();
            TblXmlAnbimaCaixa = new HashSet<TblXmlAnbimaCaixa>();
            TblXmlAnbimaCorretagem = new HashSet<TblXmlAnbimaCorretagem>();
            TblXmlAnbimaCotas = new HashSet<TblXmlAnbimaCotas>();
            TblXmlAnbimaDebenture = new HashSet<TblXmlAnbimaDebenture>();
            TblXmlAnbimaDespesas = new HashSet<TblXmlAnbimaDespesas>();
            TblXmlAnbimaForwardsmoedas = new HashSet<TblXmlAnbimaForwardsmoedas>();
            TblXmlAnbimaFuturos = new HashSet<TblXmlAnbimaFuturos>();
            TblXmlAnbimaHeader = new HashSet<TblXmlAnbimaHeader>();
            TblXmlAnbimaOpcoesacoes = new HashSet<TblXmlAnbimaOpcoesacoes>();
            TblXmlAnbimaOutrasdespesas = new HashSet<TblXmlAnbimaOutrasdespesas>();
            TblXmlAnbimaProvisao = new HashSet<TblXmlAnbimaProvisao>();
            TblXmlAnbimaTitprivado = new HashSet<TblXmlAnbimaTitprivado>();
            TblXmlAnbimaTitpublico = new HashSet<TblXmlAnbimaTitpublico>();
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
        [Column("data_abertura", TypeName = "date")]
        public DateTime? DataAbertura { get; set; }
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
        [Column("nickname")]
        [StringLength(5)]
        public string Nickname { get; set; }
        [Column("cod_ativo_sma")]
        [StringLength(15)]
        public string CodAtivoSma { get; set; }
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
        [Column("usuaria_modificacao")]
        [StringLength(50)]
        public string UsuariaModificacao { get; set; }

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
        public virtual ICollection<TblContas> TblContas { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblMovimentacaoNota> TblMovimentacaoNota { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblOrdemPassivo> TblOrdemPassivo { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoAcao> TblPosicaoAcao { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoAdr> TblPosicaoAdr { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoBdr> TblPosicaoBdr { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoCambio> TblPosicaoCambio { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoCliente> TblPosicaoCliente { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoContacorrente> TblPosicaoContacorrente { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoCotaFundo> TblPosicaoCotaFundo { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoCpr> TblPosicaoCpr { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoEmprAcao> TblPosicaoEmprAcao { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoFuturo> TblPosicaoFuturo { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoOpcaoAcao> TblPosicaoOpcaoAcao { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoOpcaoFuturo> TblPosicaoOpcaoFuturo { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoPatrimonio> TblPosicaoPatrimonio { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoRendafixa> TblPosicaoRendafixa { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoRentabilidade> TblPosicaoRentabilidade { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblPosicaoTesouraria> TblPosicaoTesouraria { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaAcoes> TblXmlAnbimaAcoes { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaCaixa> TblXmlAnbimaCaixa { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaCorretagem> TblXmlAnbimaCorretagem { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaCotas> TblXmlAnbimaCotas { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaDebenture> TblXmlAnbimaDebenture { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaDespesas> TblXmlAnbimaDespesas { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaForwardsmoedas> TblXmlAnbimaForwardsmoedas { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaFuturos> TblXmlAnbimaFuturos { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaHeader> TblXmlAnbimaHeader { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaOpcoesacoes> TblXmlAnbimaOpcoesacoes { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaOutrasdespesas> TblXmlAnbimaOutrasdespesas { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaProvisao> TblXmlAnbimaProvisao { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaTitprivado> TblXmlAnbimaTitprivado { get; set; }
        [InverseProperty("CodFundoNavigation")]
        public virtual ICollection<TblXmlAnbimaTitpublico> TblXmlAnbimaTitpublico { get; set; }
    }
}
