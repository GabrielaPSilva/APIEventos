using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_tipo_contrato")]
    [Index(nameof(TipoContrato), Name = "IX_tbl_tipo_contrato", IsUnique = true)]
    public partial class TblTipoContrato
    {
        public TblTipoContrato()
        {
            TblContrato = new HashSet<TblContrato>();
            TblInvestidor = new HashSet<TblInvestidor>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("tipo_contrato")]
        [StringLength(50)]
        public string TipoContrato { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }
        [Column("data_modificacao", TypeName = "smalldatetime")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }

        [InverseProperty("CodTipoContratoNavigation")]
        public virtual ICollection<TblContrato> TblContrato { get; set; }
        [InverseProperty("CodTipoContratoNavigation")]
        public virtual ICollection<TblInvestidor> TblInvestidor { get; set; }
    }
}
