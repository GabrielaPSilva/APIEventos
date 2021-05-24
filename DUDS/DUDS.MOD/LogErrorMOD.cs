using System;
using System.Collections.Generic;
using System.Text;

namespace DUDS.MOD
{
    public class LogErrorMOD
    {
        public int Codigo { get; set; }
        public string Sistema { get; set; }
        public string Arquivo { get; set; }
        public string Metodo { get; set; }
        public int Linha { get; set; }
        public string Mensagem { get; set; }
        public string Descricao { get; set; }
        public string Tabela { get; set; }
        public int? CodigoTabela { get; set; }
        public string Outros { get; set; }
        public DateTime DataHoraCadastro { get; set; }
    }
}
