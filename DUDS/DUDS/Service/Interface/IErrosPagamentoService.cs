using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IErrosPagamentoService
    {
        Task<IEnumerable<ErrosPagamentoModel>> GetErrosPagamento();
        Task<ErrosPagamentoModel> GetErrosPagamentoById(int id);
        Task<IEnumerable<ErrosPagamentoModel>> GetErrosPagamentoByCompetenciaDataAgendamento(string competencia, DateTime? dataAgendamento);
        Task<bool> AddErrosPagamento(List<ErrosPagamentoModel> errosPagamento);
        Task<bool> DeleteErrosPagamentoByDataAgendamento(DateTime dataAgendamento);
        Task<bool> DeleteErrosPagamentoById(int id);
    }
}
