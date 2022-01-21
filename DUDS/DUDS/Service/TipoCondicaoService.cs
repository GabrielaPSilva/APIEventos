﻿using Dapper;
using DUDS.Models.Tipos;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class TipoCondicaoService : GenericService<TipoCondicaoModel>, ITipoCondicaoService
    {
        public TipoCondicaoService() : base(new TipoCondicaoModel(),
                                               "tbl_tipo_condicao")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddAsync(TipoCondicaoModel tipoCondicao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
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
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<TipoCondicaoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {

                const string query = ITipoCondicaoService.QUERY_BASE +
                    @"
                    WHERE 
	                    Ativo = 1
                    ORDER BY    
                        TipoCondicao";

                return await connection.QueryAsync<TipoCondicaoModel>(query);
            }
        }

        public async Task<TipoCondicaoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = ITipoCondicaoService.QUERY_BASE +
                    @"
                    WHERE 
                        Id = @Id";

                return await connection.QueryFirstOrDefaultAsync<TipoCondicaoModel>(query, new { Id = id });
            }
        }

        public async Task<TipoCondicaoModel> GetTipoCondicaoExistsBase(string tipoCondicao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = ITipoCondicaoService.QUERY_BASE +
                    @"
                    WHERE 
                        TipoCondicao = @TipoCondicao";

                return await connection.QueryFirstOrDefaultAsync<TipoCondicaoModel>(query, new { TipoCondicao = tipoCondicao });
            }
        }

        public async Task<bool> UpdateAsync(TipoCondicaoModel tipoCondicao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.UPDATE_COMMAND.Replace("TABELA", TableName);
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
