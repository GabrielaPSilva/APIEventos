using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class PagamentoServicoService : GenericService<PgtoServicoModel>, IPagamentoServicoService
    {
        public PagamentoServicoService() : base(new PagamentoServicoModel(),
            "tbl_pagamento_servico",
            new List<string> { "'id'" },
            new List<string> { "Id", "NomeFundo" },
            new List<string> { "'id'" },
            new List<string> { "Id", "NomeFundo" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(PgtoServicoModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                        var retorno = await connection.ExecuteAsync(sql: query, param: item, transaction: transaction);
                        transaction.Commit();
                        return retorno > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<IEnumerable<PgtoServicoModel>> AddPagamentoServico(List<PgtoServicoModel> pagamentoServicos)
        {
            ConcurrentBag<PgtoServicoModel> vs = new ConcurrentBag<PgtoServicoModel>();
            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess };
            await Parallel.ForEachAsync(pagamentoServicos, parallelOptions, async (x, cancellationToken) =>
            {
                var result = await AddAsync(x);
                if (!result) { vs.Add(x); }
            }
            );
            return vs;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.DELETE_COMMAND.Replace("TABELA", _tableName);
                        var retorno = await connection.ExecuteAsync(sql: query, param: new { id }, transaction: transaction);
                        transaction.Commit();
                        return retorno > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<bool> DeleteByCompetenciaAsync(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string query = "DELETE FROM tbl_pagamento_servico WHERE competencia = @competencia";
                        var retorno = await connection.ExecuteAsync(sql: query, param: new { competencia }, transaction: transaction);
                        transaction.Commit();
                        return retorno > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PgtoServicoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pagamento_servico.*,
                                        fundo.nome_reduzido as nome_fundo
                                     FROM
	                                    tbl_pagamento_servico pagamento_servico
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pagamento_servico.cod_fundo
                                     ORDER BY
                                        pagamento_servico.competencia,
                                        fundo.nome_reduzido";

                return await connection.QueryAsync<PgtoServicoModel>(query);
            }
        }

        public async Task<PgtoServicoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pagamento_servico.*,
                                        fundo.nome_reduzido as nome_fundo
                                     FROM
	                                    tbl_pagamento_servico pagamento_servico
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pagamento_servico.cod_fundo
                                     WHERE
                                        pagamento_servico.id = @id";

                return await connection.QueryFirstOrDefaultAsync<PgtoServicoModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<PgtoServicoModel>> GetByIdsAsync(string competencia, int codFundo)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pagamento_servico.*,
                                        fundo.nome_reduzido as nome_fundo
                                     FROM
	                                    tbl_pagamento_servico pagamento_servico
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pagamento_servico.cod_fundo
                                     WHERE
                                        pagamento_servico.cod_fundo = @cod_fundo
                                        AND pagamento_servico.competencia = @competencia";

                return await connection.QueryAsync<PgtoServicoModel>(query, new { cod_fundo = codFundo, competencia });
            }
        }

        public async Task<IEnumerable<PgtoServicoModel>> GetPagamentoServicoByCompetencia(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pagamento_servico.*,
                                        fundo.nome_reduzido as nome_fundo
                                     FROM
	                                    tbl_pagamento_servico pagamento_servico
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pagamento_servico.cod_fundo
                                     WHERE
                                        pagamento_servico.competencia = @competencia";

                return await connection.QueryAsync<PgtoServicoModel>(query, new { competencia });
            }
        }

        public Task<bool> UpdateAsync(PgtoServicoModel item)
        {
            throw new NotImplementedException();
        }
    }
}
