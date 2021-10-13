using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContratoFundoService : IGenericOperationsService<ContratoFundoModel>
    {
        Task<IEnumerable<ContratoFundoModel>> GetSubContratoByIdAsync(int id);
    }
}
