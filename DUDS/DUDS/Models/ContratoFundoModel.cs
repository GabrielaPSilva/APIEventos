using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class ContratoFundoModel
    {
        public int Id { get; set; }
        public int CodSubContrato { get; set; }
        public int CodFundo { get; set; }
        public int CodTipoCondicao { get; set; }

        [StringLength(100)]
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }

        public string NomeFundo { get; set; }

        public string TipoCondicao { get; set; }

        public List<ContratoRemuneracaoModel> ListaContratoRemuneracao { get; set; }

        public ContratoFundoModel()
        {
            ListaContratoRemuneracao = new List<ContratoRemuneracaoModel>();
        }

    }
}
