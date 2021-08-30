using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class LogErrosModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Sistema { get; set; }

        public string Metodo { get; set; }
        public int Linha { get; set; }

        public string Mensagem { get; set; }

        public string Descricao { get; set; }

        [StringLength(100)]
        public string UsuarioModificacao { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
