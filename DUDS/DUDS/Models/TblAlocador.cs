using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_alocador")]
    [Index(nameof(CodInvestidor), nameof(CodSubContrato), Name = "IX_tbl_alocador", IsUnique = true)]
    public partial class TblAlocador
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_investidor")]
        public int CodInvestidor { get; set; }
        [Column("cod_sub_contrato")]
        public int CodSubContrato { get; set; }
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

        [ForeignKey(nameof(CodInvestidor))]
        [InverseProperty(nameof(TblSubContrato.TblAlocador))]
        public virtual TblSubContrato CodInvestidor1 { get; set; }
        [ForeignKey(nameof(CodInvestidor))]
        [InverseProperty(nameof(TblInvestidor.TblAlocador))]
        public virtual TblInvestidor CodInvestidorNavigation { get; set; }
    }
}
