using System;

namespace DUDS.Models.LogErros
{
    public class LogErrosModel
    {
        public int Id { get; set; }

        public string Sistema { get; set; }

        public string Metodo { get; set; }

        public int Linha { get; set; }

        public string Mensagem { get; set; }

        public string Descricao { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
