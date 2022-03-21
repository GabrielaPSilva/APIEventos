using DUDS.Models.Rebate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IErrosPagamentoService : IGenericOperationsService<ErrosPagamentoModel>
    {
        const string QUERY_BASE = @"SELECT
                                        tbl_erros_pgto.*,
                                        tbl_fundo.NomeReduzido as NomeFundo
                                    FROM
	                                    tbl_erros_pgto
                                            INNER JOIN tbl_fundo ON tbl_erros_pgto.CodFundo = tbl_fundo.Id";

        Task<IEnumerable<ErrosPagamentoModel>> GetAllAsync();
        Task<bool> DeleteErrosPagamentoByDataAgendamento(DateTime dataAgendamento);
        Task<IEnumerable<ErrosPagamentoModel>> AddErrosPagamento(List<ErrosPagamentoModel> item);
        Task<IEnumerable<ErrosPagamentoModel>> GetErrosPagamentoByCompetencia(string competencia);
        Task<ErrosPagamentoModel> GetByIdAsync(int id);
    }
}
