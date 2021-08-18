using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_acordo_remuneracao")]
    [Index(nameof(CodContratoDistribuicao), Name = "IX_tbl_acordo_remuneracao", IsUnique = true)]
    public partial class TblAcordoRemuneracao
    {
        public TblAcordoRemuneracao()
        {
            TblAcordoCondicional = new HashSet<TblAcordoCondicional>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_contrato_distribuicao")]
        public int CodContratoDistribuicao { get; set; }
        [Column("tipo_range")]
        [StringLength(15)]
        public string TipoRange { get; set; }
        [Column("data_vigencia_inicio", TypeName = "date")]
        public DateTime DataVigenciaInicio { get; set; }
        [Column("data_vigencia_fim", TypeName = "date")]
        public DateTime DataVigenciaFim { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }

        [ForeignKey(nameof(CodContratoDistribuicao))]
        [InverseProperty(nameof(TblContratoDistribuicao.TblAcordoRemuneracao))]
        public virtual TblContratoDistribuicao CodContratoDistribuicaoNavigation { get; set; }
        [InverseProperty("CodAcordoRemuneracaoNavigation")]
        public virtual ICollection<TblAcordoCondicional> TblAcordoCondicional { get; set; }
    }
}
