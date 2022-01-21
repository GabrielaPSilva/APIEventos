using DUDS.Models.Distribuidor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IDistribuidorService : IGenericOperationsService<DistribuidorModel>
    {
        const string QUERY_BASE = @"SELECT
                                        tbl_distribuidor.*,
                                        tbl_tipo_classificacao.Classificacao
                                    FROM
	                                    tbl_distribuidor
                                            INNER JOIN tbl_tipo_classificacao 
                                                ON tbl_distribuidor.CodTipoClassificacao = tbl_tipo_classificacao.Id";

        Task<IEnumerable<DistribuidorViewModel>> GetAllAsync();
        Task<DistribuidorViewModel> GetByIdAsync(int id);
        Task<DistribuidorViewModel> GetDistribuidorExistsBase(string cnpj);
    }
}
