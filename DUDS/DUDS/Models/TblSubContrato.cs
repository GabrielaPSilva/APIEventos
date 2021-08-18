using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_sub_contrato")]
    public partial class TblSubContrato
    {
        public TblSubContrato()
        {
            TblContratoDistribuicao = new HashSet<TblContratoDistribuicao>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_contrato")]
        public int CodContrato { get; set; }
        [Required]
        [Column("versao")]
        [StringLength(20)]
        public string Versao { get; set; }
        [Required]
        [Column("status")]
        [StringLength(10)]
        public string Status { get; set; }
        [Column("id_docusign")]
        [StringLength(50)]
        public string IdDocusign { get; set; }
        [Column("clausula_retroatividade")]
        public bool ClausulaRetroatividade { get; set; }
        [Column("data_retroatividade", TypeName = "date")]
        public DateTime? DataRetroatividade { get; set; }
        [Column("data_assinatura", TypeName = "date")]
        public DateTime? DataAssinatura { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Column("data_modificacao", TypeName = "datetime")]
        public DateTime DataModificacao { get; set; }

        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(TblContrato.TblSubContrato))]
        public virtual TblContrato CodContratoNavigation { get; set; }
        [InverseProperty("CodSubContratoNavigation")]
        public virtual ICollection<TblContratoDistribuicao> TblContratoDistribuicao { get; set; }
    }
}
