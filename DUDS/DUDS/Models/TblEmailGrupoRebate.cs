using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_email_grupo_rebate")]
    [Index(nameof(CodGrupoRebate), Name = "IX_tbl_email_grupo_rebate")]
    [Index(nameof(CodGrupoRebate), nameof(Email), Name = "IX_tbl_email_grupo_rebate_1", IsUnique = true)]
    public partial class TblEmailGrupoRebate
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_grupo_rebate")]
        public int CodGrupoRebate { get; set; }
        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }

        [ForeignKey(nameof(CodGrupoRebate))]
        [InverseProperty(nameof(TblGrupoRebate.TblEmailGrupoRebate))]
        public virtual TblGrupoRebate CodGrupoRebateNavigation { get; set; }
    }
}
