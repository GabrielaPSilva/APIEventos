using DUDS.Models.Rebate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IGrupoRebateService : IGenericOperationsService<GrupoRebateModel>
    {
        const string QUERY_BASE = 
            @"SELECT
                *
            FROM
                tbl_grupo_rebate";

        Task<IEnumerable<GrupoRebateModel>> GetAllAsync();

        Task<GrupoRebateModel> GetByIdAsync(int id);
    }
}
