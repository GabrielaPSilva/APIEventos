using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_distribuidor")]
    [Index(nameof(Cnpj), Name = "cnpj_tbl_distribuidor", IsUnique = true)]
    public partial class TblDistribuidor
    {
        public TblDistribuidor()
        {
            TblCliente = new HashSet<TblCliente>();
            TblContrato = new HashSet<TblContrato>();
            TblDistribuidorAdministrador = new HashSet<TblDistribuidorAdministrador>();
            TblInvestidorDistribuidor = new HashSet<TblInvestidorDistribuidor>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nome_distribuidor")]
        [StringLength(100)]
        public string NomeDistribuidor { get; set; }
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
        [Column("cod_tipo_classificacao")]
        public int? CodTipoClassificacao { get; set; }

        [ForeignKey(nameof(CodTipoClassificacao))]
        [InverseProperty(nameof(TblTipoClassificacao.TblDistribuidor))]
        public virtual TblTipoClassificacao CodTipoClassificacaoNavigation { get; set; }

        [InverseProperty("CodDistribuidorNavigation")]
        public virtual ICollection<TblCliente> TblCliente { get; set; }
        [InverseProperty("CodDistribuidorNavigation")]
        public virtual ICollection<TblContrato> TblContrato { get; set; }
        [InverseProperty("CodDistribuidorNavigation")]
        public virtual ICollection<TblDistribuidorAdministrador> TblDistribuidorAdministrador { get; set; }
        [InverseProperty("CodDistribuidorNavigation")]
        public virtual ICollection<TblInvestidorDistribuidor> TblInvestidorDistribuidor { get; set; }
    }
}
