using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_administrador")]
    [Index(nameof(Cnpj), Name = "cnpj_tbl_administrador", IsUnique = true)]
    [Index(nameof(NomeAdministrador), Name = "nome_adm_tbl_administrador", IsUnique = true)]
    public partial class TblAdministrador
    {
        public TblAdministrador()
        {
            TblCalculoPgtoAdmPfee = new HashSet<TblCalculoPgtoAdmPfee>();
            TblDistribuidorAdministrador = new HashSet<TblDistribuidorAdministrador>();
            TblFundo = new HashSet<TblFundo>();
            TblInvestidorDistribuidor = new HashSet<TblInvestidorDistribuidor>();
            TblOrdemPassivo = new HashSet<TblOrdemPassivo>();
            TblPgtoAdmPfee = new HashSet<TblPgtoAdmPfee>();
            TblPosicaoCliente = new HashSet<TblPosicaoCliente>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nome_administrador")]
        [StringLength(100)]
        public string NomeAdministrador { get; set; }
        [Required]
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Column("data_modificacao", TypeName = "smalldatetime")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }

        [InverseProperty("CodAdministradorNavigation")]
        public virtual ICollection<TblCalculoPgtoAdmPfee> TblCalculoPgtoAdmPfee { get; set; }
        [InverseProperty("CodAdministradorNavigation")]
        public virtual ICollection<TblDistribuidorAdministrador> TblDistribuidorAdministrador { get; set; }
        [InverseProperty("CodAdministradorNavigation")]
        public virtual ICollection<TblFundo> TblFundo { get; set; }
        [InverseProperty("CodAdministradorNavigation")]
        public virtual ICollection<TblInvestidorDistribuidor> TblInvestidorDistribuidor { get; set; }
        [InverseProperty("CodAdministradorNavigation")]
        public virtual ICollection<TblOrdemPassivo> TblOrdemPassivo { get; set; }
        [InverseProperty("CodAdministradorNavigation")]
        public virtual ICollection<TblPgtoAdmPfee> TblPgtoAdmPfee { get; set; }
        [InverseProperty("CodAdministradorNavigation")]
        public virtual ICollection<TblPosicaoCliente> TblPosicaoCliente { get; set; }
    }
}
