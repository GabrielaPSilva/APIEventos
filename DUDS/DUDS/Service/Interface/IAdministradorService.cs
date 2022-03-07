using DUDS.Models.Administrador;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IAdministradorService : IGenericOperationsService<AdministradorModel>
    {
        const string QUERY_BASE = @"SELECT
                                        *
                                    FROM
                                        tbl_administrador";

        Task<IEnumerable<AdministradorModel>> GetAllAsync();
        Task<AdministradorModel> GetByIdAsync(int id);
        Task<AdministradorModel> GetAdministradorExistsBase(string cnpj, string nome);
    }
}
