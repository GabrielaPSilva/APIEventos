﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_acordo_remuneracao")]
    public partial class TblAcordoRemuneracao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_contrato_fundo")]
        public int CodContratoFundo { get; set; }
        [Column("inicio")]
        public double Inicio { get; set; }
        [Column("fim")]
        public double Fim { get; set; }
        [Column("percentual")]
        public double Percentual { get; set; }
        [Column("tipo_taxa")]
        [StringLength(15)]
        public string TipoTaxa { get; set; }
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

        [ForeignKey(nameof(CodContratoFundo))]
        [InverseProperty(nameof(TblContratoDistribuicao.TblAcordoRemuneracao))]
        public virtual TblContratoDistribuicao CodContratoFundoNavigation { get; set; }
    }
}
