using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_xmlAnbima_cotas")]
    public partial class TblXmlAnbimaCotas
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
        [Column("cnpjfundo")]
        [StringLength(14)]
        public string Cnpjfundo { get; set; }
        [Column("qtdisponivel", TypeName = "decimal(22, 10)")]
        public decimal? Qtdisponivel { get; set; }
        [Column("qtgarantia", TypeName = "decimal(22, 10)")]
        public decimal? Qtgarantia { get; set; }
        [Column("puposicao", TypeName = "decimal(22, 10)")]
        public decimal? Puposicao { get; set; }
        [Column("tributos", TypeName = "decimal(22, 10)")]
        public decimal? Tributos { get; set; }
        [Column("nivelrsc")]
        [StringLength(30)]
        public string Nivelrsc { get; set; }
        [Column("fundo_Id")]
        public int? FundoId { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("data_import", TypeName = "date")]
        public DateTime? DataImport { get; set; }
    }
}
