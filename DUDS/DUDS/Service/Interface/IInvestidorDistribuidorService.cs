using DUDS.Models;
using DUDS.Models.Investidor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IInvestidorDistribuidorService : IGenericOperationsService<InvestidorDistribuidorModel>
    {
		const string QUERY_BASE = 
			@"SELECT 
	            tbl_investidor_distribuidor.*,
	            tbl_investidor.NomeInvestidor,
	            tbl_distribuidor.NomeDistribuidor,
	            tbl_administrador.NomeAdministrador,
	            tbl_tipo_contrato.TipoContrato,
	            tbl_grupo_rebate.NomeGrupoRebate
            FROM 
	            tbl_investidor_distribuidor
	            INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.CodInvestidor = tbl_investidor.Id
	            INNER JOIN tbl_distribuidor_administrador ON tbl_investidor_distribuidor.CodDistribuidorAdministrador = tbl_distribuidor_administrador.Id
                INNER JOIN tbl_distribuidor ON tbl_distribuidor.Id = tbl_distribuidor_administrador.CodDistribuidor
	            INNER JOIN tbl_administrador ON tbl_investidor_distribuidor.CodAdministrador = tbl_administrador.Id
                INNER JOIN tbl_tipo_contrato ON tbl_investidor_distribuidor.CodTipoContrato = tbl_tipo_contrato.Id
		        INNER JOIN tbl_grupo_rebate ON tbl_investidor_distribuidor.CodGrupoRebate = tbl_grupo_rebate.Id";

		Task<IEnumerable<InvestidorDistribuidorViewModel>> GetByIdsAsync(int codInvestidor, int codDistribuidor, int codAdministrador);

        Task<IEnumerable<InvestidorDistribuidorModel>> AddInvestidorDistribuidores(List<InvestidorDistribuidorModel> investDistribuidor);

        //Task<IEnumerable<InvestidorDistribuidorViewModel>> GetInvestidorDistribuidorByDataCriacao(DateTime dataCriacao);

		Task<IEnumerable<InvestidorDistribuidorViewModel>> GetAllAsync();

		Task<IEnumerable<InvestidorDistribuidorViewModel>> GetInvestidorDistribuidorByCodInvestidorAsync(int codInvestidor);

		Task<IEnumerable<InvestidorDistribuidorViewModel>> GetInvestidorDistribuidorByCodAdministradorAsync(int codInvestidorAdministrador);

		Task<InvestidorDistribuidorViewModel> GetByIdAsync(int id);
	}
}
