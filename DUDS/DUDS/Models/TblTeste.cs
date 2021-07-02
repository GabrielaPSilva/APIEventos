using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_teste")]
    public partial class TblTeste
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("valor")]
        public double? Valor { get; set; }
        [Column("observacao")]
        [StringLength(255)]
        public string Observacao { get; set; }
        [Column("data_cadastro", TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [Column("usuario_modificacao")]
        public int? UsuarioModificacao { get; set; }
    }
}
