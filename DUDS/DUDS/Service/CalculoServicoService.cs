using Dapper;
using DUDS.Models.Contrato;
using DUDS.Models.Filtros;
using DUDS.Models.Rebate;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class CalculoServicoService : GenericService<CalculoServicoModel>, ICalculoServicoService
    {
        public CalculoServicoService() : base(new CalculoServicoModel(),
                                        "tbl_calculo_pgto_servico")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(CalculoServicoModel item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CalculoServicoModel>> AddBulkAsync(List<CalculoServicoModel> item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        SqlBulkCopy bulkCopy = new SqlBulkCopy(connection: (SqlConnection)connection, copyOptions: SqlBulkCopyOptions.TableLock, externalTransaction: (SqlTransaction)transaction);
                        var dataTable = ToDataTable(item);
                        bulkCopy = SqlBulkCopyConfigure(bulkCopy, dataTable.Rows.Count);
                        await bulkCopy.WriteToServerAsync(dataTable).ConfigureAwait(continueOnCapturedContext: false);
                        transaction.Commit();
                        return item;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CalculoServicoViewModel>> GetCalculoServico(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICalculoRebateService.QUERY_BASE +
                                @"
								    WHERE
										tbl_calculo_pgto_servico.Competencia = @Competencia
                                    ORDER BY
	                                    tbl_distribuidor.NomeDistribuidor,
                                        tbl_custodiante.NomeCustodiante,
                                        tbl_administrador.NomeAdministrador";

                return await connection.QueryAsync<CalculoServicoViewModel>(query, new { Competencia = competencia });
            }
        }

        public Task<bool> UpdateAsync(CalculoServicoModel item)
        {
            throw new NotImplementedException();
        }
    }
}