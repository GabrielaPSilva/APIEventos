using DUDS.Models.Distribuidor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IDistribuidorService : IGenericOperationsService<DistribuidorModel>
    {
        const string QUERY_BASE = @"SELECT
                                        distribuidor.*,
                                        tipo_classificacao.classificacao
                                    FROM
	                                    tbl_distribuidor distribuidor
                                            INNER JOIN tbl_tipo_classificacao tipo_classificacao 
                                                ON distribuidor.cod_tipo_classificacao = tipo_classificacao.id";

        Task<IEnumerable<DistribuidorViewModel>> GetAllAsync();
        Task<DistribuidorViewModel> GetByIdAsync(int id);
        Task<DistribuidorViewModel> GetDistribuidorExistsBase(string cnpj);
    }
}
