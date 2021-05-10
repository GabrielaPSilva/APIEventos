using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_posicao_rentabilidade")]
    [Index(nameof(CodFundo), Name = "cod_fundo_tbl_rentabilidade_fundo")]
    [Index(nameof(DataRef), Name = "data_ref_tbl_rentabilidade_fundo")]
    public partial class TblPosicaoRentabilidade
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_ref", TypeName = "date")]
        public DateTime DataRef { get; set; }
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Required]
        [Column("indexador")]
        [StringLength(20)]
        public string Indexador { get; set; }
        [Column("benchmarck", TypeName = "decimal(22, 10)")]
        public decimal Benchmarck { get; set; }
        [Column("rent_real", TypeName = "decimal(22, 10)")]
        public decimal RentReal { get; set; }
        [Column("variacao_diaria", TypeName = "decimal(22, 10)")]
        public decimal VariacaoDiaria { get; set; }
        [Column("variacao_mensal", TypeName = "decimal(22, 10)")]
        public decimal VariacaoMensal { get; set; }
        [Column("variacao_anual", TypeName = "decimal(22, 10)")]
        public decimal VariacaoAnual { get; set; }
        [Column("variacao_seis_meses", TypeName = "decimal(22, 10)")]
        public decimal VariacaoSeisMeses { get; set; }
        [Column("variacao_doze_meses", TypeName = "decimal(22, 10)")]
        public decimal VariacaoDozeMeses { get; set; }
        [Column("variacao_vintequatro_meses", TypeName = "decimal(22, 10)")]
        public decimal VariacaoVintequatroMeses { get; set; }
        [Column("variacoa_trintaseis_meses", TypeName = "decimal(22, 10)")]
        public decimal VariacoaTrintaseisMeses { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblPosicaoRentabilidade))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
