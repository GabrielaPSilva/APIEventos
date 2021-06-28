using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_feriados_anbima")]
    [Index(nameof(DataFeriado), Name = "IX_tbl_feriados_anbima")]
    public partial class TblFeriadosAnbima
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("data_feriado", TypeName = "date")]
        public DateTime DataFeriado { get; set; }
        [Required]
        [Column("dia_semana")]
        [StringLength(15)]
        public string DiaSemana { get; set; }
        [Required]
        [Column("descr_feriado")]
        [StringLength(100)]
        public string DescrFeriado { get; set; }
    }
}
