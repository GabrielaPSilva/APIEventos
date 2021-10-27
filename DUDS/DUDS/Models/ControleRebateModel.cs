using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class ControleRebateModel
    {
        public int Id { get; set; }
        public int CodGrupoRebate { get; set; }
        public string Competencia { get; set; }
        public bool Validado { get; set; }
        public bool Enviado { get; set; }
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }

        public string NomeGrupoRebate { get; set; }
        public string NomeInvestidor { get; set; }
        public CalculoRebateModel Calculo { get; set; }
    }
}
