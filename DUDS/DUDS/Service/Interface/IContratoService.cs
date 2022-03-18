using DUDS.Models.Contrato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContratoService : IGenericOperationsService<ContratoModel>
    {
		const string QUERY_BASE =
			@"SELECT 
	            tbl_contrato.*,
	            tbl_distribuidor.NomeDistribuidor,
	            tbl_gestor.NomeGestor,
	            tbl_tipo_contrato.TipoContrato,
				tbl_grupo_rebate.NomeGrupoRebate
            FROM
	            tbl_contrato
				INNER JOIN tbl_tipo_contrato ON tbl_contrato.CodTipoContrato = tbl_tipo_contrato.Id	            
				LEFT JOIN tbl_distribuidor ON tbl_contrato.CodDistribuidor = tbl_distribuidor.Id
	            LEFT JOIN tbl_gestor ON tbl_contrato.CodGestor = tbl_gestor.Id
	            LEFT JOIN tbl_grupo_rebate ON tbl_grupo_rebate.Id = tbl_contrato.CodGrupoRebate";

        Task<IEnumerable<EstruturaContratoViewModel>> GetContratosRebateAsync(string subContratoStatus);

        Task<IEnumerable<ContratoViewModel>> GetAllAsync();

		Task<ContratoViewModel> GetByIdAsync(int id);

	}
}
