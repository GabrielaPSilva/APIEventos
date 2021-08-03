using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_opcao_acao")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_posicao_opcao_acao")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_posicao_opcao_acao")]
    public partial class TblPosicaoOpcaoAcao
    {
        [Key]
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Key]
        [Column("ativo")]
        [StringLength(20)]
        public string Ativo { get; set; }
        [Required]
        [Column("papel")]
        [StringLength(50)]
        public string Papel { get; set; }
        [Key]
        [Column("tipo")]
        [StringLength(5)]
        public string Tipo { get; set; }
        [Key]
        [Column("corretora")]
        [StringLength(30)]
        public string Corretora { get; set; }
        [Required]
        [Column("praca")]
        [StringLength(2)]
        public string Praca { get; set; }
        [Column("preco_exercicio", TypeName = "decimal(22, 10)")]
        public decimal PrecoExercicio { get; set; }
        [Column("data_vcto", TypeName = "date")]
        public DateTime DataVcto { get; set; }
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
        [Column("irrf", TypeName = "decimal(22, 10)")]
        public decimal Irrf { get; set; }
        [Column("valor_mercado_liquido", TypeName = "decimal(22, 10)")]
        public decimal ValorMercadoLiquido { get; set; }
        [Column("perc_sobre_ativo", TypeName = "decimal(22, 10)")]
        public decimal PercSobreAtivo { get; set; }
        [Column("perc_sobre_total", TypeName = "decimal(22, 10)")]
        public decimal PercSobreTotal { get; set; }
    }
}
