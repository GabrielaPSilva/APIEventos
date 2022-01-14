using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models.Conta
{
    public class ContaModel
    {
        public int Id { get; set; }

        public int? CodFundo { get; set; }

        public int? CodInvestidor { get; set; }

        public int CodTipoConta { get; set; }

        public string Banco { get; set; }

        public string Agencia { get; set; }

        public string Conta { get; set; }

        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }

        public bool Ativo { get; set; }

    }
}
