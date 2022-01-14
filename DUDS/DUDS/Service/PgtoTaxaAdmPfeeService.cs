using Dapper;
using DUDS.Models.PgtoTaxaAdmPfee;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class PgtoTaxaAdmPfeeService : GenericService<PgtoTaxaAdmPfeeModel>, IPgtoTaxaAdmPfeeService
    {
        public PgtoTaxaAdmPfeeService() : base(new PgtoTaxaAdmPfeeModel(),"tbl_pgto_adm_pfee")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(PgtoTaxaAdmPfeeModel item)
        {
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

        public async Task<IEnumerable<PgtoTaxaAdmPfeeModel>> AddBulkAsync(List<PgtoTaxaAdmPfeeModel> pgtoTaxaAdmimPerf)
        {
            ConcurrentBag<PgtoTaxaAdmPfeeModel> vs = new ConcurrentBag<PgtoTaxaAdmPfeeModel>();
            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess };
            await Parallel.ForEachAsync(pgtoTaxaAdmimPerf, parallelOptions, async (x, cancellationToken) =>
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
                        var retorno = await connection.ExecuteAsync(sql: query, param: new { id }, transaction: transaction,commandTimeout:180);
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
                        pgto_adm_pfee.id = @id
                    ORDER BY
                        pgto_adm_pfee.competencia,
                        fundo.nome_reduzido,
                        investidor.nome_investidor";

                return await connection.QueryFirstOrDefaultAsync<PgtoTaxaAdmPfeeViewModel>(query, new { id });
            }
        }

        private async Task<IEnumerable<PgtoTaxaAdmPfeeViewModel>> GetByParametersAsync(string competencia, int? codFundo, int? codAdministrador, int? codInvestidorDistribuidor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IPgtoTaxaAdmPfeeService.QUERY_BASE + 
                    @"
                    WHERE
                        (@competencia IS NULL OR pgto_adm_pfee.competencia = @competencia)
                        AND (@cod_fundo IS NULL OR pgto_adm_pfee.cod_fundo = @cod_fundo)
                        AND (@cod_administrador IS NULL OR pgto_adm_pfee.cod_administrador = @cod_administrador)
                        AND (@cod_investidor_distribuidor IS NULL OR pgto_adm_pfee.cod_investidor_distribuidor = @cod_investidor_distribuidor)
                    ORDER BY
                        pgto_adm_pfee.competencia,
                        fundo.nome_reduzido,
                        investidor.nome_investidor";

                return await connection.QueryAsync<PgtoTaxaAdmPfeeViewModel>(query, new { competencia, cod_fundo = codFundo, cod_administrador = codAdministrador, cod_investidor_distribuidor = codInvestidorDistribuidor });
            }
        }

        public async Task<IEnumerable<PgtoTaxaAdmPfeeViewModel>> GetByCompetenciaAsync(string competencia)
        {
            return await GetByParametersAsync(competencia:competencia,codFundo:null,codAdministrador:null,codInvestidorDistribuidor:null);
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
                        const string query = "DELETE FROM tbl_pgto_adm_pfee WHERE competencia = @competencia";
                        var retorno = await connection.ExecuteAsync(sql: query, param: new { competencia }, transaction:transaction,commandTimeout:180);
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
	                                    pgto_adm_pfee.competencia,
	                                    pgto_adm_pfee.cod_fundo,
	                                    fundo.nome_reduzido AS nome_fundo,
	                                    pgto_adm_pfee.cod_administrador AS source_administrador,
	                                    source_administrador.nome_administrador AS nome_source_administrador,
	                                    pgto_adm_pfee.taxa_administracao,
	                                    pgto_adm_pfee.taxa_performance_apropriada,
	                                    pgto_adm_pfee.taxa_performance_resgate,
	                                    investidor_distribuidor.cod_invest_administrador AS cod_investidor_administrador,
	                                    investidor_distribuidor.cod_investidor,
                                        investidor_distribuidor.id AS cod_investidor_distribuidor,
	                                    investidor_distribuidor.cod_distribuidor AS cod_distribuidor_investidor,
	                                    distribuidor_investidor.nome_distribuidor AS nome_distribuidor_investidor,
	                                    investidor_distribuidor.cod_administrador AS cod_administrador_codigo_investidor,
	                                    administrador_codigo_investidor.nome_administrador AS nome_administrador_codigo_investidor,
	                                    investidor.nome_investidor,
	                                    investidor.cnpj,
	                                    investidor.tipo_investidor AS tipo_cliente,
	                                    investidor_distribuidor.cod_tipo_contrato,
	                                    tipo_contrato.tipo_contrato,
	                                    investidor_distribuidor.cod_grupo_rebate,
	                                    grupo_rebate.nome_grupo_rebate,
	                                    investidor.cod_administrador AS cod_administrador_investidor,
	                                    administrador_investidor.nome_administrador AS nome_administrador_investidor,
	                                    investidor.cod_gestor AS cod_gestor_investidor,
	                                    gestor.nome_gestor AS nome_gestor_investidor
                                    FROM
	                                    tbl_pgto_adm_pfee pgto_adm_pfee
	                                    INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.id = pgto_adm_pfee.cod_investidor_distribuidor
	                                    INNER JOIN tbl_investidor investidor ON investidor.id = investidor_distribuidor.cod_investidor
	                                    INNER JOIN tbl_fundo fundo ON fundo.id = pgto_adm_pfee.cod_fundo
	                                    INNER JOIN tbl_administrador source_administrador ON source_administrador.id = pgto_adm_pfee.cod_administrador
	                                    INNER JOIN tbl_distribuidor distribuidor_investidor ON distribuidor_investidor.id = investidor_distribuidor.cod_distribuidor
	                                    INNER JOIN tbl_administrador administrador_codigo_investidor ON administrador_codigo_investidor.id = investidor_distribuidor.cod_administrador
	                                    INNER JOIN tbl_tipo_contrato tipo_contrato ON tipo_contrato.id = investidor_distribuidor.cod_tipo_contrato
	                                    INNER JOIN tbl_grupo_rebate grupo_rebate ON grupo_rebate.id = investidor_distribuidor.cod_grupo_rebate
	                                    LEFT JOIN tbl_administrador administrador_investidor ON administrador_investidor.id = investidor.cod_administrador
	                                    LEFT JOIN tbl_gestor gestor ON gestor.id = investidor.cod_gestor
                                    WHERE
	                                    pgto_adm_pfee.competencia = @competencia";

                return await connection.QueryAsync<PgtoAdmPfeeInvestidorViewModel>(query, new { competencia });
            }
        }
    }
}
