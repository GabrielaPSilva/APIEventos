using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Keyless]
    [Table("tbl_log_erros")]
    public partial class TblLogErros
    {
        [Column("id")]
        public Guid? Id { get; set; }
        [Column("sistema")]
        [StringLength(50)]
        public string Sistema { get; set; }
        [Column("metodo", TypeName = "text")]
        public string Metodo { get; set; }
        [Column("linha")]
        public int? Linha { get; set; }
        [Column("mensagem", TypeName = "text")]
        public string Mensagem { get; set; }
        [Column("descricao", TypeName = "text")]
        public string Descricao { get; set; }
        [Column("usuario_modificacao")]
        public int? UsuarioModificacao { get; set; }
        [Column("data_cadastro", TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
    }
}
