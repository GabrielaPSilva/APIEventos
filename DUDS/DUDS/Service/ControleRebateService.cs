using Dapper;
using DUDS.Models;
using DUDS.Models.Filtros;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ControleRebateService : GenericService<ControleRebateModel>, IControleRebateService
    {
        public ControleRebateService() : base(new ControleRebateModel(),
                                              "tbl_controle_rebate",
                                              new List<string> { "'id'", "'data_criacao'", "'ativo'" },
                                              new List<string> { "Id", "DataCriacao", "Calculo", "NomeGrupoRebate" },
                                              new List<string> { "'id'", "'data_criacao'", "'ativo'", "'usuario_criacao'" },
                                              new List<string> { "Id", "DataCriacao", "UsuarioCriacao", "Calculo", "NomeGrupoRebate" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddAsync(ControleRebateModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            return DisableAsync(id);
        }

        public async Task<bool> DisableAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<ControleRebateModel>> GetByCompetenciaAsync(FiltroModel filtro, int pagina, int itensPorPagina)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
                                 tbl_controle_rebate.*,
                                 tbl_grupo_rebate.nome_grupo_rebate
                             FROM
                                 tbl_controle_rebate
	                               INNER JOIN tbl_grupo_rebate ON tbl_controle_rebate.cod_grupo_rebate = tbl_grupo_rebate.id
                             WHERE
                                (@Competencia IS NULL OR tbl_controle_rebate.competencia = @Competencia)
                             ORDER BY
                                tbl_controle_rebate.enviado,
								tbl_controle_rebate.validado,
								tbl_grupo_rebate.nome_grupo_rebate
                             OFFSET
                                 @itensPorPagina * (@pagina - 1)
                             ROWS FETCH NEXT
                                 @itensPorPagina
                             ROWS ONLY";

                return await connection.QueryAsync<ControleRebateModel>(query, new
                {
                    pagina,
                    itensPorPagina,
                    filtro.Competencia
                });
            }
        }

        public async Task<IEnumerable<ControleRebateModel>> GetFiltroControleRebateAsync(int grupoRebate, string investidor, string competencia, string codMellon)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                tbl_controle_rebate.*,
                                tbl_calculo_pgto_adm_pfee.*,
                                tbl_grupo_rebate.nome_grupo_rebate AS NomeGrupoRebate,
                                tbl_investidor.nome_investidor AS NomeInvestidor,
                                tbl_fundo.nome_reduzido AS NomeFundo,
	                            tbl_tipo_contrato.tipo_contrato AS NomeTipoContrato,
                                tbl_investidor_distribuidor.cod_invest_administrador AS CodMellon
                            FROM
                                tbl_controle_rebate
		                            INNER JOIN tbl_investidor_distribuidor ON tbl_controle_rebate.cod_grupo_rebate = tbl_investidor_distribuidor.cod_grupo_rebate
		                            INNER JOIN tbl_calculo_pgto_adm_pfee ON tbl_calculo_pgto_adm_pfee.cod_investidor_distribuidor = tbl_investidor_distribuidor.id
		                            INNER JOIN tbl_fundo ON tbl_calculo_pgto_adm_pfee.cod_fundo = tbl_fundo.id
		                            INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
		                            INNER JOIN tbl_tipo_contrato ON tbl_investidor_distribuidor.cod_tipo_contrato = tbl_tipo_contrato.id
		                            INNER JOIN tbl_grupo_rebate ON tbl_controle_rebate.cod_grupo_rebate = tbl_grupo_rebate.id
                            WHERE
	                            tbl_controle_rebate.cod_grupo_rebate = @id
	                            AND (@investidor IS NULL OR tbl_investidor.nome_investidor COLLATE Latin1_general_CI_AI LIKE '%' + @investidor + '%')
                                AND (@codMellon IS NULL OR tbl_investidor_distribuidor.cod_invest_administrador = @codMellon)
	                            AND tbl_calculo_pgto_adm_pfee.competencia = @competencia";

                var a = await connection.QueryAsync<ControleRebateModel, CalculoRebateModel, ControleRebateModel>(query,
                   (controle, calculo) =>
                   {
                       controle.Calculo = calculo;

                       return controle;
                   }, new
                   {
                       id = grupoRebate,
                       competencia,
                       investidor,
                       codMellon
                   }, splitOn: "id");

                return a;

            }
        }

        public async Task<bool> UpdateAsync(ControleRebateModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.UPDATE_COMMAND.Replace("TABELA", _tableName);
                List<string> str = new List<string>();
                for (int i = 0; i < _propertiesUpdate.Count; i++)
                {
                    str.Add(_fieldsUpdate[i] + " = " + _propertiesUpdate[i]);
                }
                query = query.Replace("VALORES", String.Join(",", str));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<ControleRebateModel> GetGrupoRebateExistsBase(int codGrupoRebate, string Competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                             tbl_controle_rebate.*,
	                             tbl_grupo_rebate.nome_grupo_rebate
                             FROM
	                             tbl_controle_rebate
		                            INNER JOIN tbl_grupo_rebate ON tbl_controle_rebate.cod_grupo_rebate = tbl_grupo_rebate.id
                             WHERE 
	                             tbl_controle_rebate.cod_grupo_rebate = @cod_grupo_rebate AND
                                 tbl_controle_rebate.competencia = @competencia";

                return await connection.QueryFirstOrDefaultAsync<ControleRebateModel>(query, new { cod_grupo_rebate = codGrupoRebate, competencia = Competencia });
            }
        }

        public Task<IEnumerable<ControleRebateModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ControleRebateModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                     tbl_controle_rebate.*,
	                                     tbl_grupo_rebate.nome_grupo_rebate
                                      FROM
	                                     tbl_controle_rebate
                                            INNER JOIN tbl_grupo_rebate ON tbl_controle_rebate.cod_grupo_rebate = tbl_grupo_rebate.id
                                      WHERE
                                         tbl_controle_rebate.id = @id";

                return await connection.QueryFirstOrDefaultAsync<ControleRebateModel>(query, new { id });
            }
        }

        public async Task<int> GetCountControleRebateAsync(FiltroModel filtro)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
                                 COUNT(1)
                              FROM
                                 tbl_controle_rebate
	                               INNER JOIN tbl_grupo_rebate ON tbl_controle_rebate.cod_grupo_rebate = tbl_grupo_rebate.id
                             WHERE
                                 (@Competencia IS NULL OR tbl_controle_rebate.competencia = @Competencia)";
                
                return await connection.QueryFirstOrDefaultAsync<int>(query, new { filtro.Competencia });
            }
        }
    }
}
