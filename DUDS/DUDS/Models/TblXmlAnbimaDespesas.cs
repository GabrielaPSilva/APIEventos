using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_xmlAnbima_despesas")]
    public partial class TblXmlAnbimaDespesas
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_ref", TypeName = "datetime")]
        public DateTime? DataRef { get; set; }
        [Column("cod_fundo")]
        public int? CodFundo { get; set; }
        [Column("txadm", TypeName = "decimal(22, 10)")]
        public decimal? Txadm { get; set; }
        [Column("tributos", TypeName = "decimal(22, 10)")]
        public decimal? Tributos { get; set; }
        [Column("perctaxaadm", TypeName = "decimal(22, 10)")]
        public decimal? Perctaxaadm { get; set; }
        [Column("txperf")]
        [StringLength(5)]
        public string Txperf { get; set; }
        [Column("vltxperf", TypeName = "decimal(22, 10)")]
        public decimal? Vltxperf { get; set; }
        [Column("perctxperf")]
        public int? Perctxperf { get; set; }
        [Column("percindex", TypeName = "decimal(22, 10)")]
        public decimal? Percindex { get; set; }
        [Column("outtax", TypeName = "decimal(22, 10)")]
        public decimal? Outtax { get; set; }
        [Column("indexador")]
        [StringLength(15)]
        public string Indexador { get; set; }
        [Column("fundo_Id")]
        public int? FundoId { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("data_import", TypeName = "datetime")]
        public DateTime? DataImport { get; set; }
    }
}
