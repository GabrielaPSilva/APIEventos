using System;

namespace DUDS.Models.Distribuidor
{
    public class DistribuidorModel
    {
        public int Id { get; set; }

        public string NomeDistribuidor { get; set; }

        public string Cnpj { get; set; }

        public int? CodTipoClassificacao { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool Ativo { get; set; }

    }
}
