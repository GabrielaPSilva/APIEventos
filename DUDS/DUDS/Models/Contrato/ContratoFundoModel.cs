using System;

namespace DUDS.Models.Contrato
{
    public class ContratoFundoModel
    {
        public int Id { get; set; }

        public int CodSubContrato { get; set; }

        public int CodFundo { get; set; }

        public int CodTipoCondicao { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

    }
}
