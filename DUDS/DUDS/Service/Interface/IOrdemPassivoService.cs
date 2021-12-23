using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IOrdemPassivoService : IGenericOperationsService<OrdemPassivoModel>
    {
        Task<bool> DeleteByDataRefAsync(DateTime dataRef);

        Task<IEnumerable<OrdemPassivoModel>> GetByDataEntradaAsync(DateTime? dataEntrada);

        Task<IEnumerable<OrdemPassivoModel>> AddBulkAsync(List<OrdemPassivoModel> item);
    }
}
