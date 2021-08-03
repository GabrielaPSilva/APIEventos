using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_xmlAnbima_corretagem")]
    public partial class TblXmlAnbimaCorretagem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_ref", TypeName = "datetime")]
        public DateTime? DataRef { get; set; }
        [Column("cod_fundo")]
        public int? CodFundo { get; set; }
        [Column("cnpjcorretora")]
        [StringLength(14)]
        public string Cnpjcorretora { get; set; }
        [Column("tpcorretora")]
        public int? Tpcorretora { get; set; }
        [Column("numope")]
        public int? Numope { get; set; }
        [Column("vlbov", TypeName = "decimal(22, 10)")]
        public decimal? Vlbov { get; set; }
        [Column("vlrepassebov", TypeName = "decimal(22, 10)")]
        public decimal? Vlrepassebov { get; set; }
        [Column("vlbmf", TypeName = "decimal(22, 10)")]
        public decimal? Vlbmf { get; set; }
        [Column("vlrepassebmf", TypeName = "decimal(22, 10)")]
        public decimal? Vlrepassebmf { get; set; }
        [Column("vloutbolsas", TypeName = "decimal(22, 10)")]
        public decimal? Vloutbolsas { get; set; }
        [Column("vlrepasseoutbol", TypeName = "decimal(22, 10)")]
        public decimal? Vlrepasseoutbol { get; set; }
        [Column("fundo_Id")]
        public int? FundoId { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("data_import", TypeName = "datetime")]
        public DateTime? DataImport { get; set; }
    }
}
