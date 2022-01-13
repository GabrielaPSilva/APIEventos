using System;

namespace DUDS.Models.Gestor
{
    public class GestorModel
    {
        public int Id { get; set; }

        public string NomeGestor { get; set; }

        public string Cnpj { get; set; }

        public int CodTipoClassificacao { get; set; }

        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }

        public bool Ativo { get; set; }

    }
}
