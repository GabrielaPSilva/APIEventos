using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPosicaoClientePassivoService : IGenericOperationsService<PosicaoClienteModel>
    {
        Task<IEnumerable<PosicaoClienteModel>> AddBulkAsync(List<PosicaoClienteModel> item);

        Task<bool> DeleteByDataRefAsync(DateTime dataRef);

        Task<bool> DeleteAsync(Guid id);

        // public Task<IEnumerable<PosicaoClientePassivoModel>> GetByDataRefAsync(DateTime dataRef);

        Task<IEnumerable<PosicaoClienteModel>> GetByParametersAsync(DateTime? dataInicio, DateTime? dataFim, int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor);

        Task<PosicaoClienteModel> GetMaxValorBrutoAsync(int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor);
    }
}
