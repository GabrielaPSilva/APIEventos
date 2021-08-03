using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_investidor")]
    public partial class TblInvestidor
    {
        public TblInvestidor()
        {
            TblInvestidorDistribuidor = new HashSet<TblInvestidorDistribuidor>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nome_cliente")]
        [StringLength(200)]
        public string NomeCliente { get; set; }
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Required]
        [Column("tipo_cliente")]
        [StringLength(20)]
        public string TipoCliente { get; set; }
        [Column("cod_administrador")]
        public int? CodAdministrador { get; set; }
        [Column("cod_gestor")]
        public int? CodGestor { get; set; }
        [Column("data_modificacao", TypeName = "date")]
        public DateTime DataModificacao { get; set; }
        [Column("usuario_modificacao")]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        [Required]
        [Column("ativo")]
        public bool? Ativo { get; set; }

        [InverseProperty("CodInvestidorNavigation")]
        public virtual ICollection<TblInvestidorDistribuidor> TblInvestidorDistribuidor { get; set; }
    }
}
