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
	            tbl_investidor.nome_investidor,
	            tbl_distribuidor.nome_distribuidor,
	            tbl_administrador.nome_administrador,
	            tbl_tipo_contrato.tipo_contrato,
	            tbl_grupo_rebate.nome_grupo_rebate
            FROM 
	            tbl_investidor_distribuidor
	            INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
	            INNER JOIN tbl_distribuidor_administrador ON tbl_investidor_distribuidor.cod_distribuidor_administrador = tbl_distribuidor_administrador.id
                INNER JOIN tbl_distribuidor ON tbl_distribuidor.id = tbl_distribuidor_administrador.cod_distribuidor
	            INNER JOIN tbl_administrador ON tbl_investidor_distribuidor.cod_administrador = tbl_administrador.id
                INNER JOIN tbl_tipo_contrato ON tbl_investidor_distribuidor.cod_tipo_contrato = tbl_tipo_contrato.id
		        INNER JOIN tbl_grupo_rebate ON tbl_investidor_distribuidor.cod_grupo_rebate = tbl_grupo_rebate.id";

		Task<IEnumerable<InvestidorDistribuidorViewModel>> GetByIdsAsync(int codInvestidor, int codDistribuidor, int codAdministrador);

        Task<IEnumerable<InvestidorDistribuidorModel>> AddInvestidorDistribuidores(List<InvestidorDistribuidorModel> investDistribuidor);

        //Task<IEnumerable<InvestidorDistribuidorViewModel>> GetInvestidorDistribuidorByDataCriacao(DateTime dataCriacao);

		Task<IEnumerable<InvestidorDistribuidorModel>> GetAllAsync();

		Task<IEnumerable<InvestidorDistribuidorViewModel>> GetInvestidorDistribuidorByCodInvestidorAsync(int codInvestidor);

		Task<IEnumerable<InvestidorDistribuidorViewModel>> GetInvestidorDistribuidorByCodAdministradorAsync(int codInvestidorAdministrador);

		Task<InvestidorDistribuidorModel> GetByIdAsync(int id);
	}
}
