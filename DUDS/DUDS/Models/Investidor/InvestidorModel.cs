using System;

namespace DUDS.Models.Investidor
{
    public class InvestidorModel
    {
        public int Id { get; set; }

        public string NomeInvestidor { get; set; }

        public string Cnpj { get; set; }

        public string TipoInvestidor { get; set; }

        public int? CodAdministrador { get; set; }

        public int? CodGestor { get; set; }

        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }

        public bool Ativo { get; set; }

    }
}
