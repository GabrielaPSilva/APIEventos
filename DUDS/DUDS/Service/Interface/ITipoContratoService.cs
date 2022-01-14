using DUDS.Models.Tipos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ITipoContratoService : IGenericOperationsService<TipoContratoModel>
    {
        const string QUERY_BASE = @"SELECT
                                 *
                              FROM
	                             tbl_tipo_contrato";

        Task<TipoContratoModel> GetTipoContaExistsBase(string tipoContrato);
        
        Task<IEnumerable<TipoContratoModel>> GetAllAsync();
        
        Task<TipoContratoModel> GetByIdAsync(int id);
    }
}
