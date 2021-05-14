using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_tesouraria")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_posicao_tesouraria")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_posicao_tesouraria")]
    public partial class TblPosicaoTesouraria
    {
        [Key]
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Required]
        [Column("descricao")]
        [StringLength(30)]
        public string Descricao { get; set; }
        [Column("valor", TypeName = "decimal(22, 10)")]
        public decimal Valor { get; set; }
        [Column("perc_sobre_ativo", TypeName = "decimal(22, 10)")]
        public decimal PercSobreAtivo { get; set; }
        [Column("perc_sobre_total", TypeName = "decimal(22, 10)")]
        public decimal PercSobreTotal { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPosicaoTesouraria))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
