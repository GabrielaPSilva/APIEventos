using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_xmlAnbima_header")]
    public partial class TblXmlAnbimaHeader
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
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Column("nome")]
        [StringLength(100)]
        public string Nome { get; set; }
        [Column("dtposicao")]
        [StringLength(8)]
        public string Dtposicao { get; set; }
        [Column("nomeadm")]
        [StringLength(60)]
        public string Nomeadm { get; set; }
        [Column("cnpjadm")]
        [StringLength(14)]
        public string Cnpjadm { get; set; }
        [Column("nomegestor")]
        [StringLength(60)]
        public string Nomegestor { get; set; }
        [Column("cnpjgestor")]
        [StringLength(14)]
        public string Cnpjgestor { get; set; }
        [Column("nomecustodiante")]
        [StringLength(60)]
        public string Nomecustodiante { get; set; }
        [Column("cnpjcustodiante")]
        [StringLength(14)]
        public string Cnpjcustodiante { get; set; }
        [Column("valorcota", TypeName = "decimal(22, 10)")]
        public decimal? Valorcota { get; set; }
        [Column("quantidade", TypeName = "decimal(22, 10)")]
        public decimal? Quantidade { get; set; }
        [Column("patliq", TypeName = "decimal(22, 10)")]
        public decimal? Patliq { get; set; }
        [Column("valorativos", TypeName = "decimal(22, 10)")]
        public decimal? Valorativos { get; set; }
        [Column("valorreceber", TypeName = "decimal(22, 10)")]
        public decimal? Valorreceber { get; set; }
        [Column("valorpagar", TypeName = "decimal(22, 10)")]
        public decimal? Valorpagar { get; set; }
        [Column("vlcotasemitir", TypeName = "decimal(22, 10)")]
        public decimal? Vlcotasemitir { get; set; }
        [Column("vlcotasresgatar", TypeName = "decimal(22, 10)")]
        public decimal? Vlcotasresgatar { get; set; }
        [Column("codanbid")]
        [StringLength(6)]
        public string Codanbid { get; set; }
        [Column("tipofundo")]
        [StringLength(50)]
        public string Tipofundo { get; set; }
        [Column("nivelrsc")]
        [StringLength(4)]
        public string Nivelrsc { get; set; }
        [Column("fundo_Id")]
        public int? FundoId { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("data_import", TypeName = "datetime")]
        public DateTime? DataImport { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblXmlAnbimaHeader))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
