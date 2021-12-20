using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IInvestidorService : IGenericOperationsService<InvestidorModel>
    {
        Task<InvestidorModel> GetInvestidorExistsBase(string cnpj);
        Task<IEnumerable<InvestidorModel>> GetInvestidorByDataCriacao(DateTime dataCriacao);
        Task<IEnumerable<InvestidorModel>> AddInvestidores(List<InvestidorModel> investidores);
    }
}
