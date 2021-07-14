﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_distribuidor")]
    [Index(nameof(Cnpj), Name = "cnpj_tbl_distribuidor", IsUnique = true)]
    public partial class TblDistribuidor
    {
        public TblDistribuidor()
        {
            TblAcordoDistribuicao = new HashSet<TblAcordoDistribuicao>();
            TblCliente = new HashSet<TblCliente>();
            TblContrato = new HashSet<TblContrato>();
            TblMovimentacaoNota = new HashSet<TblMovimentacaoNota>();
            TblOrdemPassivo = new HashSet<TblOrdemPassivo>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nome_distribuidor")]
        [StringLength(100)]
        public string NomeDistribuidor { get; set; }
        [Required]
        [Column("nome_distribuidor_reduzido")]
        [StringLength(50)]
        public string NomeDistribuidorReduzido { get; set; }
        [Required]
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Required]
        [Column("classificacao_distribuidor")]
        [StringLength(50)]
        public string ClassificacaoDistribuidor { get; set; }
        [Column("cod_distr_adm")]
        public int? CodDistrAdm { get; set; }
        [Column("data_modificacao", TypeName = "smalldatetime")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }

        [InverseProperty("CodDistribuidorNavigation")]
        public virtual ICollection<TblAcordoDistribuicao> TblAcordoDistribuicao { get; set; }
        [InverseProperty("CodDistribuidorNavigation")]
        public virtual ICollection<TblCliente> TblCliente { get; set; }
        [InverseProperty("CodDistribuidorNavigation")]
        public virtual ICollection<TblContrato> TblContrato { get; set; }
        [InverseProperty("CodDistribuidorNavigation")]
        public virtual ICollection<TblMovimentacaoNota> TblMovimentacaoNota { get; set; }
        [InverseProperty("CodDistribuidorNavigation")]
        public virtual ICollection<TblOrdemPassivo> TblOrdemPassivo { get; set; }
    }
}