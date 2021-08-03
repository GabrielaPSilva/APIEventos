using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_distribuidor_administrador")]
    public partial class TblDistribuidorAdministrador
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_distr_adm")]
        [StringLength(50)]
        public string CodDistrAdm { get; set; }
        [Column("cod_distribuidor")]
        public int CodDistribuidor { get; set; }
        [Column("cod_administrador")]
        public int CodAdministrador { get; set; }

        [ForeignKey(nameof(CodAdministrador))]
        [InverseProperty(nameof(TblAdministrador.TblDistribuidorAdministrador))]
        public virtual TblAdministrador CodAdministradorNavigation { get; set; }
        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblDistribuidorAdministrador))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
    }
}
