using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_acordo_condicional")]
    public partial class TblAcordoCondicional
    {
        public TblAcordoCondicional()
        {
            TblListaCondicoes = new HashSet<TblListaCondicoes>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_acordo_remuneracao")]
        public int CodAcordoRemuneracao { get; set; }
        [Column("cod_tipo_condicao")]
        public int CodTipoCondicao { get; set; }
        [Column("percentual_adm")]
        public double PercentualAdm { get; set; }
        [Column("percentual_pfee")]
        public double PercentualPfee { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }

        [ForeignKey(nameof(CodAcordoRemuneracao))]
        [InverseProperty(nameof(TblAcordoRemuneracao.TblAcordoCondicional))]
        public virtual TblAcordoRemuneracao CodAcordoRemuneracaoNavigation { get; set; }
        [ForeignKey(nameof(CodTipoCondicao))]
        [InverseProperty(nameof(TblTipoCondicao.TblAcordoCondicional))]
        public virtual TblTipoCondicao CodTipoCondicaoNavigation { get; set; }
        [InverseProperty("CodAcordoCondicionalNavigation")]
        public virtual ICollection<TblListaCondicoes> TblListaCondicoes { get; set; }
    }
}
