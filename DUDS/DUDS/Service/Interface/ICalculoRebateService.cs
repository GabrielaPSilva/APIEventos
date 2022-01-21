using DUDS.Models.Filtros;
using DUDS.Models.Rebate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ICalculoRebateService : IGenericOperationsService<CalculoRebateModel>
	{
		const string QUERY_BASE = @"SELECT
										calculo_pgto_adm_pfee.*,
	                                   	pagamento.Competencia,
										pagamento.CodInvestidorDistribuidor,
                                        investidor.NomeInvestidor,
	                                    investidor.Cnpj,
	                                    investidor_distribuidor.CodInvestAdministrador AS CodMellon,
										investidor_distribuidor.CodTipoContrato,
                                        investidor_distribuidor.CodGrupoRebate,
                                        grupo_rebate.NomeGrupoRebate,
                                        tipo_contrato.TipoContrato AS NomeTipoContrato,
										pagamento.CodFundo,
                                        fundo.NomeReduzido AS NomeFundo,
	                                    fundo.Cnpj AS CnpjFundo,
	                                    distribuidor.NomeDistribuidor,
                                        pagamento.CodAdministrador,
	                                    administrador.NomeAdministrador,
	                                    pagamento.TaxaAdministracao AS ValorAdm,
	                                    pagamento.TaxaPerformanceResgate AS ValorPfeeResgate,
	                                    pagamento.TaxaPerformanceApropriada AS ValorPfeeSemestre,
                                        contrato_remuneracao.PercentualAdm AS PercAdm,
                                        contrato_remuneracao.PercentualPfee AS PercPfee
                                    FROM
                                        tbl_calculo_pgto_adm_pfee calculo_pgto_adm_pfee
											INNER JOIN tbl_pgto_adm_pfee pagamento ON pagamento.Id = calculo_pgto_adm_pfee.CodPgtoAdmPfee
		                                    INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.Id = pagamento.CodInvestidorDistribuidor
		                                    INNER JOIN tbl_investidor investidor ON investidor.Id = investidor_distribuidor.CodInvestidor
		                                    INNER JOIN tbl_distribuidor_administrador distribuidor_administrador ON investidor_distribuidor.CodDistribuidorAdministrador = distribuidor_administrador.Id
		                                    INNER JOIN tbl_distribuidor distribuidor ON distribuidor.Id = distribuidor_administrador.CodDistribuidor
											INNER JOIN tbl_contrato_remuneracao contrato_remuneracao ON calculo_pgto_adm_pfee.CodContratoRemuneracao = contrato_remuneracao.Id
		                                    INNER JOIN tbl_administrador administrador ON pagamento.CodAdministrador = administrador.Id
		                                    INNER JOIN tbl_grupo_rebate grupo_rebate ON grupo_rebate.Id = investidor_distribuidor.CodGrupoRebate
		                                    INNER JOIN tbl_fundo fundo ON fundo.Id = pagamento.CodFundo
		                                    INNER JOIN tbl_tipo_contrato tipo_contrato ON tipo_contrato.Id = investidor_distribuidor.CodTipoContrato";

		Task<IEnumerable<CalculoRebateModel>> AddBulkAsync(List<CalculoRebateModel> item);
		Task<bool> DeleteByCompetenciaAsync(string competencia);
		Task<CalculoRebateViewModel> GetByIdAsync(Guid id);
		Task<IEnumerable<CalculoRebateViewModel>> GetCalculoRebate(string competencia, int? codGrupoRebate);
        Task<IEnumerable<DescricaoCalculoRebateViewModel>> GetDescricaoRebateAsync(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao);
        Task<int> GetCountCalculoRebateAsync(FiltroModel filtro);
		Task<IEnumerable<ControleRebateModel>> GetParametroControleRebateAsync(string competencia);

	}
}
