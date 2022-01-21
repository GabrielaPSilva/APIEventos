﻿using Dapper;
using DUDS.Models.Tipos;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class TipoClassificacaoService : GenericService<TipoClassificacaoModel>, ITipoClassificacaoService
    {
        public TipoClassificacaoService() : base(new TipoClassificacaoModel(),
                                                "tbl_tipo_classificacao")
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

        public async Task<bool> AddAsync(TipoClassificacaoModel tipoClassificacao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, tipoClassificacao) > 0;
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

        public async Task<IEnumerable<TipoClassificacaoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {

                const string query = ITipoClassificacaoService.QUERY_BASE +
                    @"
                    WHERE 
                        Ativo = 1
                    ORDER BY    
                        Classificacao";

                return await connection.QueryAsync<TipoClassificacaoModel>(query);
            }
        }

        public async Task<TipoClassificacaoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = ITipoClassificacaoService.QUERY_BASE +
                    @"
                    WHERE 
                        Id = @Id";

                return await connection.QueryFirstOrDefaultAsync<TipoClassificacaoModel>(query, new { Id = id });
            }
        }

        public async Task<TipoClassificacaoModel> GetTipoClassificacaoExistsBase(string classificacao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = ITipoClassificacaoService.QUERY_BASE +
                    @"
                    WHERE 
                        Classificacao = @Classificacao";

                return await connection.QueryFirstOrDefaultAsync<TipoClassificacaoModel>(query, new { Classificacao = classificacao });
            }
        }

        public async Task<bool> UpdateAsync(TipoClassificacaoModel tipoClassificacao)
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
                return await connection.ExecuteAsync(query, tipoClassificacao) > 0;
            }
        }
    }
}
