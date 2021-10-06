using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IAdministradorService
    {
        Task<IEnumerable<AdministradorModel>> GetAdministrador();
        Task<AdministradorModel> GetAdministradorById(int id);
        Task<bool> AddAdministrador(AdministradorModel administrador);
        Task<bool> UpdateAdministrador(AdministradorModel administrador);
        Task<bool> DisableAdministrador(int id);
        Task<bool> ActivateAdministrador(int id);
    }
}
