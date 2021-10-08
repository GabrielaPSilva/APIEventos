using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IDistribuidorService
    {
        Task<IEnumerable<DistribuidorModel>> GetDistribuidor();
        Task<DistribuidorModel> GetDistribuidorById(int id);
        Task<DistribuidorModel> GetDistribuidorExistsBase(string cnpj);
        Task<bool> AddDistribuidor(DistribuidorModel distribuidor);
        Task<bool> AddDistribuidorAdministrador(DistribuidorAdministradorModel distribuidorAdministrador);
        Task<bool> UpdateDistribuidor(DistribuidorModel distribuidor);
        Task<bool> UpdateDistribuidorAdministrador(DistribuidorAdministradorModel distribuidorAdministrador);
        Task<bool> DisableDistribuidorr(int id);
        Task<bool> ActivateDistribuidor(int id);
    }
}
