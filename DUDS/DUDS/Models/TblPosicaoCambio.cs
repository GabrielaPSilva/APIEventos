using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_cambio")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_posicao_cambio")]
    [Index(nameof(DataRef), Name = "date_ref_tbl_posicao_cambio")]
    public partial class TblPosicaoCambio
    {
        [Key]
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Required]
        [Column("descricao")]
        [StringLength(20)]
        public string Descricao { get; set; }
        [Key]
        [Column("ativo")]
        [StringLength(20)]
        public string Ativo { get; set; }
        [Required]
        [Column("tipo")]
        [StringLength(20)]
        public string Tipo { get; set; }
        [Column("vencimento", TypeName = "date")]
        public DateTime Vencimento { get; set; }
        [Required]
        [Column("moeda_vendida")]
        [StringLength(3)]
        public string MoedaVendida { get; set; }
        [Required]
        [Column("moeda_comprada")]
        [StringLength(3)]
        public string MoedaComprada { get; set; }
        [Column("valor_moeda_vendida", TypeName = "decimal(22, 10)")]
        public decimal ValorMoedaVendida { get; set; }
        [Column("valor_moeda_comprada", TypeName = "decimal(22, 10)")]
        public decimal ValorMoedaComprada { get; set; }
        [Column("paridade_contratacao", TypeName = "decimal(22, 10)")]
        public decimal ParidadeContratacao { get; set; }
        [Column("paridade_atual", TypeName = "decimal(22, 10)")]
        public decimal ParidadeAtual { get; set; }
        [Column("valor", TypeName = "decimal(22, 10)")]
        public decimal Valor { get; set; }
        [Column("valor_ajuste", TypeName = "decimal(22, 10)")]
        public decimal ValorAjuste { get; set; }
        [Column("perc_sobre_ativo", TypeName = "decimal(22, 10)")]
        public decimal PercSobreAtivo { get; set; }
        [Column("perc_sobre_total", TypeName = "decimal(22, 10)")]
        public decimal PercSobreTotal { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPosicaoCambio))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
