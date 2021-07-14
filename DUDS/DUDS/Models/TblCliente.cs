using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_cliente")]
    [Index(nameof(CodDistribuidor), Name = "cod_distr_tbl_cliente")]
    [Index(nameof(NomeCliente), Name = "nome_cliente_tbl_cliente")]
    public partial class TblCliente
    {
        public TblCliente()
        {
            TblAlocador = new HashSet<TblAlocador>();
            TblCalculoPgtoAdmPfee = new HashSet<TblCalculoPgtoAdmPfee>();
            TblMovimentacaoNota = new HashSet<TblMovimentacaoNota>();
            TblOrdemPassivo = new HashSet<TblOrdemPassivo>();
            TblPgtoAdmPfee = new HashSet<TblPgtoAdmPfee>();
            TblPosicaoCliente = new HashSet<TblPosicaoCliente>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("cod_cliente_custodia")]
        [StringLength(50)]
        public string CodClienteCustodia { get; set; }
        [Required]
        [Column("nome_cliente")]
        [StringLength(100)]
        public string NomeCliente { get; set; }
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Column("cod_distribuidor")]
        public int CodDistribuidor { get; set; }
        [Column("cod_administrador")]
        public int? CodAdministrador { get; set; }
        [Column("cod_gestor")]
        public int? CodGestor { get; set; }
        [Required]
        [Column("cod_cliente_distribuidor")]
        [StringLength(14)]
        public string CodClienteDistribuidor { get; set; }
        [Required]
        [Column("tipo_cliente")]
        [StringLength(10)]
        public string TipoCliente { get; set; }
        [Column("data_modificacao", TypeName = "smalldatetime")]
        public DateTime DataModificacao { get; set; }
        [Column("usuario_modificacao")]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }

        [ForeignKey(nameof(CodAdministrador))]
        [InverseProperty(nameof(TblAdministrador.TblCliente))]
        public virtual TblAdministrador CodAdministradorNavigation { get; set; }
        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblCliente))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
        [ForeignKey(nameof(CodGestor))]
        [InverseProperty(nameof(TblGestor.TblCliente))]
        public virtual TblGestor CodGestorNavigation { get; set; }
        [InverseProperty("CodClienteNavigation")]
        public virtual ICollection<TblAlocador> TblAlocador { get; set; }
        [InverseProperty("CodClienteNavigation")]
        public virtual ICollection<TblCalculoPgtoAdmPfee> TblCalculoPgtoAdmPfee { get; set; }
        [InverseProperty("CdCotistaNavigation")]
        public virtual ICollection<TblMovimentacaoNota> TblMovimentacaoNota { get; set; }
        [InverseProperty("CdCotistaNavigation")]
        public virtual ICollection<TblOrdemPassivo> TblOrdemPassivo { get; set; }
        [InverseProperty("CodClienteNavigation")]
        public virtual ICollection<TblPgtoAdmPfee> TblPgtoAdmPfee { get; set; }
        [InverseProperty("CodClienteNavigation")]
        public virtual ICollection<TblPosicaoCliente> TblPosicaoCliente { get; set; }
    }
}
