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
	                                   	pagamento.competencia,
										pagamento.cod_investidor_distribuidor,
                                        investidor.nome_investidor,
	                                    investidor.cnpj,
	                                    investidor_distribuidor.cod_invest_administrador AS cod_mellon,
										investidor_distribuidor.cod_tipo_contrato,
                                        investidor_distribuidor.cod_grupo_rebate,
                                        grupo_rebate.nome_grupo_rebate,
                                        tipo_contrato.tipo_contrato AS nome_tipo_contrato,
										pagamento.cod_fundo,
                                        fundo.nome_reduzido AS nome_fundo,
	                                    fundo.cnpj AS cnpj_fundo,
	                                    distribuidor.nome_distribuidor,
                                        pagamento.cod_administrador,
	                                    administrador.nome_administrador,
	                                    pagamento.taxa_administracao AS valor_adm,
	                                    pagamento.taxa_performance_resgate AS valor_pfee_resgate,
	                                    pagamento.taxa_performance_apropriada AS valor_pfee_semestre,
                                        contrato_remuneracao.percentual_adm AS perc_adm,
                                        contrato_remuneracao.percentual_pfee AS perc_pfee
                                    FROM
                                        tbl_calculo_pgto_adm_pfee calculo_pgto_adm_pfee
											INNER JOIN tbl_pgto_adm_pfee pagamento ON pagamento.id = calculo_pgto_adm_pfee.cod_pgto_adm_pfee
		                                    INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.id = pagamento.cod_investidor_distribuidor
		                                    INNER JOIN tbl_investidor investidor ON investidor.id = investidor_distribuidor.cod_investidor
		                                    INNER JOIN tbl_distribuidor_administrador distribuidor_administrador ON investidor_distribuidor.cod_distribuidor_administrador = distribuidor_administrador.id
		                                    INNER JOIN tbl_distribuidor distribuidor ON distribuidor.id = distribuidor_administrador.cod_distribuidor
											INNER JOIN tbl_contrato_remuneracao contrato_remuneracao ON calculo_pgto_adm_pfee.cod_contrato_remuneracao = contrato_remuneracao.id
		                                    INNER JOIN tbl_administrador administrador ON pagamento.cod_administrador = administrador.id
		                                    INNER JOIN tbl_grupo_rebate grupo_rebate ON grupo_rebate.id = investidor_distribuidor.cod_grupo_rebate
		                                    INNER JOIN tbl_fundo fundo ON fundo.id = pagamento.cod_fundo
		                                    INNER JOIN tbl_tipo_contrato tipo_contrato ON tipo_contrato.id = investidor_distribuidor.cod_tipo_contrato";

		Task<IEnumerable<CalculoRebateModel>> AddBulkAsync(List<CalculoRebateModel> item);
		Task<bool> DeleteByCompetenciaAsync(string competencia);
		Task<CalculoRebateViewModel> GetByIdAsync(Guid id);
		Task<IEnumerable<CalculoRebateViewModel>> GetCalculoRebate(string competencia, int? codGrupoRebate);
        Task<IEnumerable<DescricaoCalculoRebateViewModel>> GetDescricaoRebateAsync(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao);
        Task<int> GetCountCalculoRebateAsync(FiltroModel filtro);
    }
}
