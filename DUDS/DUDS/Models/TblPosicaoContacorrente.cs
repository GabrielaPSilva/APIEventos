using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_contacorrente")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_posicao_contacorrente")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_posicao_contacorrente")]
    public partial class TblPosicaoContacorrente
    {
        [Key]
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Key]
        [Column("codigo")]
        [StringLength(10)]
        public string Codigo { get; set; }
        [Required]
        [Column("descricao")]
        [StringLength(50)]
        public string Descricao { get; set; }
        [Required]
        [Column("instituicao")]
        [StringLength(50)]
        public string Instituicao { get; set; }
        [Column("valor", TypeName = "decimal(22, 10)")]
        public decimal Valor { get; set; }
        [Column("valor_origem", TypeName = "decimal(22, 10)")]
        public decimal ValorOrigem { get; set; }
        [Required]
        [Column("moeda_origem")]
        [StringLength(10)]
        public string MoedaOrigem { get; set; }
        [Column("perc_sobre_ativo", TypeName = "decimal(22, 10)")]
        public decimal PercSobreAtivo { get; set; }
        [Column("perc_sobre_total", TypeName = "decimal(22, 10)")]
        public decimal PercSobreTotal { get; set; }
    }
}
