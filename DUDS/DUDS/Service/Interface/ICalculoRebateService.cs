using DUDS.Models;
using DUDS.Models.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ICalculoRebateService : IGenericOperationsService<CalculoRebateModel>
    {
        Task<bool> AddBulkAsync(List<CalculoRebateModel> calculoPgtoTaxaAdmPfee);
        Task<IEnumerable<CalculoRebateModel>> GetByCompetenciaAsync(string competencia, int codGrupoRebate);
        Task<bool> DeleteByCompetenciaAsync(string competencia);
        Task<IEnumerable<DescricaoCalculoRebateModel>> GetDescricaoRebateAsync(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao, string codCondicaoRemuneracao);

        Task<CalculoRebateModel> GetByIdAsync(Guid id);
        Task<int> GetCountCalculoRebateAsync(FiltroModel filtro);
    }
}
