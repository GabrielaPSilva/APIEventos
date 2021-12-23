using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPagamentoServicoService : IGenericOperationsService<PagamentoServicoModel>
    {
        Task<bool> DeleteByCompetenciaAsync(string competencia);
        Task<IEnumerable<PagamentoServicoModel>> GetByIdsAsync(string competencia, int codFundo);
        Task<IEnumerable<PagamentoServicoModel>> AddPagamentoServico(List<PagamentoServicoModel> pagamentoServicos);
        Task<IEnumerable<PagamentoServicoModel>> GetPagamentoServicoByCompetencia(string competencia);
    }
}
