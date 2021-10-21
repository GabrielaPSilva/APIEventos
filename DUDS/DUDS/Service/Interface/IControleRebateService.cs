using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IControleRebateService : IGenericOperationsService<ControleRebateModel>
    {
        Task<ControleRebateModel> GetGrupoRebateExistsBase(int codGrupoRebate, string Competencia);
    }
}
