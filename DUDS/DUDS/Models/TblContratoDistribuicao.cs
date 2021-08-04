using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_contrato_distribuicao")]
    public partial class TblContratoDistribuicao
    {
        public TblContratoDistribuicao()
        {
            TblAcordoRemuneracao = new HashSet<TblAcordoRemuneracao>();
            TblAlocador = new HashSet<TblAlocador>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_contrato")]
        public int CodContrato { get; set; }
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "datetime")]
        public DateTime? DataModificacao { get; set; }

        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(TblContrato.TblContratoDistribuicao))]
        public virtual TblContrato CodContratoNavigation { get; set; }
        [InverseProperty("CodContratoDistribuicaoNavigation")]
        public virtual ICollection<TblAcordoRemuneracao> TblAcordoRemuneracao { get; set; }
        [InverseProperty("CodContratoDistribuicaoNavigation")]
        public virtual ICollection<TblAlocador> TblAlocador { get; set; }
    }
}
