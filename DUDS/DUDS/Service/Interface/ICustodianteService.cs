using DUDS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ICustodianteService : IGenericOperationsService<CustodianteModel>
    {
        Task<CustodianteModel> GetCustodianteExistsBase(string cnpj);
    }
}
