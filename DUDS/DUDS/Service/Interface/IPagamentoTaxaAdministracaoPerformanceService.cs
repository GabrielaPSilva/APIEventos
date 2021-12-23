using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPagamentoTaxaAdministracaoPerformanceService : IGenericOperationsService<PagamentoTaxaAdminPfeeModel>
    {
        Task<IEnumerable<PagamentoTaxaAdminPfeeModel>> AddBulkAsync(List<PagamentoTaxaAdminPfeeModel> pgtoTaxaAdmimPerf);
        Task<IEnumerable<PagamentoTaxaAdminPfeeModel>> GetByCompetenciaAsync(string competencia);
        Task<IEnumerable<PagamentoTaxaAdminPfeeModel>> GetByIdsAsync(string competencia, int codFundo, int codAdministrador, int codInvestidorDistribuidor);
        Task<bool> DeleteByCompetenciaAsync(string competencia);
        Task<IEnumerable<PagamentoAdmPfeeInvestidorModel>> GetPgtoAdmPfeeInvestByCompetenciaAsync(string competencia);

        Task<PagamentoTaxaAdminPfeeModel> GetByIdAsync(Guid id);


    }
}
