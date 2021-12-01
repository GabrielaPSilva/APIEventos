using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IMovimentacaoPassivoService : IGenericOperationsService<MovimentacaoPassivoModel>
    {
        public Task<bool> AddBulkAsync(List<MovimentacaoPassivoModel> item);

        public Task<bool> DeleteByDataRefAsync(DateTime dataMovimentacao);

        public Task<IEnumerable<MovimentacaoPassivoModel>> GetByDataEntradaAsync(DateTime dataMovimentacao);
    }
}
