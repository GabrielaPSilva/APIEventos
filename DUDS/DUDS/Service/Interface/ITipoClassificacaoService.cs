using DUDS.Models.Tipos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ITipoClassificacaoService : IGenericOperationsService<TipoClassificacaoModel>
    {
        const string QUERY_BASE = 
            @"
            SELECT
                *
            FROM
                tbl_tipo_classificacao";

        Task<TipoClassificacaoModel> GetTipoClassificacaoExistsBase(string classificacao);

        Task<IEnumerable<TipoClassificacaoModel>> GetAllAsync();

        Task<TipoClassificacaoModel> GetByIdAsync(int id);
    }
}
