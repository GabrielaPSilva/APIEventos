using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_xmlAnbima_forwardsmoedas")]
    public partial class TblXmlAnbimaForwardsmoedas
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_ref", TypeName = "datetime")]
        public DateTime? DataRef { get; set; }
        [Column("cod_fundo")]
        public int? CodFundo { get; set; }
        [Column("tipooperacao")]
        [StringLength(10)]
        public string Tipooperacao { get; set; }
        [Column("moedaativa")]
        [StringLength(10)]
        public string Moedaativa { get; set; }
        [Column("moedapassiva")]
        [StringLength(10)]
        public string Moedapassiva { get; set; }
        [Column("notional", TypeName = "decimal(22, 10)")]
        public decimal? Notional { get; set; }
        [Column("taxa", TypeName = "decimal(18, 0)")]
        public decimal? Taxa { get; set; }
        [Column("valorfinanceiro", TypeName = "decimal(18, 0)")]
        public decimal? Valorfinanceiro { get; set; }
        [Column("tributos")]
        public int? Tributos { get; set; }
        [Column("dtvencimento")]
        [StringLength(8)]
        public string Dtvencimento { get; set; }
        [Column("classeoperacao")]
        [StringLength(5)]
        public string Classeoperacao { get; set; }
        [Column("hedge")]
        [StringLength(5)]
        public string Hedge { get; set; }
        [Column("tphedge")]
        public byte? Tphedge { get; set; }
        [Column("fundo_Id")]
        public int? FundoId { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("data_import", TypeName = "datetime")]
        public DateTime? DataImport { get; set; }
    }
}
