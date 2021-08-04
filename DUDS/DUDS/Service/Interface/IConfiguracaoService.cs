using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IConfiguracaoService
    {
        Task<bool> GetValidacaoExisteIdOutrasTabelas(int id, string tableName);
    }
}
