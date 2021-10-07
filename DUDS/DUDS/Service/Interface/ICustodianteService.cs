using DUDS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ICustodianteService
    {
        Task<IEnumerable<CustodianteModel>> GetCustodiante();
        Task<CustodianteModel> GetCustodianteById(int id);
        Task<CustodianteModel> GetCustodianteExistsBase(string cnpj);
        Task<bool> AddCustodiante(CustodianteModel gestor);
        Task<bool> UpdateCustodiante(CustodianteModel gestor);
        Task<bool> DisableCustodiante(int id);
        Task<bool> ActivateCustodiante(int id);
    }
}
