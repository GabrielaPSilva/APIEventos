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

        const string INSERT_STMT = @"
                                    INSERT INTO tbl_controle_rebate (CodGrupoRebate,Competencia,UsuarioCriacao)
                                    SELECT 
	                                    DISTINCT tbl_investidor_distribuidor.CodGrupoRebate,
	                                    tbl_pgto_adm_pfee.Competencia,
	                                    tbl_calculo_pgto_adm_pfee.UsuarioCriacao
                                    FROM 
	                                    tbl_calculo_pgto_adm_pfee
	                                    inner join tbl_pgto_adm_pfee ON tbl_pgto_adm_pfee.Id = tbl_calculo_pgto_adm_pfee.CodPgtoAdmPfee
	                                    inner join tbl_investidor_distribuidor ON tbl_investidor_distribuidor.Id = tbl_pgto_adm_pfee.CodInvestidorDistribuidor
	                                    inner join tbl_grupo_rebate ON tbl_grupo_rebate.Id = tbl_investidor_distribuidor.CodTipoContrato
                                    WHERE
	                                    tbl_pgto_adm_pfee.Competencia = @Competencia";

        Task<ControleRebateViewModel> GetByIdAsync(int id);
        Task<ControleRebateViewModel> GetGrupoRebateExistsBase(int codGrupoRebate, string Competencia);
        Task<IEnumerable<ControleRebateViewModel>> GetFiltroControleRebateAsync(int grupoRebate, string investidor, string competencia);
        Task<int> GetCountControleRebateAsync(FiltroModel filtro);
        Task<IEnumerable<ControleRebateViewModel>> GetByCompetenciaAsync(FiltroModel filtro);
    }
}
