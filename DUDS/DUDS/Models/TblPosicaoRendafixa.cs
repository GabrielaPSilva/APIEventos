using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_rendafixa")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_posicao_rendafixa")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_posicao_rendafixa")]
    public partial class TblPosicaoRendafixa
    {
        [Key]
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Required]
        [Column("papel")]
        [StringLength(20)]
        public string Papel { get; set; }
        [Key]
        [Column("codigo")]
        [StringLength(20)]
        public string Codigo { get; set; }
        [Column("data_compra", TypeName = "date")]
        public DateTime DataCompra { get; set; }
        [Required]
        [Column("emitente")]
        [StringLength(15)]
        public string Emitente { get; set; }
        [Required]
        [Column("ativo")]
        [StringLength(15)]
        public string Ativo { get; set; }
        [Column("mtm_aa", TypeName = "decimal(22, 10)")]
        public decimal MtmAa { get; set; }
        [Column("taxa_over", TypeName = "decimal(22, 10)")]
        public decimal TaxaOver { get; set; }
        [Column("taxa_aa", TypeName = "decimal(22, 10)")]
        public decimal TaxaAa { get; set; }
        [Required]
        [Column("indexador")]
        [StringLength(15)]
        public string Indexador { get; set; }
        [Column("data_emissao", TypeName = "date")]
        public DateTime? DataEmissao { get; set; }
        [Column("data_vcto", TypeName = "date")]
        public DateTime DataVcto { get; set; }
        [Column("qtde", TypeName = "decimal(22, 10)")]
        public decimal Qtde { get; set; }
        [Column("pu", TypeName = "decimal(22, 10)")]
        public decimal Pu { get; set; }
        [Column("valor_aplicacao", TypeName = "decimal(22, 10)")]
        public decimal ValorAplicacao { get; set; }
        [Column("valor_resgate", TypeName = "decimal(22, 10)")]
        public decimal ValorResgate { get; set; }
        [Column("valor_bruto", TypeName = "decimal(22, 10)")]
        public decimal ValorBruto { get; set; }
        [Column("imposto", TypeName = "decimal(22, 10)")]
        public decimal Imposto { get; set; }
        [Column("valor_liquido", TypeName = "decimal(22, 10)")]
        public decimal ValorLiquido { get; set; }
        [Column("perc_sobre_ativo", TypeName = "decimal(22, 10)")]
        public decimal PercSobreAtivo { get; set; }
        [Column("perc_sobre_total", TypeName = "decimal(22, 10)")]
        public decimal PercSobreTotal { get; set; }
    }
}
