using DUDS.Models.Rebate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IEmailGrupoRebateService: IGenericOperationsService<EmailGrupoRebateModel>
    {
        const string QUERY_BASE = @"SELECT 
	                                    tbl_email_grupo_rebate.*,
	                                    tbl_grupo_rebate.nome_grupo_rebate
                                    FROM 
	                                    tbl_email_grupo_rebate
		                                    INNER JOIN tbl_grupo_rebate ON tbl_email_grupo_rebate.cod_grupo_rebate = tbl_grupo_rebate.id";

        Task<EmailGrupoRebateViewModel> GetByIdAsync(int id);
        Task<IEnumerable<EmailGrupoRebateViewModel>> GetAllAsync();
        Task<IEnumerable<EmailGrupoRebateViewModel>> GetByGrupoRebateAsync(int codGrupoRebate);
    }
}
