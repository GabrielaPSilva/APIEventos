using DUDS.Models.Rebate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IErrosPagamentoService : IGenericOperationsService<ErrosPagamentoModel>
    {
        Task<IEnumerable<ErrosPagamentoModel>> GetErrosPagamentoByCompetencia(string competencia);
        Task<bool> DeleteErrosPagamentoByDataAgendamento(DateTime dataAgendamento);
        Task<bool> AddErrosPagamento(List<ErrosPagamentoModel> errosPagamento);
    }
}
