using System;
using System.ComponentModel.DataAnnotations;

namespace DUDS.Models
{
    public class GestorModel
    {

        public int Id { get; set; }

        [StringLength(100)]
        public string NomeGestor { get; set; }

        [StringLength(14)]
        public string Cnpj { get; set; }

        public int? CodTipoClassificacao { get; set; }

        public DateTime DataCriacao { get; set; }

        [StringLength(50)]
        public string UsuarioCriacao { get; set; }

        public bool Ativo { get; set; }

        public string Classificacao { get; set; }
    }
}
