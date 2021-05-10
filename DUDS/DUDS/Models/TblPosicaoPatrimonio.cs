using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_patrimonio")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_patrimonio")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_patrimonio")]
    public partial class TblPosicaoPatrimonio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Column("qtde_cotas", TypeName = "decimal(22, 10)")]
        public decimal QtdeCotas { get; set; }
        [Column("valor_cota_bruta", TypeName = "decimal(22, 10)")]
        public decimal ValorCotaBruta { get; set; }
        [Column("valor_cota_liquida", TypeName = "decimal(22, 10)")]
        public decimal ValorCotaLiquida { get; set; }
        [Column("patrimonio", TypeName = "decimal(22, 10)")]
        public decimal Patrimonio { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPosicaoPatrimonio))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
