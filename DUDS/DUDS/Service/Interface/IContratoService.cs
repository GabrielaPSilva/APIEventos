using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContratoService: IGenericOperationsService<ContratoModel>
    {
        Task<IEnumerable<EstruturaContratoModel>> GetContratosRebateAsync(string subContratoStatus);
    }
}
