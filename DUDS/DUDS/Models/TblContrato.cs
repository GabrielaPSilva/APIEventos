using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_contrato")]
    public partial class TblContrato
    {
        public TblContrato()
        {
            TblSubContrato = new HashSet<TblSubContrato>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_distribuidor")]
        public int? CodDistribuidor { get; set; }
        [Required]
        [Column("tipo_contrato")]
        [StringLength(20)]
        public string TipoContrato { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "datetime")]
        public DateTime? DataModificacao { get; set; }
        [Column("parceiro")]
        public int? Parceiro { get; set; }

        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblContrato))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
        [ForeignKey(nameof(Parceiro))]
        [InverseProperty(nameof(TblGestor.TblContrato))]
        public virtual TblGestor ParceiroNavigation { get; set; }
        [InverseProperty("CodContratoNavigation")]
        public virtual ICollection<TblSubContrato> TblSubContrato { get; set; }
    }
}
