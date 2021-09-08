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
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("nome_cliente")]
        [StringLength(100)]
        public string NomeCliente { get; set; }
        [Column("cod_distribuidor")]
        public int CodDistribuidor { get; set; }
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

        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblCliente))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
    }
}
