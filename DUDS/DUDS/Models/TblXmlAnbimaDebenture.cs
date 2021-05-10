using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_xmlAnbima_debenture")]
    public partial class TblXmlAnbimaDebenture
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
        [Column("coddeb")]
        [StringLength(15)]
        public string Coddeb { get; set; }
        [Column("debconv")]
        [StringLength(5)]
        public string Debconv { get; set; }
        [Column("debpartlucro")]
        [StringLength(5)]
        public string Debpartlucro { get; set; }
        [Column("SPE")]
        [StringLength(5)]
        public string Spe { get; set; }
        [Column("cusip")]
        [StringLength(15)]
        public string Cusip { get; set; }
        [Column("dtemissao")]
        [StringLength(8)]
        public string Dtemissao { get; set; }
        [Column("dtoperacao")]
        [StringLength(8)]
        public string Dtoperacao { get; set; }
        [Column("dtvencimento")]
        [StringLength(8)]
        public string Dtvencimento { get; set; }
        [Column("cnpjemissor")]
        [StringLength(14)]
        public string Cnpjemissor { get; set; }
        [Column("qtdisponivel")]
        public int? Qtdisponivel { get; set; }
        [Column("qtgarantia")]
        public int? Qtgarantia { get; set; }
        [Column("depgar")]
        public int? Depgar { get; set; }
        [Column("pucompra", TypeName = "decimal(22, 10)")]
        public decimal? Pucompra { get; set; }
        [Column("puvencimento")]
        public int? Puvencimento { get; set; }
        [Column("puposicao", TypeName = "decimal(22, 10)")]
        public decimal? Puposicao { get; set; }
        [Column("puemissao", TypeName = "decimal(22, 10)")]
        public decimal? Puemissao { get; set; }
        [Column("principal", TypeName = "decimal(22, 10)")]
        public decimal? Principal { get; set; }
        [Column("tributos")]
        public int? Tributos { get; set; }
        [Column("valorfindisp", TypeName = "decimal(22, 10)")]
        public decimal? Valorfindisp { get; set; }
        [Column("valorfinemgar")]
        public int? Valorfinemgar { get; set; }
        [Column("coupom", TypeName = "decimal(22, 10)")]
        public decimal? Coupom { get; set; }
        [Column("indexador")]
        [StringLength(15)]
        public string Indexador { get; set; }
        [Column("percindex")]
        public int? Percindex { get; set; }
        [Column("caracteristica")]
        [StringLength(30)]
        public string Caracteristica { get; set; }
        [Column("percprovcred")]
        public int? Percprovcred { get; set; }
        [Column("classeoperacao")]
        [StringLength(5)]
        public string Classeoperacao { get; set; }
        [Column("idinternoativo")]
        [StringLength(20)]
        public string Idinternoativo { get; set; }
        [Column("nivelrsc")]
        [StringLength(15)]
        public string Nivelrsc { get; set; }
        [Column("fundo_Id")]
        public int? FundoId { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("data_import", TypeName = "datetime")]
        public DateTime? DataImport { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblXmlAnbimaDebenture))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
