using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class GrupoRebateModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string NomeGrupoRebate { get; set; }

        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public DateTime DataModificacao { get; set; }
        public bool? Ativo { get; set; }
    }
}
