using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class FundoModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string NomeReduzido { get; set; }

        [Required]
        [StringLength(150)]
        public string NomeFundo { get; set; }
        public string Mnemonico { get; set; }
        public string Cnpj { get; set; }
        public float? PerformanceFee { get; set; }
        public float? AdmFee { get; set; }

        [Required]
        [StringLength(6)]
        public string TipoFundo { get; set; }

        [StringLength(40)]
        public string ClassificacaoAnbima { get; set; }

        [StringLength(30)]
        public string ClassificacaoCvm { get; set; }
        public int? MasterId { get; set; }
        public DateTime? DataEncerramento { get; set; }
        public DateTime? DataCotaInicial { get; set; }
        public float? ValorCotaInicial { get; set; }

        [StringLength(10)]
        public string CodAnbima { get; set; }

        [StringLength(10)]
        public string CodCvm { get; set; }

        [StringLength(1)]
        public string TipoCota { get; set; }
        public int CodAdministrador { get; set; }
        public int CodCustodiante { get; set; }
        public int? CodGestor { get; set; }

        [StringLength(15)]
        public string AtivoCetip { get; set; }

        [StringLength(12)]
        public string Isin { get; set; }

        [StringLength(20)]
        public string NumeroGiin { get; set; }

        [StringLength(3)]
        public string MoedaFundo { get; set; }
        public int? CdFundoAdm { get; set; }

        [StringLength(10)]
        public string Estrategia { get; set; }
        public int? DiasCotizacaoAplicacao { get; set; }

        [StringLength(2)]
        public string ContagemDiasCotizacaoAplicacao { get; set; }
        public int? DiasCotizacaoResgate { get; set; }

        [StringLength(2)]
        public string ContagemDiasCotizacaoResgate { get; set; }
        public int? DiasLiquidacaoAplicacao { get; set; }

        [StringLength(2)]
        public string ContagemDiasLiquidacaoAplicacao { get; set; }
        public int? DiasLiquidacaoResgate { get; set; }

        [StringLength(2)]
        public string ContagemDiasLiquidacaoResgate { get; set; }
        public DateTime DataModificacao { get; set; }

        [Required]
        [StringLength(50)]
        public string UsuarioModificacao { get; set; }
        public bool? Ativo { get; set; }
    }
}
