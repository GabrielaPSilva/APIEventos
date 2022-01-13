using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPagamentoServicoService : IGenericOperationsService<PgtoServicoModel>
    {
        Task<bool> DeleteByCompetenciaAsync(string competencia);
        Task<IEnumerable<PgtoServicoModel>> GetByIdsAsync(string competencia, int codFundo);
        Task<IEnumerable<PgtoServicoModel>> AddPagamentoServico(List<PgtoServicoModel> pagamentoServicos);
        Task<IEnumerable<PgtoServicoModel>> GetPagamentoServicoByCompetencia(string competencia);
    }
}
