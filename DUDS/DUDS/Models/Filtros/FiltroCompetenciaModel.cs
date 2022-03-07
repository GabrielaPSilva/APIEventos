using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models.Filtros
{
    public partial class FiltroModel
    {
        public string Competencia { get; set; }
        public string NomeGrupoRebate { get; set; }
        public bool Validado { get; set; }
        public bool Enviado { get; set; }
    }
}
