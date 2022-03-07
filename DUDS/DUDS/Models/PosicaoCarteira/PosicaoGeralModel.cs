using DUDS.Models.Comum;
using System;

namespace DUDS.Models.PosicaoCarteira
{
    public class PosicaoGeralModel : ComumModel<Guid>
    {
        public int CodFundo { get; set; }

        public DateTime DataRef { get; set; }
    }
}
