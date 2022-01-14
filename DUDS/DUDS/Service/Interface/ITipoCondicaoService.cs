using DUDS.Models.Tipos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ITipoCondicaoService : IGenericOperationsService<TipoCondicaoModel>
    {
        const string QUERY_BASE = 
            @"
            SELECT
                *
            FROM
                tbl_tipo_condicao";

        Task<TipoCondicaoModel> GetTipoCondicaoExistsBase(string tipoCondicao);

        Task<IEnumerable<TipoCondicaoModel>> GetAllAsync();

        Task<TipoCondicaoModel> GetByIdAsync(int id);
    }
}
