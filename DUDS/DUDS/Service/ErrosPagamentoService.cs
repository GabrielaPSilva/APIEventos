using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ErrosPagamentoService : IErrosPagamentoService
    {

        public ErrosPagamentoService()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddErrosPagamento(List<ErrosPagamentoModel> errosPagamento)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                Parallel.ForEach(errosPagamento, x =>
                {
                    const string query = @"
                                INSERT INTO
                                    tbl_erros_pagamento
                                VALUES
                                   (@DataAgendamento, @CodFundo, @TipoDespesa, @ValorBruto, @CpfCnpjFavorecido, @Favorecido, 
                                    @ContaFavorecida, @Competencia, @Status, @CnpjFundoInvestidor, @MensagemErro)";
                    connection.ExecuteAsync(query, x);
                });

                return GetErrosPagamentoByCompetenciaDataAgendamento(errosPagamento.FirstOrDefault().Competencia, errosPagamento.FirstOrDefault().DataAgendamento).Result.ToArray().Length == errosPagamento.Count;
            }
        }

        public async Task<bool> DeleteErrosPagamentoByDataAgendamento(DateTime dataAgendamento)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                DELETE FROM
                                    tbl_erros_pagamento
                                WHERE
                                   data_agendamento = @dataAgendamento";
                return await connection.ExecuteAsync(query, new { dataAgendamento }) > 0;
            }
        }

        public async Task<bool> DeleteErrosPagamentoById(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                DELETE FROM
                                    tbl_erros_pagamento
                                WHERE
                                   id = @id";

                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<ErrosPagamentoModel>> GetErrosPagamento()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                erros_pagamento.*,
                                fundo.nome_reduzido as NomeFundo
                              FROM
	                            tbl_erros_pagamento erros_pagamento
                                inner join tbl_fundo fundo on erros_pagamento.cod_fundo = fundo.id
                            ORDER BY    
                                erros_pagamento.data_agendamento";

                return await connection.QueryAsync<ErrosPagamentoModel>(query);
            }
        }

        public async Task<IEnumerable<ErrosPagamentoModel>> GetErrosPagamentoByCompetenciaDataAgendamento(string competencia, DateTime? dataAgendamento)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                erros_pagamento.*,
                                fundo.nome_reduzido as NomeFundo
                              FROM
	                            tbl_erros_pagamento erros_pagamento
                                inner join tbl_fundo fundo on erros_pagamento.cod_fundo = fundo.id
                            WHERE
                                erros_pagamento.competencia = @competencia OR
                                erros_pagamento.data_agendamento = @dataAgendamento";

                return await connection.QueryAsync<ErrosPagamentoModel>(query, new { competencia, dataAgendamento });
            }
        }

        public async Task<ErrosPagamentoModel> GetErrosPagamentoById(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                erros_pagamento.*,
                                fundo.nome_reduzido as NomeFundo
                              FROM
	                            tbl_erros_pagamento erros_pagamento
                                inner join tbl_fundo fundo on erros_pagamento.cod_fundo = fundo.id
                            WHERE
                                erros_pagamento.id = @id";

                return await connection.QueryFirstAsync<ErrosPagamentoModel>(query, new { id });
            }
        }

    }
}
