using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_tipo_condicao")]
    public partial class TblTipoCondicao
    {
        public TblTipoCondicao()
        {
            TblAcordoCondicional = new HashSet<TblAcordoCondicional>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("tipo_condicao")]
        [StringLength(50)]
        public string TipoCondicao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }

        [InverseProperty("CodTipoCondicaoNavigation")]
        public virtual ICollection<TblAcordoCondicional> TblAcordoCondicional { get; set; }
    }
}
