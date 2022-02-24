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
	                                    tbl_grupo_rebate.NomeGrupoRebate
                                    FROM 
	                                    tbl_email_grupo_rebate
		                                    INNER JOIN tbl_grupo_rebate ON tbl_email_grupo_rebate.CodGrupoRebate = tbl_grupo_rebate.Id";

        Task<EmailGrupoRebateViewModel> GetByIdAsync(int id);
        Task<IEnumerable<EmailGrupoRebateViewModel>> GetAllAsync();
        Task<IEnumerable<EmailGrupoRebateViewModel>> GetEmailByGrupoRebateAsync(int codGrupoRebate);
    }
}
