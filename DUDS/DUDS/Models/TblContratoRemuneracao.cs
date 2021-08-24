using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_contrato_remuneracao")]
    [Index(nameof(CodContratoFundo), Name = "IX_tbl_acordo_remuneracao", IsUnique = true)]
    public partial class TblContratoRemuneracao
    {
        public TblContratoRemuneracao()
        {
            TblCondicaoRemuneracao = new HashSet<TblCondicaoRemuneracao>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_contrato_fundo")]
        public int CodContratoFundo { get; set; }
        [Column("percentual_adm")]
        public double PercentualAdm { get; set; }
        [Column("percentual_pfee")]
        public double PercentualPfee { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }

        [ForeignKey(nameof(CodContratoFundo))]
        [InverseProperty(nameof(TblContratoFundo.TblContratoRemuneracao))]
        public virtual TblContratoFundo CodContratoFundoNavigation { get; set; }
        [InverseProperty("CodContratoRemuneracaoNavigation")]
        public virtual ICollection<TblCondicaoRemuneracao> TblCondicaoRemuneracao { get; set; }
    }
}
