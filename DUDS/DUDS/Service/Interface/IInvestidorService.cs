using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IInvestidorService
    {
        Task<IEnumerable<InvestidorModel>> GetInvestidor();
        Task<InvestidorModel> GetInvestidorById(int id);
        Task<InvestidorModel> GetInvestidorExistsBase(string cnpj);
        Task<bool> AddInvestidor(InvestidorModel investidor);
        Task<bool> UpdateInvestidor(InvestidorModel investidor);
        Task<bool> DisableInvestidor(int id);
        Task<bool> ActivateInvestidor(int id);
    }
}
