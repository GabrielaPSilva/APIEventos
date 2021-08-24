using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_investidor_distribuidor")]
    [Index(nameof(Id), Name = "IX_tbl_investidor_distribuidor", IsUnique = true)]
    public partial class TblInvestidorDistribuidor
    {
        public TblInvestidorDistribuidor()
        {
            TblPgtoAdmPfee = new HashSet<TblPgtoAdmPfee>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("cod_invest_administrador")]
        [StringLength(50)]
        public string CodInvestAdministrador { get; set; }
        [Key]
        [Column("cod_investidor")]
        public int CodInvestidor { get; set; }
        [Key]
        [Column("cod_distribuidor")]
        public int CodDistribuidor { get; set; }
        [Key]
        [Column("cod_administrador")]
        public int CodAdministrador { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }

        [ForeignKey(nameof(CodAdministrador))]
        [InverseProperty(nameof(TblAdministrador.TblInvestidorDistribuidor))]
        public virtual TblAdministrador CodAdministradorNavigation { get; set; }
        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblInvestidorDistribuidor))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
        [ForeignKey(nameof(CodInvestidor))]
        [InverseProperty(nameof(TblInvestidor.TblInvestidorDistribuidor))]
        public virtual TblInvestidor CodInvestidorNavigation { get; set; }
        public virtual ICollection<TblPgtoAdmPfee> TblPgtoAdmPfee { get; set; }
    }
}
