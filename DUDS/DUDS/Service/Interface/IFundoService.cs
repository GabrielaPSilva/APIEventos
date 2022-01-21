using DUDS.Models.Fundo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IFundoService : IGenericOperationsService<FundoModel>
    {
		const string QUERY_BASE = 
			@"SELECT 
	            tbl_fundo.*,
	            tbl_administrador.NomeAdministrador,
	            tbl_custodiante.NomeCustodiante,
	            tbl_gestor.NomeGestor,
	            tbl_tipo_estrategia.Estrategia
            FROM
	            tbl_fundo
		        INNER JOIN tbl_administrador ON tbl_fundo.CodAdministrador = tbl_administrador.Id
		        INNER JOIN tbl_custodiante ON tbl_fundo.CodCustodiante = tbl_custodiante.Id
		        INNER JOIN tbl_gestor ON tbl_fundo.CodGestor = tbl_gestor.Id
		        INNER JOIN tbl_tipo_estrategia ON tbl_fundo.CodTipoEstrategia = tbl_tipo_estrategia.Id";

        Task<FundoViewModel> GetFundoExistsBase(string cnpj);

        Task<IEnumerable<FundoViewModel>> GetAllAsync();

		Task<FundoViewModel> GetByIdAsync(int id);

	}
}
