using DUDS.Models.Filtros;
using DUDS.Models.Rebate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ICalculoServicoService : IGenericOperationsService<CalculoServicoModel>
	{
		const string QUERY_BASE = @"
									SELECT
										tbl_calculo_pgto_servico.*
										tbl_distribuidor.NomeDistribuidor,
										tbl_custodiante.NomeCustodiante,
										tbl_administrador.NomeAdministrador
									FROM
										tbl_calculo_pgto_servico
										LEFT JOIN tbl_distribuidor ON tbl_distribuidor.Id = tbl_calculo_pgto_servico.CodDistribuidor
										LEFT JOIN tbl_custodiante ON tbl_custodiante.Id = tbl_calculo_pgto_servico.CodCustodiante
										LEFT JOIN tbl_administrador ON tbl_administrador.Id = tbl_calculo_pgto_servico.CodAdministrador";

		Task<IEnumerable<CalculoServicoModel>> AddBulkAsync(List<CalculoServicoModel> item);

		Task<IEnumerable<CalculoServicoViewModel>> GetCalculoServico(string competencia);
		/*
		Task<CalculoRebateViewModel> GetByIdAsync(Guid id);
		
        Task<IEnumerable<DescricaoCalculoRebateViewModel>> GetDescricaoRebateAsync(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao);
        Task<int> GetCountCalculoRebateAsync(FiltroModel filtro);
		Task<IEnumerable<ControleRebateModel>> GetParametroControleRebateAsync(string competencia);
		*/
	}
}
