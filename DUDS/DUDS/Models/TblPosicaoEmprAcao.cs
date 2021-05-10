using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_empr_acao")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_posicao_empr_acao")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_posicao_empr_acao")]
    public partial class TblPosicaoEmprAcao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Required]
        [Column("tipo_operacao")]
        [StringLength(20)]
        public string TipoOperacao { get; set; }
        [Required]
        [Column("cod_operacao")]
        [StringLength(10)]
        public string CodOperacao { get; set; }
        [Column("data_operacao", TypeName = "date")]
        public DateTime DataOperacao { get; set; }
        [Column("data_vcto", TypeName = "date")]
        public DateTime DataVcto { get; set; }
        [Required]
        [Column("ativo")]
        [StringLength(10)]
        public string Ativo { get; set; }
        [Column("papel")]
        [StringLength(50)]
        public string Papel { get; set; }
        [Column("qtde", TypeName = "decimal(22, 10)")]
        public decimal Qtde { get; set; }
        [Column("preco", TypeName = "decimal(22, 10)")]
        public decimal Preco { get; set; }
        [Column("financeiro", TypeName = "decimal(22, 10)")]
        public decimal Financeiro { get; set; }
        [Column("perc_sobre_remuneracao", TypeName = "decimal(22, 10)")]
        public decimal PercSobreRemuneracao { get; set; }
        [Column("perc_sobre_mercado", TypeName = "decimal(22, 10)")]
        public decimal PercSobreMercado { get; set; }
        [Column("remuneracao", TypeName = "decimal(22, 10)")]
        public decimal Remuneracao { get; set; }
        [Column("taxas", TypeName = "decimal(22, 10)")]
        public decimal Taxas { get; set; }
        [Column("total", TypeName = "decimal(22, 10)")]
        public decimal Total { get; set; }
        [Column("perc_sobre_ativo", TypeName = "decimal(22, 10)")]
        public decimal PercSobreAtivo { get; set; }
        [Column("perc_sobre_total", TypeName = "decimal(22, 10)")]
        public decimal PercSobreTotal { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPosicaoEmprAcao))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
