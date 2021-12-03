using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPosicaoClientePassivoService : IGenericOperationsService<PosicaoClientePassivoModel>
    {
        public Task<bool> AddBulkAsync(List<PosicaoClientePassivoModel> item);

        public Task<bool> DeleteByDataRefAsync(DateTime dataRef);

        public Task<bool> DeleteAsync(Guid id);

        // public Task<IEnumerable<PosicaoClientePassivoModel>> GetByDataRefAsync(DateTime dataRef);

        public Task<IEnumerable<PosicaoClientePassivoModel>> GetByParametersAsync(DateTime? dataInicio, DateTime? dataFim, int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor);

        public Task<PosicaoClientePassivoModel> GetMaxValorBrutoAsync(int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor);
    }
}
