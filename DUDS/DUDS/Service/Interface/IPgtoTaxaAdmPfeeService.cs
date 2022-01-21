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
	            tbl_pgto_adm_pfee.*,
                tbl_fundo.NomeReduzido as NomeFundo,
                tbl_investidor.NomeInvestidor
            FROM
	            tbl_pgto_adm_pfee
                INNER JOIN tbl_fundo ON tbl_fundo.Id = tbl_pgto_adm_pfee.CodFundo
                INNER JOIN tbl_investidor_distribuidor ON tbl_investidor_distribuidor.Id = tbl_pgto_adm_pfee.CodInvestidorDistribuidor
                INNER JOIN tbl_investidor ON tbl_investidor.Id = tbl_investidor_distribuidor.CodInvestidor";

        Task<IEnumerable<PgtoTaxaAdmPfeeModel>> AddBulkAsync(List<PgtoTaxaAdmPfeeModel> pgtoTaxaAdmimPerf);

        Task<IEnumerable<PgtoTaxaAdmPfeeViewModel>> GetByCompetenciaAsync(string competencia);

        Task<bool> DeleteByCompetenciaAsync(string competencia);

        Task<IEnumerable<PgtoAdmPfeeInvestidorViewModel>> GetPgtoAdmPfeeInvestByCompetenciaAsync(string competencia);

        Task<PgtoTaxaAdmPfeeViewModel> GetByIdAsync(Guid id);

        Task<IEnumerable<PgtoTaxaAdmPfeeViewModel>> GetAllAsync();
        Task<IEnumerable<PgtoTaxaAdmPfeeViewModel>> GetByParametersAsync(string competencia, int? codFundo, int? codAdministrador, int? codInvestidorDistribuidor);
    }
}
