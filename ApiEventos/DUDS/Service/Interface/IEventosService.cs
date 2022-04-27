using DUDS.Models.Administrador;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IEventosService 
    {
        Task<IEnumerable<EventosModel>> GetAllAsync();
        Task<EventosModel> GetByIdAsync(int id);
        Task<IEnumerable<EventosModel>> GetByIdPaisAsync(int idPais);
        Task<bool> AddAsync(int idPais, string nomeEvento, string observacao, string dataEvento);
        Task<bool> UpdateAsync(EventosModel eventos);
        Task<bool> DeleteAsync(int id);
    }
}
