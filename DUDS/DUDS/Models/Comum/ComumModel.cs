using System;

namespace DUDS.Models.Comum
{
    public class ComumModel<T>
    {
        public T Id { get; set; }

        public string UsuarioCriacao { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
