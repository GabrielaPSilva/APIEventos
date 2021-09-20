using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_tipo_classificacao")]
    [Index(nameof(Classificacao), Name = "IX_tbl_tipo_classificacao")]
    public partial class TblTipoClassificacao
    {
        public TblTipoClassificacao()
        {
            TblDistribuidor = new HashSet<TblDistribuidor>();
            TblGestor = new HashSet<TblGestor>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("classificacao")]
        [StringLength(50)]
        public string Classificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }

        [InverseProperty("CodTipoClassificacaoNavigation")]
        public virtual ICollection<TblDistribuidor> TblDistribuidor { get; set; }
        [InverseProperty("CodTipoClassificacaoNavigation")]
        public virtual ICollection<TblGestor> TblGestor { get; set; }
    }
}
