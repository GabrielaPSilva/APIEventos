using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ITipoContaService : IGenericOperationsService<TipoContaModel>
    {
        Task<TipoContaModel> GetTipoContaExistsBase(string tipoConta, string descricaoConta);
    }
}
