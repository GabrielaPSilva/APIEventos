using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_bcb_entidades_supervisionadas")]
    public partial class TblBcbEntidadesSupervisionadas
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nome_entidade_interesse")]
        [StringLength(255)]
        public string NomeEntidadeInteresse { get; set; }
        [Required]
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Column("data_base", TypeName = "date")]
        public DateTime? DataBase { get; set; }
        [Column("data_modificacao", TypeName = "datetime")]
        public DateTime? DataModificacao { get; set; }
    }
}
