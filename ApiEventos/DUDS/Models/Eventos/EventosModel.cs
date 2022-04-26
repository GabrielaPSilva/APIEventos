using System;

namespace DUDS.Models.Administrador
{
    public class EventosModel
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public string NomeEvento { get; set; }
        public string Observacao { get; set; } = null;
        public DateTime DataEvento { get; set; }
    }
}
