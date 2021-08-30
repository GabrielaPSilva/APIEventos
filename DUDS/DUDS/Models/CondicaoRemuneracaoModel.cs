using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class CondicaoRemuneracaoModel
    {
        public int Id { get; set; }
        public int CodContratoRemuneracao { get; set; }
        public int CodFundo { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public double? ValorPosicaoInicio { get; set; }
        public double? ValorPosicaoFim { get; set; }
        public double? ValorPgtoFixo { get; set; }
        public DateTime DataModificacao { get; set; }

        [StringLength(100)]
        public string UsuarioModificacao { get; set; }

        public bool? Ativo { get; set; }
    }
}
