using System.Collections.Generic;

namespace DUDS.Models.Contrato
{
    public class ContratoFundoViewModel : ContratoFundoModel
    {
        public string NomeFundo { get; set; }

        public string TipoCondicao { get; set; }

        public List<ContratoRemuneracaoModel> ListaContratoRemuneracao { get; set; }

        public ContratoFundoViewModel()
        {
            ListaContratoRemuneracao = new List<ContratoRemuneracaoModel>();

        }
    }
}
