﻿using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class TipoContaService : GenericService<TipoContaModel>, ITipoContaService
    {
        public TipoContaService() : base(new TipoContaModel(),
                                              "tbl_tipo_conta",
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

        public async Task<bool> AddAsync(TipoContaModel tipoConta)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, tipoConta) > 0;
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

        public async Task<IEnumerable<TipoContaModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {

                var query = @"SELECT
                                 *
                              FROM
	                             tbl_tipo_conta
                              WHERE 
	                             ativo = 1
                              ORDER BY    
                                 tipo_conta,
                                 descricao_conta";

                return await connection.QueryAsync<TipoContaModel>(query);
            }
        }

        public async Task<TipoContaModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                 *
                              FROM
	                             tbl_tipo_conta
                              WHERE 
	                             id = @id";

                return await connection.QueryFirstOrDefaultAsync<TipoContaModel>(query, new { id });
            }
        }

        public async Task<GestorModel> GetTipoContaExistsBase(string tipoConta, string descricaoConta)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                 *
                              FROM
	                             tbl_tipo_conta
                              WHERE 
	                             tipo_conta = @tipoConta AND
                                 descricao_conta = @descricaoConta";

                return await connection.QueryFirstOrDefaultAsync<GestorModel>(query, new { tipoConta, descricaoConta });
            }
        }

        public async Task<bool> UpdateAsync(TipoContaModel tipoConta)
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
                return await connection.ExecuteAsync(query, tipoConta) > 0;
            }
        }
    }
}