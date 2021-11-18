using DUDS.Models;
using DUDS.Models.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IControleRebateService : IGenericOperationsService<ControleRebateModel>
    {
        Task<ControleRebateModel> GetGrupoRebateExistsBase(int codGrupoRebate, string Competencia);
        Task<IEnumerable<ControleRebateModel>> GetByCompetenciaAsync(FiltroModel filtro, int pagina, int itensPorPagina);
        Task<IEnumerable<ControleRebateModel>> GetFiltroControleRebateAsync(int id, string nomeInvestidor, string competencia, string codMellon);
        Task<int> GetCountControleRebateAsync(FiltroModel filtro);
    }
}
