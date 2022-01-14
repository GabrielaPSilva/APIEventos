using DUDS.Models.Contrato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContratoService : IGenericOperationsService<ContratoModel>
    {
        Task<IEnumerable<EstruturaContratoViewModel>> GetContratosRebateAsync(string subContratoStatus);
    }
}
