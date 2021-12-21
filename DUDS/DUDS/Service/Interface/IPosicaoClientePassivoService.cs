using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPosicaoClientePassivoService : IGenericOperationsService<PosicaoClientePassivoModel>
    {
        Task<IEnumerable<PosicaoClientePassivoModel>> AddBulkAsync(List<PosicaoClientePassivoModel> item);

        Task<bool> DeleteByDataRefAsync(DateTime dataRef);

        Task<bool> DeleteAsync(Guid id);

        // public Task<IEnumerable<PosicaoClientePassivoModel>> GetByDataRefAsync(DateTime dataRef);

        Task<IEnumerable<PosicaoClientePassivoModel>> GetByParametersAsync(DateTime? dataInicio, DateTime? dataFim, int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor);

        Task<PosicaoClientePassivoModel> GetMaxValorBrutoAsync(int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor);
    }
}
