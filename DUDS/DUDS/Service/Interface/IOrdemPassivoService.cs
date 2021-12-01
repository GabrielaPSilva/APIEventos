using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IOrdemPassivoService : IGenericOperationsService<OrdemPassivoModel>
    {
        public Task<bool> DeleteByDataRefAsync(DateTime dataRef);

        public Task<IEnumerable<OrdemPassivoModel>> GetByDataEntradaAsync(DateTime dataEntrada);

        public Task<bool> AddBulkAsync(List<OrdemPassivoModel> item);
    }
}
