using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_xmlAnbima_opcoesacoes")]
    public partial class TblXmlAnbimaOpcoesacoes
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_ref", TypeName = "datetime")]
        public DateTime? DataRef { get; set; }
        [Column("cod_fundo")]
        public int? CodFundo { get; set; }
        [Column("isin")]
        [StringLength(12)]
        public string Isin { get; set; }
        [Column("cusip")]
        [StringLength(12)]
        public string Cusip { get; set; }
        [Column("codativo")]
        [StringLength(18)]
        public string Codativo { get; set; }
        [Column("ativobase")]
        [StringLength(30)]
        public string Ativobase { get; set; }
        [Column("qtdisponivel", TypeName = "decimal(22, 10)")]
        public decimal? Qtdisponivel { get; set; }
        [Column("valorfinanceiro", TypeName = "decimal(22, 10)")]
        public decimal? Valorfinanceiro { get; set; }
        [Column("precoexercicio", TypeName = "decimal(22, 10)")]
        public decimal? Precoexercicio { get; set; }
        [Column("dtvencimento")]
        [StringLength(8)]
        public string Dtvencimento { get; set; }
        [Column("classeoperacao")]
        [StringLength(1)]
        public string Classeoperacao { get; set; }
        [Column("tributos", TypeName = "decimal(22, 10)")]
        public decimal? Tributos { get; set; }
        [Column("puposicao", TypeName = "decimal(22, 10)")]
        public decimal? Puposicao { get; set; }
        [Column("premio", TypeName = "decimal(22, 10)")]
        public decimal? Premio { get; set; }
        [Column("percprovcred", TypeName = "decimal(22, 10)")]
        public decimal? Percprovcred { get; set; }
        [Column("tpconta")]
        public int? Tpconta { get; set; }
        [Column("hedge")]
        [StringLength(2)]
        public string Hedge { get; set; }
        [Column("tphedge")]
        public int? Tphedge { get; set; }
        [Column("fundo_Id")]
        public int? FundoId { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("data_import", TypeName = "datetime")]
        public DateTime? DataImport { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblXmlAnbimaOpcoesacoes))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
