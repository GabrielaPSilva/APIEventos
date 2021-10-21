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
    public class ControleRebateService : GenericService<ControleRebateModel>, IControleRebateService
    {
        public ControleRebateService() : base(new ControleRebateModel(),
                                              "tbl_controle_rebate",
                                              new List<string> { "'id'", "'data_criacao'", "'ativo'" },
                                              new List<string> { "Id", "DataCriacao", "GrupoRebate" },
                                              new List<string> { "'id'", "'data_criacao'", "'ativo'", "'usuario_criacao'" },
                                              new List<string> { "Id", "DataCriacao", "UsuarioCriacao", "GrupoRebate" })
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

        public async Task<IEnumerable<ControleRebateModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                             tbl_controle_rebate.*,
	                             tbl_grupo_rebate.nome_grupo_rebate
                             FROM
	                             tbl_controle_rebate
		                            INNER JOIN tbl_grupo_rebate ON tbl_controle_rebate.cod_grupo_rebate = tbl_grupo_rebate.id";

                return await connection.QueryAsync<ControleRebateModel>(query);
            }
        }

        public async Task<ControleRebateModel> GetByIdAsync(int id)
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
	                             id = @id";

                return await connection.QueryFirstOrDefaultAsync<ControleRebateModel>(query, new { id });
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
    }
}
