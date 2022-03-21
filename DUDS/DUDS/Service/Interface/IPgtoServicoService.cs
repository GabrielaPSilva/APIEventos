using DUDS.Models.PgtoServico;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPgtoServicoService : IGenericOperationsService<PgtoServicoModel>
    {
        const string QUERY_BASE =
            @"SELECT 
	            tbl_pgto_servico.*,
                fundo.NomeReduzido as NomeFundo
            FROM
	            tbl_pgto_servico
                INNER JOIN tbl_fundo fundo ON fundo.Id = tbl_pgto_servico.CodFundo";

        Task<bool> DeleteByCompetenciaAsync(string competencia);

        Task<IEnumerable<PgtoServicoViewModel>> GetAllAsync();

        Task<PgtoServicoViewModel> GetByIdAsync(int id);

        Task<IEnumerable<PgtoServicoViewModel>> GetByIdsAsync(string competencia, int codFundo);

        Task<IEnumerable<PgtoServicoModel>> AddPgtoServico(List<PgtoServicoModel> pagamentoServicos);
        
        Task<IEnumerable<PgtoServicoViewModel>> GetPgtoServicoByCompetencia(string competencia);
    }
}
