using DUDS.Models.Administrador;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IEventosService 
    {
        Task<IEnumerable<EventosModel>> GetAllAsync();
        Task<EventosModel> GetByIdAsync(int id);
        Task<IEnumerable<EventosModel>> GetByIdPaisAsync(int idPais);
        Task<bool> AddAsync(EventosModel eventos);
        Task<bool> UpdateAsync(EventosModel eventos);
        Task<bool> DeleteAsync(int id);
    }
}
