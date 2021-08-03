using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_contas")]
    [Index(nameof(Banco), nameof(Agencia), nameof(Conta), Name = "IX_tbl_contas")]
    public partial class TblContas
    {
        [Key]
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Key]
        [Column("cod_tipo_conta")]
        public int CodTipoConta { get; set; }
        [Required]
        [Column("banco")]
        [StringLength(4)]
        public string Banco { get; set; }
        [Required]
        [Column("agencia")]
        [StringLength(10)]
        public string Agencia { get; set; }
        [Required]
        [Column("conta")]
        [StringLength(15)]
        public string Conta { get; set; }
        [Column("data_modificacao", TypeName = "smalldatetime")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey(nameof(CodTipoConta))]
        [InverseProperty(nameof(TblTipoConta.TblContas))]
        public virtual TblTipoConta CodTipoContaNavigation { get; set; }
    }
}
