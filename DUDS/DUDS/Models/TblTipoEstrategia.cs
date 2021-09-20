using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_tipo_estrategia")]
    [Index(nameof(Estrategia), Name = "IX_tbl_tipo_estrategia", IsUnique = true)]
    public partial class TblTipoEstrategia
    {
        public TblTipoEstrategia()
        {
            TblFundo = new HashSet<TblFundo>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("estrategia")]
        [StringLength(20)]
        public string Estrategia { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }

        [InverseProperty("CodTipoEstrategiaNavigation")]
        public virtual ICollection<TblFundo> TblFundo { get; set; }
    }
}
