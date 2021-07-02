using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Keyless]
    [Table("auditoria")]
    public partial class Auditoria
    {
        [Column("id_tabela")]
        public int IdTabela { get; set; }
        [Required]
        [Column("usuario")]
        [StringLength(50)]
        public string Usuario { get; set; }
        [Column("data", TypeName = "datetime")]
        public DateTime Data { get; set; }
        [Required]
        [Column("tabela")]
        [StringLength(50)]
        public string Tabela { get; set; }
        [Required]
        [Column("campo")]
        [StringLength(50)]
        public string Campo { get; set; }
        [Column("conteudoanterior")]
        public string Conteudoanterior { get; set; }
        [Column("conteudoatual")]
        public string Conteudoatual { get; set; }
        [Required]
        [Column("acao")]
        [StringLength(6)]
        public string Acao { get; set; }
    }
}
