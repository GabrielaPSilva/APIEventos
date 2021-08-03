using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_xmlAnbima_futuros")]
    public partial class TblXmlAnbimaFuturos
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
        [Column("ativo")]
        [StringLength(15)]
        public string Ativo { get; set; }
        [Column("cnpjcorretora")]
        [StringLength(14)]
        public string Cnpjcorretora { get; set; }
        [Column("serie")]
        [StringLength(15)]
        public string Serie { get; set; }
        [Column("quantidade")]
        public int? Quantidade { get; set; }
        [Column("vltotalpos", TypeName = "decimal(22, 10)")]
        public decimal? Vltotalpos { get; set; }
        [Column("tributos")]
        public int? Tributos { get; set; }
        [Column("dtvencimento")]
        [StringLength(8)]
        public string Dtvencimento { get; set; }
        [Column("vlajuste", TypeName = "decimal(22, 10)")]
        public decimal? Vlajuste { get; set; }
        [Column("classeoperacao")]
        [StringLength(5)]
        public string Classeoperacao { get; set; }
        [Column("hedge")]
        [StringLength(5)]
        public string Hedge { get; set; }
        [Column("tphedge")]
        public int? Tphedge { get; set; }
        [Column("fundo_Id")]
        public int? FundoId { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("data_import", TypeName = "datetime")]
        public DateTime? DataImport { get; set; }
    }
}
