using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_futuro")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_posicao_futuro")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_posicao_futuro")]
    public partial class TblPosicaoFuturo
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
        [Key]
        [Column("vencimento")]
        [StringLength(10)]
        public string Vencimento { get; set; }
        [Key]
        [Column("corretora")]
        [StringLength(30)]
        public string Corretora { get; set; }
        [Column("qtde", TypeName = "decimal(22, 10)")]
        public decimal Qtde { get; set; }
        [Column("ajuste_equalizacao", TypeName = "decimal(22, 10)")]
        public decimal AjusteEqualizacao { get; set; }
        [Column("ajuste_valorizacao", TypeName = "decimal(22, 10)")]
        public decimal AjusteValorizacao { get; set; }
        [Column("preco_mercado", TypeName = "decimal(22, 10)")]
        public decimal PrecoMercado { get; set; }
        [Column("valor_mercado", TypeName = "decimal(22, 10)")]
        public decimal ValorMercado { get; set; }
        [Column("perc_sobre_ativo", TypeName = "decimal(22, 10)")]
        public decimal PercSobreAtivo { get; set; }
        [Column("perc_sobre_total", TypeName = "decimal(22, 10)")]
        public decimal PercSobreTotal { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPosicaoFuturo))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
