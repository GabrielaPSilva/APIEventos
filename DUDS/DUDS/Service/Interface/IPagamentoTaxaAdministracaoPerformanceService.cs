using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPagamentoTaxaAdministracaoPerformanceService : IGenericOperationsService<PgtoTaxaAdmPfeeModel>
    {
        Task<IEnumerable<PgtoTaxaAdmPfeeModel>> AddBulkAsync(List<PgtoTaxaAdmPfeeModel> pgtoTaxaAdmimPerf);
        Task<IEnumerable<PgtoTaxaAdmPfeeModel>> GetByCompetenciaAsync(string competencia);
        Task<IEnumerable<PgtoTaxaAdmPfeeModel>> GetByIdsAsync(string competencia, int? codFundo, int? codAdministrador, int? codInvestidorDistribuidor);
        Task<bool> DeleteByCompetenciaAsync(string competencia);
        Task<IEnumerable<PgtoAdmPfeeInvestidorViewModel>> GetPgtoAdmPfeeInvestByCompetenciaAsync(string competencia);

        Task<PgtoTaxaAdmPfeeModel> GetByIdAsync(Guid id);


    }
}
