using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_alocador")]
    [Index(nameof(Id), Name = "IX_tbl_alocador", IsUnique = true)]
    public partial class TblAlocador
    {
        [Column("id")]
        public int Id { get; set; }
        [Key]
        [Column("cod_investidor")]
        public int CodInvestidor { get; set; }
        [Key]
        [Column("cod_contrato_distribuicao")]
        public int CodContratoDistribuicao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }

        [ForeignKey(nameof(CodContratoDistribuicao))]
        [InverseProperty(nameof(TblContratoDistribuicao.TblAlocador))]
        public virtual TblContratoDistribuicao CodContratoDistribuicaoNavigation { get; set; }
        [ForeignKey(nameof(CodInvestidor))]
        [InverseProperty(nameof(TblInvestidor.TblAlocador))]
        public virtual TblInvestidor CodInvestidorNavigation { get; set; }
    }
}
