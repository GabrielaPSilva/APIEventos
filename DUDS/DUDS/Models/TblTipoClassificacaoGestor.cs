using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_tipo_classificacao_gestor")]
    public partial class TblTipoClassificacaoGestor
    {
        public TblTipoClassificacaoGestor()
        {
            TblGestor = new HashSet<TblGestor>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("classificacao_gestor")]
        [StringLength(50)]
        public string ClassificacaoGestor { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }

        [InverseProperty("CodTipoClassificacaoGestorNavigation")]
        public virtual ICollection<TblGestor> TblGestor { get; set; }
    }
}
