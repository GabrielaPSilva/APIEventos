using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class MovimentacaoPassivoService : GenericService<MovimentacaoPassivoModel>, IMovimentacaoPassivoService
    {
        public MovimentacaoPassivoService() : base(new MovimentacaoPassivoModel(),
                   "tbl_movimentacao_nota")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddAsync(MovimentacaoPassivoModel item)
        {
            if (item == null) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                        var retorno = await connection.ExecuteAsync(sql: query, param: item, transaction: transaction, commandTimeout: 180);
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

        public async Task<IEnumerable<MovimentacaoPassivoModel>> AddBulkAsync(List<MovimentacaoPassivoModel> item)
        {

            ConcurrentBag<MovimentacaoPassivoModel> vs = new ConcurrentBag<MovimentacaoPassivoModel>();
            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess };
            await Parallel.ForEachAsync(item, parallelOptions, async (x, cancellationToken) =>
            {
                var result = await AddAsync(x);
                if (!result) { vs.Add(x); }
            }
            );
            return vs;
            /*
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                _ = Parallel.ForEach(item, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, x =>
                {
                    var result = AddAsync(x);
                    if (result.Result) { vs.Add(result.Result); }
                });

                // return GetInvestidorByDataCriacao(investidor.FirstOrDefault().DataCriacao).Result.ToArray().Length == investidor.Count;
                return vs.Count == item.Count;
            }
            */
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteByDataRefAsync(DateTime dataMovimentacao)
        {
            List<OrdemPassivoModel> result = await GetByDataEntradaAsync(dataMovimentacao: dataMovimentacao) as List<OrdemPassivoModel>;
            if (result == null) return false;
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string query = "DELETE FROM tbl_movimentacao_passivo WHERE data_movimentacao = @data_movimentacao";
                        int rowsAffected = await connection.ExecuteAsync(sql: query, param: new { data_movimentacao = dataMovimentacao }, transaction: transaction);
                        transaction.Commit();
                        return rowsAffected > 0 && rowsAffected == result.Count;
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
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MovimentacaoPassivoModel>> GetAllAsync()
        {
            return GetByDataEntradaAsync(null);
        }

        public async Task<IEnumerable<MovimentacaoPassivoModel>> GetByDataEntradaAsync(DateTime? dataMovimentacao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"
                            SELECT
	                            tbl_movimentacao_passivo.*,
	                            tbl_fundo.nome_reduzido AS nome_fundo,
	                            tbl_investidor.nome_investidor,
	                            tbl_administrador.nome_administrador
                            FROM
	                            tbl_movimentacao_passivo
		                        INNER JOIN tbl_fundo ON tbl_movimentacao_passivo.cod_fundo = tbl_fundo.id
                                INNER JOIN tbl_investidor_distribuidor ON tbl_movimentacao_passivo.cod_investidor_distribuidor = tbl_investidor_distribuidor.id
		                        INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
		                        INNER JOIN tbl_administrador ON tbl_ordem_passivo.cod_administrador = tbl_administrador.id
                                
                            WHERE
                                (@data_movimentacao IS NULL OR tbl_ordem_passivo.data_movimentacao = @data_movimentacao)
                            ORDER BY
                                tbl_fundo.nome_reduzido,
	                            tbl_investidor.nome_investidor,
                                tbl_ordem_passivo.tipo_movimentacao";

                return await connection.QueryAsync<MovimentacaoPassivoModel>(query, new { data_movimentacao = dataMovimentacao });
            }
        }

        public Task<MovimentacaoPassivoModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateAsync(MovimentacaoPassivoModel item)
        {
            if (item == null) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.UPDATE_COMMAND.Replace("TABELA", _tableName);
                        List<string> str = new List<string>();
                        for (int i = 0; i < _propertiesUpdate.Count; i++)
                        {
                            str.Add(_fieldsUpdate[i] + " = " + _propertiesUpdate[i]);
                        }
                        query = query.Replace("VALORES", String.Join(",", str));
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
    }
}
