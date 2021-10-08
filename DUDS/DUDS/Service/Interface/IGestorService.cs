using DUDS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IGestorService : IGenericOperationsService<GestorModel>
    {
        Task<GestorModel> GetGestorExistsBase(string cnpj);
    }
}
