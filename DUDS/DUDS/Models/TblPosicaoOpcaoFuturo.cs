using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_opcao_futuro")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_posicao_opcao_futuro")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_posicao_opcao_futuro")]
    public partial class TblPosicaoOpcaoFuturo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Required]
        [Column("ativo")]
        [StringLength(20)]
        public string Ativo { get; set; }
        [Required]
        [Column("vencimento")]
        [StringLength(10)]
        public string Vencimento { get; set; }
        [Required]
        [Column("tipo")]
        [StringLength(5)]
        public string Tipo { get; set; }
        [Required]
        [Column("papel")]
        [StringLength(50)]
        public string Papel { get; set; }
        [Required]
        [Column("corretora")]
        [StringLength(30)]
        public string Corretora { get; set; }
        [Column("preco_exercicio", TypeName = "decimal(22, 10)")]
        public decimal PrecoExercicio { get; set; }
        [Column("data_vecto", TypeName = "date")]
        public DateTime DataVecto { get; set; }
        [Column("qtde", TypeName = "decimal(22, 10)")]
        public decimal Qtde { get; set; }
        [Column("custo_corretagem", TypeName = "decimal(22, 10)")]
        public decimal CustoCorretagem { get; set; }
        [Column("cotacao", TypeName = "decimal(22, 10)")]
        public decimal Cotacao { get; set; }
        [Column("custo_total", TypeName = "decimal(22, 10)")]
        public decimal CustoTotal { get; set; }
        [Column("resultado", TypeName = "decimal(22, 10)")]
        public decimal Resultado { get; set; }
        [Column("valor_mercado", TypeName = "decimal(22, 10)")]
        public decimal ValorMercado { get; set; }
        [Column("perc_sobre_ativo", TypeName = "decimal(22, 10)")]
        public decimal PercSobreAtivo { get; set; }
        [Column("perc_sobre_total", TypeName = "decimal(22, 10)")]
        public decimal PercSobreTotal { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPosicaoOpcaoFuturo))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
