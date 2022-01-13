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
    public class PagamentoServicoService : GenericService<PagamentoServicoModel>, IPagamentoServicoService
    {
        public PagamentoServicoService() : base(new PagamentoServicoModel(),
            "tbl_pagamento_servico")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(PagamentoServicoModel item)
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

        public async Task<IEnumerable<PagamentoServicoModel>> AddPagamentoServico(List<PagamentoServicoModel> pagamentoServicos)
        {
            ConcurrentBag<PagamentoServicoModel> vs = new ConcurrentBag<PagamentoServicoModel>();
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

        public async Task<IEnumerable<PagamentoServicoModel>> GetAllAsync()
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

                return await connection.QueryAsync<PagamentoServicoModel>(query);
            }
        }

        public async Task<PagamentoServicoModel> GetByIdAsync(int id)
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

                return await connection.QueryFirstOrDefaultAsync<PagamentoServicoModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<PagamentoServicoModel>> GetByIdsAsync(string competencia, int codFundo)
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

                return await connection.QueryAsync<PagamentoServicoModel>(query, new { cod_fundo = codFundo, competencia });
            }
        }

        public async Task<IEnumerable<PagamentoServicoModel>> GetPagamentoServicoByCompetencia(string competencia)
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

                return await connection.QueryAsync<PagamentoServicoModel>(query, new { competencia });
            }
        }

        public Task<bool> UpdateAsync(PagamentoServicoModel item)
        {
            throw new NotImplementedException();
        }
    }
}
