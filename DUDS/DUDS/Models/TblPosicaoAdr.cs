using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_adr")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_posicao_adr")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_posicao_adr")]
    public partial class TblPosicaoAdr
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
        [Column("papel")]
        [StringLength(50)]
        public string Papel { get; set; }
        [Column("qtde_disponivel", TypeName = "decimal(22, 10)")]
        public decimal QtdeDisponivel { get; set; }
        [Column("qtde_bloqueada", TypeName = "decimal(22, 10)")]
        public decimal QtdeBloqueada { get; set; }
        [Column("qtde_total", TypeName = "decimal(22, 10)")]
        public decimal QtdeTotal { get; set; }
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

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPosicaoAdr))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
