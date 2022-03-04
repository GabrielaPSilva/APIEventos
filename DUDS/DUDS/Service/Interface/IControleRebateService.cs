using DUDS.Models.Filtros;
using DUDS.Models.Rebate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IControleRebateService : IGenericOperationsService<ControleRebateModel>
    {
        const string QUERY_BASE = @"SELECT 
	                                     tbl_controle_rebate.*,
	                                     tbl_grupo_rebate.NomeGrupoRebate
                                      FROM
	                                     tbl_controle_rebate
                                            INNER JOIN tbl_grupo_rebate ON tbl_controle_rebate.CodGrupoRebate = tbl_grupo_rebate.Id";

        Task<ControleRebateViewModel> GetByIdAsync(int id);
        Task<ControleRebateViewModel> GetGrupoRebateExistsBase(int codGrupoRebate, string Competencia);
        Task<IEnumerable<ControleRebateViewModel>> GetFiltroControleRebateAsync(int grupoRebate, string investidor, string competencia);
        Task<int> GetCountControleRebateAsync(FiltroModel filtro);
        Task<IEnumerable<ControleRebateViewModel>> GetByCompetenciaAsync(FiltroModel filtro);

        Task<IEnumerable<ControleRebateModel>> AddBulkAsync(List<ControleRebateModel> item);
    }
}
