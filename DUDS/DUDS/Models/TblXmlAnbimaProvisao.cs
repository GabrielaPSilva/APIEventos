using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_xmlAnbima_provisao")]
    public partial class TblXmlAnbimaProvisao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_ref", TypeName = "datetime")]
        public DateTime? DataRef { get; set; }
        [Column("cod_fundo")]
        public int? CodFundo { get; set; }
        [Column("codprov")]
        public int? Codprov { get; set; }
        [Column("credeb")]
        [StringLength(5)]
        public string Credeb { get; set; }
        [Column("dt")]
        [StringLength(8)]
        public string Dt { get; set; }
        [Column("valor", TypeName = "decimal(22, 10)")]
        public decimal? Valor { get; set; }
        [Column("fundo_Id")]
        public int? FundoId { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("data_import", TypeName = "datetime")]
        public DateTime? DataImport { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblXmlAnbimaProvisao))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
