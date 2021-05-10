using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_cpr")]
    [Index(nameof(CodFundo), Name = "cod_fundo_Table_1")]
    [Index(nameof(DataRef), Name = "data_ref_Table_1")]
    public partial class TblPosicaoCpr
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Required]
        [Column("operacao")]
        [StringLength(50)]
        public string Operacao { get; set; }
        [Required]
        [Column("descricao")]
        [StringLength(150)]
        public string Descricao { get; set; }
        [Column("valor", TypeName = "decimal(22, 10)")]
        public decimal Valor { get; set; }
        [Column("perc_sobre_ativo", TypeName = "decimal(22, 10)")]
        public decimal PercSobreAtivo { get; set; }
        [Column("perc_sobre_total", TypeName = "decimal(22, 10)")]
        public decimal PercSobreTotal { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPosicaoCpr))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
