using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_contrato")]
    public partial class TblContrato
    {
        public TblContrato()
        {
            TblContratoDistribuicao = new HashSet<TblContratoDistribuicao>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_distribuidor")]
        public int? CodDistribuidor { get; set; }
        [Required]
        [Column("tipo_contrato")]
        [StringLength(20)]
        public string TipoContrato { get; set; }
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
        [Required]
        [Column("direcao_pagamento")]
        [StringLength(20)]
        public string DirecaoPagamento { get; set; }
        [Column("clausula_retroatividade")]
        public bool ClausulaRetroatividade { get; set; }
        [Column("data_retroatividade", TypeName = "date")]
        public DateTime? DataRetroatividade { get; set; }
        [Column("data_assinatura", TypeName = "date")]
        public DateTime? DataAssinatura { get; set; }

        [ForeignKey(nameof(CodDistribuidor))]
        [InverseProperty(nameof(TblDistribuidor.TblContrato))]
        public virtual TblDistribuidor CodDistribuidorNavigation { get; set; }
        [InverseProperty("CodContratoNavigation")]
        public virtual ICollection<TblContratoDistribuicao> TblContratoDistribuicao { get; set; }
    }
}
