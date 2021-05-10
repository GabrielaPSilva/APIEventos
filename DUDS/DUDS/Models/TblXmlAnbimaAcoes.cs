using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_xmlAnbima_acoes")]
    public partial class TblXmlAnbimaAcoes
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
        [StringLength(14)]
        public string Cusip { get; set; }
        [Column("codativo")]
        [StringLength(100)]
        public string Codativo { get; set; }
        [Column("qtdisponivel", TypeName = "decimal(22, 10)")]
        public decimal? Qtdisponivel { get; set; }
        [Column("lote")]
        public int? Lote { get; set; }
        [Column("qtgarantia", TypeName = "decimal(22, 10)")]
        public decimal? Qtgarantia { get; set; }
        [Column("valorfindisp", TypeName = "decimal(22, 10)")]
        public decimal? Valorfindisp { get; set; }
        [Column("valorfinemgar", TypeName = "decimal(22, 10)")]
        public decimal? Valorfinemgar { get; set; }
        [Column("tributos", TypeName = "decimal(22, 10)")]
        public decimal? Tributos { get; set; }
        [Column("puposicao", TypeName = "decimal(22, 10)")]
        public decimal? Puposicao { get; set; }
        [Column("percprovcred", TypeName = "decimal(22, 10)")]
        public decimal? Percprovcred { get; set; }
        [Column("tpconta")]
        public int? Tpconta { get; set; }
        [Column("classeoperacao")]
        [StringLength(1)]
        public string Classeoperacao { get; set; }
        [Column("dtvencalug")]
        [StringLength(8)]
        public string Dtvencalug { get; set; }
        [Column("txalug", TypeName = "decimal(22, 10)")]
        public decimal? Txalug { get; set; }
        [Column("cnpjinter")]
        [StringLength(14)]
        public string Cnpjinter { get; set; }
        [Column("fundo_Id")]
        public int? FundoId { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("data_import", TypeName = "datetime")]
        public DateTime? DataImport { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblXmlAnbimaAcoes))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
