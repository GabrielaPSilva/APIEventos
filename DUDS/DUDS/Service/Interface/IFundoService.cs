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
	            tbl_administrador.nome_administrador,
	            tbl_custodiante.nome_custodiante,
	            tbl_gestor.nome_gestor,
	            tbl_tipo_estrategia.estrategia
            FROM
	            tbl_fundo
		        INNER JOIN tbl_administrador ON tbl_fundo.cod_administrador = tbl_administrador.id
		        INNER JOIN tbl_custodiante ON tbl_fundo.cod_custodiante = tbl_custodiante.id
		        INNER JOIN tbl_gestor ON tbl_fundo.cod_gestor = tbl_gestor.id
		        INNER JOIN tbl_tipo_estrategia ON tbl_fundo.cod_tipo_estrategia = tbl_tipo_estrategia.id";

        Task<FundoViewModel> GetFundoExistsBase(string cnpj);

        Task<IEnumerable<FundoViewModel>> GetAllAsync();

		Task<FundoViewModel> GetByIdAsync(int id);

	}
}
