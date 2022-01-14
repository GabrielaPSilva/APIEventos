using DUDS.Models.Contrato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ISubContratoService : IGenericOperationsService<SubContratoModel>
    {
        const string QUERY_BASE = 
            @"
            SELECT 
                sub_contrato.*,
            FROM
                tbl_sub_contrato sub_contrato
                INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrado.cod_contrato";

        Task<IEnumerable<SubContratoViewModel>> GetSubContratoCompletoByIdAsync(int id);

        Task<IEnumerable<SubContratoViewModel>> GetAllAsync();

        Task<SubContratoViewModel> GetByIdAsync(int id);
    }
}
