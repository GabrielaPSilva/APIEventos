﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_gestor")]
    [Index(nameof(Cnpj), Name = "IX_cnpj_tbl_gestor", IsUnique = true)]
    public partial class TblGestor
    {
        public TblGestor()
        {
            TblCliente = new HashSet<TblCliente>();
            TblFundo = new HashSet<TblFundo>();
            TblMovimentacaoNota = new HashSet<TblMovimentacaoNota>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nome_gestor")]
        [StringLength(100)]
        public string NomeGestor { get; set; }
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Column("cod_gestor_adm")]
        public int? CodGestorAdm { get; set; }
        [Column("data_modificacao", TypeName = "smalldatetime")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }

        [InverseProperty("CodGestorNavigation")]
        public virtual ICollection<TblCliente> TblCliente { get; set; }
        [InverseProperty("CodGestorNavigation")]
        public virtual ICollection<TblFundo> TblFundo { get; set; }
        [InverseProperty("CodGestorNavigation")]
        public virtual ICollection<TblMovimentacaoNota> TblMovimentacaoNota { get; set; }
    }
}
