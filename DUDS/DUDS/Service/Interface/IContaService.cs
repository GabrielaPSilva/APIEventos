using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContaService : IGenericOperationsService<ContaModel>
    {
        Task<GestorModel> GetGestorExistsBase(int codFundo, int codInvestidor, int codTipoConta);
        Task<IEnumerable<ContaModel>> GetFundoByIdAsync(int id);
    }
}
