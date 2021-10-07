using DUDS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IGestorService
    {
        Task<IEnumerable<GestorModel>> GetGestor();
        Task<GestorModel> GetGestorById(int id);
        Task<GestorModel> GetGestorExistsBase(string cnpj);
        Task<bool> AddGestor(GestorModel gestor);
        Task<bool> UpdateGestor(GestorModel gestor);
        Task<bool> DisableGestor(int id);
        Task<bool> ActivateGestor(int id);
    }
}
