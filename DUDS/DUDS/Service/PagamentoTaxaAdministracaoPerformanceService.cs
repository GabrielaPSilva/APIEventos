using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class PagamentoTaxaAdministracaoPerformanceService : GenericService<PagamentoTaxaAdminPfeeModel>, IPagamentoTaxaAdministracaoPerformanceService
    {
        public PagamentoTaxaAdministracaoPerformanceService() : base(new PagamentoTaxaAdminPfeeModel(),
            "tbl_pgto_adm_pfee",
            new List<string> { "'id'" },
            new List<string> { "Id", "NomeInvestidor", "NomeFundo", "NomeAdministrador" },
            new List<string> { "'id'" },
            new List<string> { "Id", "NomeInvestidor", "NomeFundo", "NomeAdministrador" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(PagamentoTaxaAdminPfeeModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> AddBulkAsync(List<PagamentoTaxaAdminPfeeModel> pgtoTaxaAdmimPerf)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                Parallel.ForEach(pgtoTaxaAdmimPerf, async x =>
                {
                    _ = await AddAsync(x);
                });

                return GetByCompetenciaAsync(pgtoTaxaAdmimPerf.FirstOrDefault().Competencia).Result.ToArray().Length == pgtoTaxaAdmimPerf.Count;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DELETE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PagamentoTaxaAdminPfeeModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pgto_adm_pfee.*,
                                        fundo.nome_reduzido as nome_fundo,
                                        investidor.nome_investidor
                                     FROM
	                                    tbl_pgto_adm_pfee pgto_adm_pfee
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pgto_adm_pfee.cod_fundo
                                        INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.id = pgto_adm_pfee.cod_investidor_distribuidor
                                        INNER JOIN tbl_investidor investidor ON investidor.id = investidor_distribuidor.cod_investidor
                                     ORDER BY
                                        pgto_adm_pfee.competencia,
                                        fundo.nome_reduzido,
                                        investidor.nome_investidor";

                return await connection.QueryAsync<PagamentoTaxaAdminPfeeModel>(query);
            }
        }

        public async Task<PagamentoTaxaAdminPfeeModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pgto_adm_pfee.*,
                                        fundo.nome_reduzido as nome_fundo,
                                        investidor.nome_investidor
                                     FROM
	                                    tbl_pgto_adm_pfee pgto_adm_pfee
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pgto_adm_pfee.cod_fundo
                                        INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.id = pgto_adm_pfee.cod_investidor_distribuidor
                                        INNER JOIN tbl_investidor investidor ON investidor.id = investidor_distribuidor.cod_investidor
                                     WHERE
                                        pgto_adm_pfee.id = @id
                                     ORDER BY
                                        pgto_adm_pfee.competencia,
                                        fundo.nome_reduzido,
                                        investidor.nome_investidor";

                return await connection.QueryFirstOrDefaultAsync<PagamentoTaxaAdminPfeeModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<PagamentoTaxaAdminPfeeModel>> GetByIdsAsync(string competencia, int codFundo, int codAdministrador, int codInvestidorDistribuidor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pgto_adm_pfee.*,
                                        fundo.nome_reduzido as nome_fundo,
                                        investidor.nome_investidor
                                     FROM
	                                    tbl_pgto_adm_pfee pgto_adm_pfee
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pgto_adm_pfee.cod_fundo
                                        INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.id = pgto_adm_pfee.cod_investidor_distribuidor
                                        INNER JOIN tbl_investidor investidor ON investidor.id = investidor_distribuidor.cod_investidor
                                     WHERE
                                        pgto_adm_pfee.competencia = @competencia
                                        AND pgto_adm_pfee.cod_fundo = @cod_fundo
                                        AND pgto_adm_pfee.cod_administrador = @cod_administrador
                                        AND pgto_adm_pfee.cod_investidor_distribuidor = @cod_investidor_distribuidor
                                     ORDER BY
                                        pgto_adm_pfee.competencia,
                                        fundo.nome_reduzido,
                                        investidor.nome_investidor";

                return await connection.QueryAsync<PagamentoTaxaAdminPfeeModel>(query, new { competencia, cod_fundo = codFundo, cod_administrador = codAdministrador, cod_investidor_distribuidor = codInvestidorDistribuidor });
            }
        }

        public async Task<IEnumerable<PagamentoTaxaAdminPfeeModel>> GetByCompetenciaAsync(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pgto_adm_pfee.*,
                                        fundo.nome_reduzido as nome_fundo,
                                        investidor.nome_investidor
                                     FROM
	                                    tbl_pgto_adm_pfee pgto_adm_pfee
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pgto_adm_pfee.cod_fundo
                                        INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.id = pgto_adm_pfee.cod_investidor_distribuidor
                                        INNER JOIN tbl_investidor investidor ON investidor.id = investidor_distribuidor.cod_investidor
                                     WHERE
                                        pgto_adm_pfee.competencia = @competencia
                                     ORDER BY
                                        pgto_adm_pfee.competencia,
                                        fundo.nome_reduzido,
                                        investidor.nome_investidor";

                return await connection.QueryAsync<PagamentoTaxaAdminPfeeModel>(query, new { competencia });
            }
        }

        public Task<bool> UpdateAsync(PagamentoTaxaAdminPfeeModel item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByCompetenciaAsync(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = "DELETE FROM tbl_pgto_adm_pfee WHERE competencia = @competencia";
                return await connection.ExecuteAsync(query, new { competencia }) > 0;
            }
        }

        public async Task<IEnumerable<PagamentoAdmPfeeInvestidorModel>> GetPgtoAdmPfeeInvestByCompetenciaAsync(string competencia)
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
	                                    investidor_distribuidor.cod_invest_administrador AS codigo_investidor_administrador,
	                                    investidor_distribuidor.cod_investidor,
	                                    investidor_distribuidor.cod_distribuidor AS cod_distribuidor_investidor,
	                                    distribuidor_investidor.nome_distribuidor AS nome_distribuidor_investidor,
	                                    investidor_distribuidor.cod_administrador AS cod_administrador_codigo_investidor,
	                                    administrador_codigo_investidor.nome_administrador AS nome_administrador_codigo_investidor,
	                                    investidor.nome_investidor,
	                                    investidor.cnpj,
	                                    investidor.tipo_investidor AS tipo_cliente,
	                                    investidor.cod_tipo_contrato,
	                                    tipo_contrato.tipo_contrato,
	                                    investidor.cod_grupo_rebate,
	                                    grupo_rebate.nome_grupo_rebate,
	                                    investidor.cod_administrador AS cod_administrador_investidor,
	                                    administrador_investidor.nome_administrador AS nome_administrador_investidor,
	                                    investidor.cod_gestor AS cod_gestor_investirdor,
	                                    gestor.nome_gestor AS nome_gestor_investidor
                                    FROM
	                                    tbl_pgto_adm_pfee pgto_adm_pfee
	                                    INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.id = pgto_adm_pfee.cod_investidor_distribuidor
	                                    INNER JOIN tbl_investidor investidor ON investidor.id = investidor_distribuidor.cod_investidor
	                                    INNER JOIN tbl_fundo fundo ON fundo.id = pgto_adm_pfee.cod_fundo
	                                    INNER JOIN tbl_administrador source_administrador ON source_administrador.id = pgto_adm_pfee.cod_administrador
	                                    INNER JOIN tbl_distribuidor distribuidor_investidor ON distribuidor_investidor.id = investidor_distribuidor.cod_distribuidor
	                                    INNER JOIN tbl_administrador administrador_codigo_investidor ON administrador_codigo_investidor.id = investidor_distribuidor.cod_administrador
	                                    INNER JOIN tbl_tipo_contrato tipo_contrato ON tipo_contrato.id = investidor.cod_tipo_contrato
	                                    INNER JOIN tbl_grupo_rebate grupo_rebate ON grupo_rebate.id = investidor.cod_grupo_rebate
	                                    LEFT JOIN tbl_administrador administrador_investidor ON administrador_investidor.id = investidor.cod_administrador
	                                    LEFT JOIN tbl_gestor gestor ON gestor.id = investidor.cod_gestor
                                    WHERE
	                                    pgto_adm_pfee.competencia = @competencia";

                return await connection.QueryAsync<PagamentoAdmPfeeInvestidorModel>(query, new { competencia });
            }
        }
    }
}
