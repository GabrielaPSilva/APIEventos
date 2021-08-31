using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_condicao_remuneracao")]
    [Index(nameof(CodFundo), nameof(CodContratoRemuneracao), Name = "IX_tbl_lista_condicoes", IsUnique = true)]
    public partial class TblCondicaoRemuneracao
    {
        public TblCondicaoRemuneracao()
        {
            TblCalculoPgtoAdmPfee = new HashSet<TblCalculoPgtoAdmPfee>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_contrato_remuneracao")]
        public int CodContratoRemuneracao { get; set; }
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Column("data_inicio", TypeName = "date")]
        public DateTime? DataInicio { get; set; }
        [Column("data_fim", TypeName = "date")]
        public DateTime? DataFim { get; set; }
        [Column("valor_posicao_inicio")]
        public double? ValorPosicaoInicio { get; set; }
        [Column("valor_posicao_fim")]
        public double? ValorPosicaoFim { get; set; }
        [Column("valor_pgto_fixo")]
        public double? ValorPgtoFixo { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }

        [ForeignKey(nameof(CodContratoRemuneracao))]
        [InverseProperty(nameof(TblContratoRemuneracao.TblCondicaoRemuneracao))]
        public virtual TblContratoRemuneracao CodContratoRemuneracaoNavigation { get; set; }
        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblCondicaoRemuneracao))]
        public virtual TblFundo CodFundoNavigation { get; set; }
        [InverseProperty("CodCondicaoRemuneracaoNavigation")]
        public virtual ICollection<TblCalculoPgtoAdmPfee> TblCalculoPgtoAdmPfee { get; set; }
    }
}
