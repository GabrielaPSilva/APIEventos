using System.Collections.Generic;

namespace DUDS.Models.Fundo
{
    public class FundoViewModel : FundoModel
    {
        public string NomeAdministrador { get; set; }
        public string NomeCustodiante { get; set; }
        public string NomeGestor { get; set; }
        public string TipoEstrategia { get; set; }
        public List<ContaModel> ListaConta { get; set; }
    }
}
