using DUDS.Models.Distribuidor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IDistribuidorAdministradorService : IGenericOperationsService<DistribuidorAdministradorModel>
    {
        const string QUERY_BASE = @"SELECT
                                      distr_adm.*,
                                      distribuidor.nome_distribuidor,
                                      administrador.nome_administrador
                                   FROM
	                                  tbl_distribuidor_administrador distr_adm
                                         INNER JOIN tbl_distribuidor distribuidor ON distr_adm.cod_distribuidor = distribuidor.id
                                         INNER JOIN tbl_administrador administrador ON distr_adm.cod_administrador = administrador.id";

        Task<IEnumerable<DistribuidorAdministradorViewModel>> GetAllAsync();
        Task<IEnumerable<DistribuidorAdministradorViewModel>> GetDistribuidorByIdAsync(int id);
        Task<DistribuidorAdministradorViewModel> GetByIdAsync(int id);
    }
}
