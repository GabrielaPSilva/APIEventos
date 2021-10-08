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
    public class TipoCondicaoService : GenericService<TipoCondicaoModel>, ITipoCondicaoService
    {
        public TipoCondicaoService() : base(new TipoCondicaoModel(),
                                               "tbl_tipo_classificacao",
                                               new List<string> { "'id'", "'data_criacao'", "'ativo'" },
                                               new List<string> { "Id", "DataCriacao", "Ativo" },
                                               new List<string> { "'id'", "'data_criacao'", "'ativo'", "'usuario_criacao'" },
                                               new List<string> { "Id", "DataCriacao", "Ativo", "UsuarioCriacao" })
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

        public async Task<bool> AddAsync(TipoCondicaoModel tipoCondicao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, tipoCondicao) > 0;
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

        public async Task<IEnumerable<TipoCondicaoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {

                var query = @"SELECT
                                 *
                              FROM
	                             tbl_tipo_condicao
                              WHERE 
	                             ativo = 1
                              ORDER BY    
                                 tipo_condicao";

                return await connection.QueryAsync<TipoCondicaoModel>(query);
            }
        }

        public async Task<TipoCondicaoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                 *
                              FROM
	                             tbl_tipo_condicao
                              WHERE 
	                             id = @id";

                return await connection.QueryFirstOrDefaultAsync<TipoCondicaoModel>(query, new { id });
            }
        }

        public async Task<bool> UpdateAsync(TipoCondicaoModel tipoCondicao)
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
                return await connection.ExecuteAsync(query, tipoCondicao) > 0;
            }
        }
    }
}
