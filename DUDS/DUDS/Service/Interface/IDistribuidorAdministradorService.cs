using DUDS.Models.Distribuidor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IDistribuidorAdministradorService : IGenericOperationsService<DistribuidorAdministradorModel>
    {
        const string QUERY_BASE = @"SELECT
                                      tbl_distribuidor_administrador.*,
                                      tbl_distribuidor.NomeDistribuidor,
                                      tbl_administrador.NomeAdministrador
                                   FROM
	                                  tbl_distribuidor_administrador 
                                         INNER JOIN tbl_distribuidor ON tbl_distribuidor_administrador.CodDistribuidor = tbl_distribuidor.Id
                                         INNER JOIN tbl_administrador ON tbl_distribuidor_administrador.CodAdministrador = tbl_administrador.Id";

        Task<IEnumerable<DistribuidorAdministradorViewModel>> GetAllAsync();
        Task<IEnumerable<DistribuidorAdministradorViewModel>> GetDistribuidorByIdAsync(int id);
        Task<DistribuidorAdministradorViewModel> GetByIdAsync(int id);
    }
}
