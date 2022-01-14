using DUDS.Models.Rebate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IErrosPagamentoService : IGenericOperationsService<ErrosPagamentoModel>
    {
        const string QUERY_BASE = @"SELECT
                                        erros_pagamento.*,
                                        fundo.nome_reduzido as NomeFundo
                                    FROM
	                                    tbl_erros_pagamento erros_pagamento
                                            INNER JOIN tbl_fundo fundo ON erros_pagamento.cod_fundo = fundo.id";

        Task<IEnumerable<ErrosPagamentoModel>> GetAllAsync();
        Task<bool> DeleteErrosPagamentoByDataAgendamento(DateTime dataAgendamento);
        Task<bool> AddErrosPagamento(List<ErrosPagamentoModel> errosPagamento);
        Task<IEnumerable<ErrosPagamentoModel>> GetErrosPagamentoByCompetencia(string competencia);
        Task<ErrosPagamentoModel> GetByIdAsync(int id);
    }
}
