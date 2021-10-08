using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class TipoCondicaoModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string TipoCondicao { get; set; }
        public bool? Ativo { get; set; }
        public DateTime DataCriacao { get; set; }

        [StringLength(100)]
        public string UsuarioCriacao { get; set; }
    }
}
