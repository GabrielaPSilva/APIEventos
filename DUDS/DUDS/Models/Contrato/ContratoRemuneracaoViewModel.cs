using System.Collections.Generic;

namespace DUDS.Models.Contrato
{
    public class ContratoRemuneracaoViewModel : ContratoRemuneracaoModel
    {
        public List<CondicaoRemuneracaoModel> ListaCondicaoRemuneracao { get; set; }

        public ContratoRemuneracaoViewModel()
        {
            ListaCondicaoRemuneracao = new List<CondicaoRemuneracaoModel>();
        }
    }
}
