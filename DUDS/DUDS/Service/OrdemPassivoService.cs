using Dapper;
using DUDS.Models.Passivo;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(OrdemPassivoModel item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrdemPassivoModel>> AddBulkAsync(List<OrdemPassivoModel> item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlBulkCopy bulkCopy = new SqlBulkCopy(connection: (SqlConnection)connection,
                            copyOptions: SqlBulkCopyOptions.Default,
                            externalTransaction: (SqlTransaction)transaction);

                        var dataTable = ToDataTable(item);
                        bulkCopy = SqlBulkCopyConfigure(bulkCopy, dataTable.Rows.Count);
                        bulkCopy.WriteToServer(dataTable);
                        transaction.Commit();
                        return item;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return new List<OrdemPassivoModel>();
                    }
                }
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
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
                        const string query = "DELETE FROM tbl_ordem_passivo WHERE DataEntrada = @DataEntrada";
                        int rowsAffected = await connection.ExecuteAsync(sql: query, param: new { DataEntrada = dataEntrada }, transaction: transaction);
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
            throw new NotImplementedException();
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
                        (@DataEntrada IS NULL OR tbl_ordem_passivo.DataEntrada = @DataEntrada)
                    ORDER BY
                        tbl_fundo.NomeReduzido,
	                    tbl_investidor.NomeInvestidor,
                        tbl_ordem_passivo.DescricaoOperacao";

                return await connection.QueryAsync<OrdemPassivoViewModel>(query, new { DataEntrada = dataEntrada });
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
                        string query = GenericSQLCommands.UPDATE_COMMAND.Replace("TABELA", TableName);
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
