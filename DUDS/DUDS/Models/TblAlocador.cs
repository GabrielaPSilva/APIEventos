using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_alocador")]
    public partial class TblAlocador
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_contrato_fundo")]
        public int CodContratoFundo { get; set; }
        [Column("cod_cliente")]
        public long CodCliente { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(TblCliente.TblAlocador))]
        public virtual TblCliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodContratoFundo))]
        [InverseProperty(nameof(TblContratoDistribuicao.TblAlocador))]
        public virtual TblContratoDistribuicao CodContratoFundoNavigation { get; set; }
    }
}
