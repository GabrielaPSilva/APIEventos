using DUDS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IDistribuidorAdministradorService : IGenericOperationsService<DistribuidorAdministradorModel>
    {
        Task<IEnumerable<DistribuidorAdministradorModel>> GetByDistribuidorIdAsync(int id);
    }
}
