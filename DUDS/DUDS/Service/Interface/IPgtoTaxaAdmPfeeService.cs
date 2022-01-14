using DUDS.Models.PgtoTaxaAdmPfee;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPgtoTaxaAdmPfeeService : IGenericOperationsService<PgtoTaxaAdmPfeeModel>
    {
        const string QUERY_BASE = 
            @"SELECT 
	            pgto_adm_pfee.*,
                fundo.nome_reduzido as nome_fundo,
                investidor.nome_investidor
            FROM
	            tbl_pgto_adm_pfee pgto_adm_pfee
                INNER JOIN tbl_fundo fundo ON fundo.id = pgto_adm_pfee.cod_fundo
                INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.id = pgto_adm_pfee.cod_investidor_distribuidor
                INNER JOIN tbl_investidor investidor ON investidor.id = investidor_distribuidor.cod_investidor";

        Task<IEnumerable<PgtoTaxaAdmPfeeModel>> AddBulkAsync(List<PgtoTaxaAdmPfeeModel> pgtoTaxaAdmimPerf);

        Task<IEnumerable<PgtoTaxaAdmPfeeViewModel>> GetByCompetenciaAsync(string competencia);

        Task<bool> DeleteByCompetenciaAsync(string competencia);

        Task<IEnumerable<PgtoAdmPfeeInvestidorViewModel>> GetPgtoAdmPfeeInvestByCompetenciaAsync(string competencia);

        Task<PgtoTaxaAdmPfeeViewModel> GetByIdAsync(Guid id);

        Task<IEnumerable<PgtoTaxaAdmPfeeViewModel>> GetAllAsync();
    }
}
