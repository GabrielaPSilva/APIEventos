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
        [Column("cod_investidor")]
        public int CodInvestidor { get; set; }
        [Column("cod_contrato_distribuicao")]
        public int CodContratoDistribuicao { get; set; }
        [Required]
        [Column("direcao_pagamento")]
        [StringLength(20)]
        public string DirecaoPagamento { get; set; }
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
