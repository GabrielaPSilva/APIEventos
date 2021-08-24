using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_contrato_alocador")]
    [Index(nameof(CodInvestidor), nameof(CodSubContrato), Name = "IX_tbl_alocador", IsUnique = true)]
    public partial class TblContratoAlocador
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_investidor")]
        public int CodInvestidor { get; set; }
        [Column("cod_sub_contrato")]
        public int CodSubContrato { get; set; }
        [Column("data_transferencia", TypeName = "date")]
        public DateTime? DataTransferencia { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }

        [ForeignKey(nameof(CodInvestidor))]
        [InverseProperty(nameof(TblInvestidor.TblContratoAlocador))]
        public virtual TblInvestidor CodInvestidorNavigation { get; set; }
        [ForeignKey(nameof(CodSubContrato))]
        [InverseProperty(nameof(TblSubContrato.TblContratoAlocador))]
        public virtual TblSubContrato CodSubContratoNavigation { get; set; }
    }
}
