using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IMovimentacaoPassivoService : IGenericOperationsService<MovimentacaoPassivoModel>
    {
        Task<IEnumerable<MovimentacaoPassivoModel>> AddBulkAsync(List<MovimentacaoPassivoModel> item);

        Task<bool> DeleteByDataRefAsync(DateTime dataMovimentacao);

        Task<IEnumerable<MovimentacaoPassivoModel>> GetByDataEntradaAsync(DateTime? dataMovimentacao);
    }
}
