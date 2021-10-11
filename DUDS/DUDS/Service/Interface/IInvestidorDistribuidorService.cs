using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IInvestidorDistribuidorService : IGenericOperationsService<InvestidorDistribuidorModel>
    {
        Task<IEnumerable<InvestidorDistribuidorModel>> GetByIdsAsync(int codInvestidor, int codDistribuidor, int codAdministrador);
    }
}
