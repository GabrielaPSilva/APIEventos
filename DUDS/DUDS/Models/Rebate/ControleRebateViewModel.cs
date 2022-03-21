namespace DUDS.Models.Rebate
{
    public class ControleRebateViewModel : ControleRebateModel
    {
        public string NomeGrupoRebate { get; set; }
        public bool EnviarMemoriaCalculo { get; set; }
        public CalculoRebateModel Calculo { get; set; }
    }
}
