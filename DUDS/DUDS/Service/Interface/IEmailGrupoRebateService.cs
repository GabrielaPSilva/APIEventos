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
                                        *
                                    FROM
                                        tbl_email_grupo_rebate";

        Task<EmailGrupoRebateViewModel> GetByIdAsync(int id);
        Task<IEnumerable<EmailGrupoRebateViewModel>> GetAllAsync();
    }
}
