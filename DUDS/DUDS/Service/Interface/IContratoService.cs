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
	            tbl_tipo_contrato.TipoContrato
            FROM
	            tbl_contrato
	            LEFT JOIN tbl_distribuidor ON tbl_contrato.CodDistribuidor = tbl_distribuidor.Id
	            LEFT JOIN tbl_gestor ON tbl_contrato.CodGestor = tbl_gestor.Id
	            INNER JOIN tbl_tipo_contrato ON tbl_contrato.CodTipoContrato = tbl_tipo_contrato.Id";

        Task<IEnumerable<EstruturaContratoViewModel>> GetContratosRebateAsync(string subContratoStatus);

        Task<IEnumerable<ContratoViewModel>> GetAllAsync();

		Task<ContratoViewModel> GetByIdAsync(int id);

	}
}
