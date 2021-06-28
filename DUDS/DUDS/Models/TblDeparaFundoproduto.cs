using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Keyless]
    [Table("tbl_depara_fundoproduto")]
    public partial class TblDeparaFundoproduto
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("cod_ativo")]
        [StringLength(50)]
        public string CodAtivo { get; set; }
        [Column("cod_custodiante")]
        public int? CodCustodiante { get; set; }
        [Column("cod_fundo")]
        public int? CodFundo { get; set; }

        [ForeignKey(nameof(CodCustodiante))]
        public virtual TblCustodiante CodCustodianteNavigation { get; set; }
        [ForeignKey(nameof(CodFundo))]
        public virtual TblFundo CodFundoNavigation { get; set; }
    }
}
