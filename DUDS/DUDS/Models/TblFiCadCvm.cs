using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_fi_cad_cvm")]
    public partial class TblFiCadCvm
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("denom_social")]
        [StringLength(255)]
        public string DenomSocial { get; set; }
        [Required]
        [Column("cnpj_fundo")]
        [StringLength(14)]
        public string CnpjFundo { get; set; }
        [Required]
        [Column("sit")]
        [StringLength(50)]
        public string Sit { get; set; }
        [Column("cnpj_admin")]
        [StringLength(14)]
        public string CnpjAdmin { get; set; }
        [Column("admin")]
        [StringLength(255)]
        public string Admin { get; set; }
        [Column("cpf_cnpj_gestor")]
        [StringLength(14)]
        public string CpfCnpjGestor { get; set; }
        [Column("gestor")]
        [StringLength(255)]
        public string Gestor { get; set; }
        [Column("cnpj_custodiante")]
        [StringLength(14)]
        public string CnpjCustodiante { get; set; }
        [Column("custodiante")]
        [StringLength(255)]
        public string Custodiante { get; set; }
        [Column("data_base", TypeName = "date")]
        public DateTime? DataBase { get; set; }
        [Column("data_modificacao", TypeName = "datetime")]
        public DateTime? DataModificacao { get; set; }
    }
}
