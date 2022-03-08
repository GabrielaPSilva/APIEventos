using Dapper;
using DUDS.Models.Rebate;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ErrosPagamentoService : GenericService<ErrosPagamentoModel>, IErrosPagamentoService
    {

        public ErrosPagamentoService() : base(new ErrosPagamentoModel(),
                                         "tbl_erros_pagamento")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(ErrosPagamentoModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<IEnumerable<ErrosPagamentoModel>> AddErrosPagamento(List<ErrosPagamentoModel> item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        SqlBulkCopy bulkCopy = new SqlBulkCopy(connection: (SqlConnection)connection,
                            copyOptions: SqlBulkCopyOptions.Default,
                            externalTransaction: (SqlTransaction)transaction);

                        var dataTable = ToDataTable(item);
                        bulkCopy = SqlBulkCopyConfigure(bulkCopy, dataTable.Rows.Count);
                        await bulkCopy.WriteToServerAsync(dataTable).ConfigureAwait(continueOnCapturedContext: false);
                        bulkCopy.WriteToServer(dataTable);
                        transaction.Commit();
                        return item;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return new List<ErrosPagamentoModel>();
                    }
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DELETE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
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
                                            DataAgendamento = @dataAgendamento";

                return await connection.ExecuteAsync(query, new { dataAgendamento }) > 0;
            }
        }

        public async Task<IEnumerable<ErrosPagamentoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IErrosPagamentoService.QUERY_BASE +
                          @"
                            ORDER BY    
                                erros_pagamento.DataAgendamento";

                return await connection.QueryAsync<ErrosPagamentoModel>(query);
            }
        }

        public async Task<IEnumerable<ErrosPagamentoModel>> GetErrosPagamentoByCompetencia(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IErrosPagamentoService.QUERY_BASE +
                             @"
                                WHERE
                                    erros_pagamento.Competencia = @competencia";

                return await connection.QueryAsync<ErrosPagamentoModel>(query, new { competencia });
            }
        }

        public async Task<ErrosPagamentoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IErrosPagamentoService.QUERY_BASE +
                            @"
                              WHERE
                                 erros_pagamento.Id = @id";

                return await connection.QueryFirstOrDefaultAsync<ErrosPagamentoModel>(query, new { id });
            }
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ErrosPagamentoModel item)
        {
            throw new NotImplementedException();
        }
    }
}
