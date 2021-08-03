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

        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(TblContrato.TblContratoDistribuicao))]
        public virtual TblContrato CodContratoNavigation { get; set; }
        [InverseProperty("CodContratoFundoNavigation")]
        public virtual ICollection<TblAcordoRemuneracao> TblAcordoRemuneracao { get; set; }
        [InverseProperty("CodContratoFundoNavigation")]
        public virtual ICollection<TblAlocador> TblAlocador { get; set; }
    }
}
