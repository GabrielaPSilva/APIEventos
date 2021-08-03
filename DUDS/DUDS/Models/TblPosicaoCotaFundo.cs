using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_cota_fundo")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_posicao_cota_fundo")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_posicao_cota_fundo")]
    public partial class TblPosicaoCotaFundo
    {
        [Key]
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Key]
        [Column("codigo_ativo")]
        [StringLength(20)]
        public string CodigoAtivo { get; set; }
        [Required]
        [Column("nome_ativo")]
        [StringLength(50)]
        public string NomeAtivo { get; set; }
        [Required]
        [Column("instituicao")]
        [StringLength(30)]
        public string Instituicao { get; set; }
        [Column("qtde_cota", TypeName = "decimal(22, 10)")]
        public decimal QtdeCota { get; set; }
        [Column("qtde_bloqueada", TypeName = "decimal(22, 10)")]
        public decimal QtdeBloqueada { get; set; }
        [Column("valor_cota", TypeName = "decimal(22, 10)")]
        public decimal ValorCota { get; set; }
        [Column("aplic_resg", TypeName = "decimal(22, 10)")]
        public decimal AplicResg { get; set; }
        [Column("valor_atual", TypeName = "decimal(22, 10)")]
        public decimal ValorAtual { get; set; }
        [Column("imposto", TypeName = "decimal(22, 10)")]
        public decimal Imposto { get; set; }
        [Column("valor_liquido", TypeName = "decimal(22, 10)")]
        public decimal ValorLiquido { get; set; }
        [Column("perc_sobre_ativo", TypeName = "decimal(22, 10)")]
        public decimal PercSobreAtivo { get; set; }
        [Column("perc_sobre_total", TypeName = "decimal(22, 10)")]
        public decimal PercSobreTotal { get; set; }
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Column("segmento")]
        [StringLength(30)]
        public string Segmento { get; set; }
    }
}
