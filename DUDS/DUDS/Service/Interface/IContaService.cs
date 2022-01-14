using DUDS.Models.Conta;
using DUDS.Models.Gestor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContaService : IGenericOperationsService<ContaModel>
    {
        Task<ContaModel> GetContaExistsBase(int codFundo, int codInvestidor, int codTipoConta);
        Task<IEnumerable<ContaModel>> GetFundoByIdAsync(int id);
    }
}
