using DUDS.Models.Tipos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ITipoContaService : IGenericOperationsService<TipoContaModel>
    {
        const string QUERY_BASE = 
            @"SELECT
                *
            FROM
	            tbl_tipo_conta";

        Task<TipoContaModel> GetTipoContaExistsBase(string tipoConta, string descricaoConta);

        Task<IEnumerable<TipoContaModel>> GetAllAsync();

        Task<TipoContaModel> GetByIdAsync(int id);
    }
}
