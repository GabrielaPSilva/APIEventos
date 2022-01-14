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
	            tbl_distribuidor.nome_distribuidor,
	            tbl_gestor.nome_gestor,
	            tbl_tipo_contrato.tipo_contrato
            FROM
	            tbl_contrato
	            LEFT JOIN tbl_distribuidor ON tbl_contrato.cod_distribuidor = tbl_distribuidor.id
	            LEFT JOIN tbl_gestor ON tbl_contrato.cod_gestor = tbl_gestor.id
	            INNER JOIN tbl_tipo_contrato ON tbl_contrato.cod_tipo_contrato = tbl_tipo_contrato.id";

        Task<IEnumerable<EstruturaContratoViewModel>> GetContratosRebateAsync(string subContratoStatus);

        Task<IEnumerable<ContratoViewModel>> GetAllAsync();

		Task<ContratoViewModel> GetByIdAsync(int id);

	}
}
