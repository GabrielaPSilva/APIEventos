using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_tipo_conta")]
    [Index(nameof(TipoConta), nameof(DescricaoConta), Name = "IX_tbl_tipo_conta", IsUnique = true)]
    public partial class TblTipoConta
    {
        public TblTipoConta()
        {
            TblContas = new HashSet<TblContas>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("tipo_conta")]
        [StringLength(50)]
        public string TipoConta { get; set; }
        [Required]
        [Column("descricao_conta")]
        [StringLength(50)]
        public string DescricaoConta { get; set; }
        [Column("data_modificacao", TypeName = "smalldatetime")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }

        [InverseProperty("CodTipoContaNavigation")]
        public virtual ICollection<TblContas> TblContas { get; set; }
    }
}
