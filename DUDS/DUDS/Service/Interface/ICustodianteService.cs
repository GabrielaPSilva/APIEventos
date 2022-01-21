using DUDS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ICustodianteService : IGenericOperationsService<CustodianteModel>
    {
		const string QUERY_BASE = @"SELECT
                                        *
                                    FROM
	                                    tbl_custodiante";

        Task<IEnumerable<CustodianteModel>> GetAllAsync();
        Task<CustodianteModel> GetByIdAsync(int id);
        Task<CustodianteModel> GetCustodianteExistsBase(string cnpj);
    }
}
