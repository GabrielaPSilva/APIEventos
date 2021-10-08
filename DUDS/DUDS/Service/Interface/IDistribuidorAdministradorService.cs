using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    interface IDistribuidorAdministradorService : IGenericOperationsService<DistribuidorAdministradorModel>
    {
        Task<IEnumerable<DistribuidorAdministradorModel>> GetByDistribuidorIdAsync(int id);
    }
}
