using System;

namespace DUDS.Models.Fundo
{
    public class FundoModel
    {
        public int Id { get; set; }

        public string NomeReduzido { get; set; }

        public string NomeFundo { get; set; }

        public string Mnemonico { get; set; }

        public string Cnpj { get; set; }

        public float? PerformanceFee { get; set; }

        public float? AdmFee { get; set; }

        public string TipoFundo { get; set; }

        public string ClassificacaoAnbima { get; set; }

        public string ClassificacaoCvm { get; set; }

        public int? MasterId { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public DateTime? DataCotaInicial { get; set; }

        public float? ValorCotaInicial { get; set; }

        public string CodAnbima { get; set; }

        public string CodCvm { get; set; }

        public string TipoCota { get; set; }

        public int CodAdministrador { get; set; }

        public int CodCustodiante { get; set; }

        public int? CodGestor { get; set; }

        public int? CodTipoEstrategia { get; set; }

        public int? CodClearing { get; set; }

        public string AtivoCetip { get; set; }

        public string Isin { get; set; }

        public string NumeroGiin { get; set; }

        public string MoedaFundo { get; set; }

       public int? CdFundoAdm { get; set; }

        public int? DiasCotizacaoAplicacao { get; set; }

        public string ContagemDiasCotizacaoAplicacao { get; set; }

        public int? DiasCotizacaoResgate { get; set; }

        public string ContagemDiasCotizacaoResgate { get; set; }

        public int? DiasLiquidacaoAplicacao { get; set; }

        public string ContagemDiasLiquidacaoAplicacao { get; set; }

        public int? DiasLiquidacaoResgate { get; set; }

        public string ContagemDiasLiquidacaoResgate { get; set; }

        public DateTime DataCriacao { get; set; }

        public string UsuarioCriacao { get; set; }

        public bool Ativo { get; set; }

    }
}
