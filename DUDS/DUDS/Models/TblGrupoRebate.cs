using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_grupo_rebate")]
    [Index(nameof(NomeGrupoRebate), Name = "IX_tbl_grupo_rebate", IsUnique = true)]
    public partial class TblGrupoRebate
    {
        public TblGrupoRebate()
        {
            TblControleRebate = new HashSet<TblControleRebate>();
            TblEmailGrupoRebate = new HashSet<TblEmailGrupoRebate>();
            TblInvestidor = new HashSet<TblInvestidor>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nome_grupo_rebate")]
        [StringLength(50)]
        public string NomeGrupoRebate { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }

        [InverseProperty("CodGrupoRebateNavigation")]
        public virtual ICollection<TblControleRebate> TblControleRebate { get; set; }
        [InverseProperty("CodGrupoRebateNavigation")]
        public virtual ICollection<TblEmailGrupoRebate> TblEmailGrupoRebate { get; set; }
        [InverseProperty("CodGrupoRebateNavigation")]
        public virtual ICollection<TblInvestidor> TblInvestidor { get; set; }
    }
}
