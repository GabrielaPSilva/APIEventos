using Dapper;
using DUDS.Models.PgtoTaxaAdmPfee;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class PgtoTaxaAdmPfeeService : GenericService<PgtoTaxaAdmPfeeModel>, IPgtoTaxaAdmPfeeService
    {
        public PgtoTaxaAdmPfeeService() : base(new PgtoTaxaAdmPfeeModel(), "tbl_pgto_adm_pfee")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(PgtoTaxaAdmPfeeModel item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PgtoTaxaAdmPfeeModel>> AddBulkAsync(List<PgtoTaxaAdmPfeeModel> item)
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
                        //CancellationTokenSource cancelationTokenSource = new CancellationTokenSource();
                        //CancellationToken cancellationToken = cancelationTokenSource.Token;
                        //await bulkCopy.WriteToServerAsync(dataTable, cancellationToken);
                        bulkCopy.WriteToServer(dataTable);
                        transaction.Commit();
                        return item;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return new List<PgtoTaxaAdmPfeeModel>();
                    }
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.DELETE_COMMAND.Replace("TABELA", TableName);
                        var retorno = await connection.ExecuteAsync(sql: query, param: new { id }, transaction: transaction, commandTimeout: 180);
                        transaction.Commit();
                        return retorno > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PgtoTaxaAdmPfeeViewModel>> GetAllAsync()
        {
            return await GetByParametersAsync(competencia: null, codFundo: null, codAdministrador: null, codInvestidorDistribuidor: null);
        }

        public async Task<PgtoTaxaAdmPfeeViewModel> GetByIdAsync(Guid id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IPgtoTaxaAdmPfeeService.QUERY_BASE +
                    @"
                    WHERE
                        tbl_pgto_adm_pfee.Id = @Id
                    ORDER BY
                        tbl_pgto_adm_pfee.Competencia,
                        tbl_fundo.NomeReduzido,
                        tbl_investidor.NomeInvestidor";

                return await connection.QueryFirstOrDefaultAsync<PgtoTaxaAdmPfeeViewModel>(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<PgtoTaxaAdmPfeeViewModel>> GetByParametersAsync(string competencia, int? codFundo, int? codAdministrador, int? codInvestidorDistribuidor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IPgtoTaxaAdmPfeeService.QUERY_BASE +
                    @"
                    WHERE
                        (@Competencia IS NULL OR tbl_pgto_adm_pfee.Competencia = @Competencia)
                        AND (@CodFundo IS NULL OR tbl_pgto_adm_pfee.CodFundo = @CodFundo)
                        AND (@CodAdministrador IS NULL OR tbl_pgto_adm_pfee.CodAdministrador = @CodAdministrador)
                        AND (@CodInvestidorDistribuidor IS NULL OR tbl_pgto_adm_pfee.CodInvestidorDistribuidor = @CodInvestidorDistribuidor)
                    ORDER BY
                        tbl_pgto_adm_pfee.Competencia,
                        tbl_fundo.NomeReduzido,
                        tbl_investidor.NomeInvestidor";

                return await connection.QueryAsync<PgtoTaxaAdmPfeeViewModel>(query, new
                {
                    Competencia = competencia,
                    CodFundo = codFundo,
                    CodAdministrador = codAdministrador,
                    CodInvestidorDistribuidor = codInvestidorDistribuidor
                },commandTimeout:180);
            }
        }

        public async Task<IEnumerable<PgtoTaxaAdmPfeeViewModel>> GetByCompetenciaAsync(string competencia)
        {
            return await GetByParametersAsync(competencia: competencia, codFundo: null, codAdministrador: null, codInvestidorDistribuidor: null);
        }

        public Task<bool> UpdateAsync(PgtoTaxaAdmPfeeModel item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByCompetenciaAsync(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string query = "DELETE FROM tbl_pgto_adm_pfee WHERE Competencia = @Competencia";
                        var retorno = await connection.ExecuteAsync(sql: query, param: new { Competencia = competencia }, transaction: transaction, commandTimeout: 180);
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

        public async Task<IEnumerable<PgtoAdmPfeeInvestidorViewModel>> GetPgtoAdmPfeeInvestByCompetenciaAsync(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                    SELECT
                                        pgto_adm_pfee.Id,
	                                    pgto_adm_pfee.Competencia,
	                                    pgto_adm_pfee.CodFundo,
	                                    fundo.NomeReduzido AS NomeFundo,
	                                    pgto_adm_pfee.CodAdministrador AS SourceAdministrador,
	                                    source_administrador.NomeAdministrador AS NomeSourceAdministrador,
	                                    pgto_adm_pfee.TaxaAdministracao,
	                                    pgto_adm_pfee.TaxaPerformanceApropriada,
	                                    pgto_adm_pfee.TaxaPerformanceResgate,
	                                    investidor_distribuidor.CodInvestAdministrador AS CodInvestidorAdministrador,
	                                    investidor_distribuidor.CodInvestidor,
                                        investidor_distribuidor.Id AS CodInvestidorDistribuidor,
	                                    investidor_distribuidor.CodDistribuidorAdministrador AS CodDistribuidorAdministradorInvestidor,
                                        distribuidor_administrador_investidor.CodDistribuidor AS CodDistribuidorInvestidor,
	                                    distribuidor_investidor.NomeDistribuidor AS NomeDistribuidorInvestidor,
	                                    investidor_distribuidor.CodAdministrador AS CodAdministradorCodigoInvestidor,
	                                    administrador_codigo_investidor.NomeAdministrador AS NomeAdministradorCodigoInvestidor,
	                                    investidor.NomeInvestidor,
	                                    investidor.Cnpj,
	                                    investidor.TipoInvestidor AS TipoCliente,
	                                    investidor_distribuidor.CodTipoContrato,
	                                    tipo_contrato.TipoContrato,
	                                    investidor_distribuidor.CodGrupoRebate,
	                                    grupo_rebate.NomeGrupoRebate,
	                                    investidor.CodAdministrador AS CodAdministradorInvestidor,
	                                    administrador_investidor.NomeAdministrador AS NomeAdministradorInvestidor,
	                                    investidor.CodGestor AS CodGestorInvestidor,
	                                    gestor.NomeGestor AS NomeGestorInvestidor
                                    FROM
	                                    tbl_pgto_adm_pfee pgto_adm_pfee
	                                    INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.Id = pgto_adm_pfee.CodInvestidorDistribuidor
	                                    INNER JOIN tbl_investidor investidor ON investidor.Id = investidor_distribuidor.CodInvestidor
	                                    INNER JOIN tbl_fundo fundo ON fundo.Id = pgto_adm_pfee.CodFundo
	                                    INNER JOIN tbl_administrador source_administrador ON source_administrador.Id = pgto_adm_pfee.CodAdministrador
	                                    INNER JOIN tbl_distribuidor_administrador distribuidor_administrador_investidor ON distribuidor_administrador_investidor.Id = investidor_distribuidor.CodDistribuidorAdministrador
                                        INNER JOIN tbl_distribuidor distribuidor_investidor ON distribuidor_investidor.Id = distribuidor_administrador_investidor.CodDistribuidor
	                                    INNER JOIN tbl_administrador administrador_codigo_investidor ON administrador_codigo_investidor.Id = investidor_distribuidor.CodAdministrador
	                                    INNER JOIN tbl_tipo_contrato tipo_contrato ON tipo_contrato.Id = investidor_distribuidor.CodTipoContrato
	                                    INNER JOIN tbl_grupo_rebate grupo_rebate ON grupo_rebate.Id = investidor_distribuidor.CodGrupoRebate
	                                    LEFT JOIN tbl_administrador administrador_investidor ON administrador_investidor.Id = investidor.CodAdministrador
	                                    LEFT JOIN tbl_gestor gestor ON gestor.Id = investidor.CodGestor
                                    WHERE
	                                    pgto_adm_pfee.Competencia = @Competencia";

                return await connection.QueryAsync<PgtoAdmPfeeInvestidorViewModel>(query, new { Competencia = competencia },commandTimeout:180);
            }
        }
    }
}
