using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_distribuidor_administrador")]
    [Index(nameof(CodAdministrador), nameof(CodDistribuidor), Name = "IX_tbl_distribuidor_administrador", IsUnique = true)]
    public partial class TblDistribuidorAdministrador
    {
        [Column("id")]
        public int Id { get; set; }
        [Key]
        [Column("cod_distribuidor")]
        public int CodDistribuidor { get; set; }
        [Key]
        [Column("cod_administrador")]
        public int CodAdministrador { get; set; }
        [Column("cod_distr_adm")]
        [StringLength(50)]
        public string CodDistrAdm { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }

        [ForeignKey(nameof(CodAdministrador))]
        [InverseProperty(nameof(TblAdministrador.TblDistribuidorAdministrador))]
        public virtual TblAdministrador CodAdministradorNavigation { get; set; }
        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblDistribuidorAdministrador))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
    }
}
