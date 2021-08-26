﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_custodiante")]
    [Index(nameof(Cnpj), Name = "cnpj_tbl_custodiante", IsUnique = true)]
    public partial class TblCustodiante
    {
        public TblCustodiante()
        {
            TblFundo = new HashSet<TblFundo>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nome_custodiante")]
        [StringLength(50)]
        public string NomeCustodiante { get; set; }
        [Required]
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Column("data_modificacao", TypeName = "smalldatetime")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("usuario_modificacao")]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }

        [InverseProperty("CodCustodianteNavigation")]
        public virtual ICollection<TblFundo> TblFundo { get; set; }
    }
}
