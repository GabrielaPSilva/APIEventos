using System;

namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoCambioModel : PosicaoAtivoComumModel
    {
        public string Descricao { get; set; }

        public string Ativo { get; set; }

        public string Tipo { get; set; }

        public DateTime Vencimento { get; set; }

        public string MoedaVendida { get; set; }

        public string MoedaComprada { get; set; }

        public decimal ValorMoedaVendida { get; set; } = 0;

        public decimal ValorMoedaComprada { get; set; } = 0;

        public decimal ParidadeContratacao { get; set; } = 0;

        public decimal ParidadeAtual { get; set; } = 0;

        public decimal Valor { get; set; } = 0;

        public decimal ValorAjuste { get; set; } = 0;
    }
}
