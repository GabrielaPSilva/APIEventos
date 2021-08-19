using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class ListaCondicoesModel
    {
        public int Id { get; set; }
        public int CodAcordoCondicional { get; set; }
        public int CodFundo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public double ValorPosicaoInicio { get; set; }
        public double ValorPosicaoFim { get; set; }
        public DateTime DataModificacao { get; set; }

        //[Required]
        [StringLength(100)]
        public string UsuarioModificacao { get; set; }

        //[Required]
        public bool? Ativo { get; set; }
    }
}
