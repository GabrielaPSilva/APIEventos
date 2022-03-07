using System;

namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoOpcaoModel : PosicaoAtivoComumModel
    {
        public string Ativo { get; set; }

        public string Tipo { get; set; }

        public string Papel { get; set; }

        public string Corretora { get; set; }

        public decimal PrecoExercicio { get; set; } = 0;

        public DateTime DataVcto { get; set; }

        public decimal Qtde { get; set; } = 0;

        public decimal CustoCorretagem { get; set; } = 0;

        public decimal Cotacao { get; set; } = 0;

        public decimal CustoTotal { get; set; } = 0;

        public decimal Resultado { get; set; } = 0;

        public decimal ValorMercado { get; set; } = 0;
    }
}
