using DUDS.Models.Tipos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ITipoEstrategiaService : IGenericOperationsService<TipoEstrategiaModel>
    {
        const string QUERY_BASE = @"
                                    SELECT 
                                        *
                                    FROM
	                                    tbl_tipo_estrategia";

        Task<TipoEstrategiaModel> GetTipoEstrategiaExistsBase(string estrategia);

        Task<IEnumerable<TipoEstrategiaModel>> GetAllAsync();

        Task<TipoEstrategiaModel> GetByIdAsync(int id);
    }
}
