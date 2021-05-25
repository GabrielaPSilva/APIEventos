using System;
using System.Collections.Generic;
using System.Text;

namespace DUDS.MOD
{
    public class LogErrorMOD
    {
        public int id { get; set; }
        public string sistema { get; set; }
        public string metodo { get; set; }
        public string mensagem { get; set; }
        public string descricao { get; set; }
        public int linha { get; set; }
        public string tabela { get; set; }
        public int cod_usuario { get; set; }
        public DateTime data_cadastro { get; set; }
    }
}
