using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_contrato_distribuicao")]
    [Index(nameof(CodFundo), nameof(CodSubContrato), Name = "IX_tbl_contrato_distribuicao", IsUnique = true)]
    public partial class TblContratoDistribuicao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_sub_contrato")]
        public int CodSubContrato { get; set; }
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "datetime")]
        public DateTime DataModificacao { get; set; }

        [ForeignKey(nameof(CodFundo))]
        [InverseProperty(nameof(TblFundo.TblContratoDistribuicao))]
        public virtual TblFundo CodFundoNavigation { get; set; }
        [ForeignKey(nameof(CodSubContrato))]
        [InverseProperty(nameof(TblSubContrato.TblContratoDistribuicao))]
        public virtual TblSubContrato CodSubContratoNavigation { get; set; }
        [InverseProperty("CodContratoDistribuicaoNavigation")]
        public virtual TblAcordoRemuneracao TblAcordoRemuneracao { get; set; }
    }
}
