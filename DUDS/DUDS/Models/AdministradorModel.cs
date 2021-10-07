using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class AdministradorModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string NomeAdministrador { get; set; }

        [StringLength(14)]
        public string Cnpj { get; set; }
        public DateTime DataModificacao { get; set; }

        [StringLength(50)]
        public string UsuarioModificacao { get; set; }
        public bool Ativo { get; set; }

    }
}
