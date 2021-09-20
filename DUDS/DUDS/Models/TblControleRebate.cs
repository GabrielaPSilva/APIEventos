using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_controle_rebate")]
    [Index(nameof(CodGrupoRebate), nameof(Competencia), Name = "IX_tbl_controle_rebate", IsUnique = true)]
    [Index(nameof(CodGrupoRebate), Name = "IX_tbl_controle_rebate_1")]
    [Index(nameof(Competencia), Name = "IX_tbl_controle_rebate_2")]
    public partial class TblControleRebate
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_grupo_rebate")]
        public int CodGrupoRebate { get; set; }
        [Required]
        [Column("competencia")]
        [StringLength(7)]
        public string Competencia { get; set; }
        [Column("validado")]
        public int Validado { get; set; }
        [Column("enviado")]
        public int Enviado { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }

        [ForeignKey(nameof(CodGrupoRebate))]
        [InverseProperty(nameof(TblGrupoRebate.TblControleRebate))]
        public virtual TblGrupoRebate CodGrupoRebateNavigation { get; set; }
    }
}
