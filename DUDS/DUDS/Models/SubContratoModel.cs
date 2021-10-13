using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class SubContratoModel
    {
        public int Id { get; set; }
        public int CodContrato { get; set; }

        [StringLength(20)]
        public string Versao { get; set; }

        [StringLength(30)]
        public string Status { get; set; }

        [StringLength(50)]
        public string IdDocusign { get; set; }
        public DateTime DataInclusaoContrato { get; set; }
        public bool ClausulaRetroatividade { get; set; }
        public DateTime? DataRetroatividade { get; set; }
        public DateTime? DataAssinatura { get; set; }
        public DateTime? DataVigenciaInicio { get; set; }
        public DateTime? DataVigenciaFim { get; set; }

        [StringLength(100)]
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }

        public List<ContratoAlocadorModel> ListaContratoAlocador { get; set; }

        public List<ContratoFundoModel> ListaContratoFundo { get; set; }

        public SubContratoModel()
        {
            ListaContratoFundo = new List<ContratoFundoModel>();
            ListaContratoAlocador = new List<ContratoAlocadorModel>();
        }
    }
}
