using Dapper;
using DUDS.Models.Passivo;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class OrdemPassivoService : GenericService<OrdemPassivoModel>, IOrdemPassivoService
    {
        public OrdemPassivoService() : base(new OrdemPassivoModel(),"tbl_ordem_passivo")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddAsync(OrdemPassivoModel item)
        {
            if (item == null) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                        var retorno = await connection.ExecuteAsync(sql: query, param: item, transaction: transaction,commandTimeout:180);
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

        public async Task<IEnumerable<OrdemPassivoModel>> AddBulkAsync(List<OrdemPassivoModel> item)
        {
            ConcurrentBag<OrdemPassivoModel> vs = new ConcurrentBag<OrdemPassivoModel>();
            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess };
            await Parallel.ForEachAsync(item, parallelOptions, async (x, cancellationToken) =>
            {
                var result = await AddAsync(x);
                if (!result) { vs.Add(x); }
            }
            );
            return vs;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteByDataRefAsync(DateTime dataEntrada)
        {
            // if (dataRef == null) return false;

            List<OrdemPassivoModel> result = await GetByDataEntradaAsync(dataEntrada: dataEntrada) as List<OrdemPassivoModel>;
            if (result == null) { return false; }
            if (result.Count == 0) { return false; }

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string query = "DELETE FROM tbl_ordem_passivo WHERE data_entrada = @data_entrada";
                        int rowsAffected = await connection.ExecuteAsync(sql: query, param: new { data_entrada = dataEntrada }, transaction: transaction);
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

        public Task<IEnumerable<OrdemPassivoViewModel>> GetAllAsync()
        {
            return GetByDataEntradaAsync(null);
        }

        public async Task<IEnumerable<OrdemPassivoViewModel>> GetByDataEntradaAsync(DateTime? dataEntrada)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IOrdemPassivoService.QUERY_BASE + 
                    @"
                    WHERE
                        (@data_entrada IS NULL OR tbl_ordem_passivo.data_entrada = @data_entrada)
                    ORDER BY
                        tbl_fundo.nome_reduzido,
	                    tbl_investidor.nome_investidor,
                        tbl_ordem_passivo.descricao_operacao";

                return await connection.QueryAsync<OrdemPassivoViewModel>(query, new { data_entrada = dataEntrada });
            }
        }

        public async Task<bool> UpdateAsync(OrdemPassivoModel item)
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
