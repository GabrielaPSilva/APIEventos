using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ICalculoRebateService : IGenericOperationsService<CalculoRebateModel>
    {
        Task<bool> AddBulkAsync(List<CalculoRebateModel> calculoPgtoTaxaAdmPfee);
        Task<IEnumerable<CalculoRebateModel>> GetByCompetenciaAsync(string competencia);
        Task<bool> DeleteByCompetenciaAsync(string competencia);
        Task<IEnumerable<DescricaoCalculoRebateModel>> GetDescricaoRebateAsync(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao, string codCondicaoRemuneracao);
    }
}
