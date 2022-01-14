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
        Task<IEnumerable<CalculoRebateModel>> AddBulkAsync(List<CalculoRebateModel> item);
        Task<IEnumerable<CalculoRebateModel>> GetByCompetenciaAsync(string competencia, int codGrupoRebate);
        Task<bool> DeleteByCompetenciaAsync(string competencia);
        Task<IEnumerable<DescricaoCalculoRebateViewModel>> GetDescricaoRebateAsync(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao);

        Task<CalculoRebateModel> GetByIdAsync(Guid id);
        Task<int> GetCountCalculoRebateAsync(FiltroModel filtro);
    }
}
