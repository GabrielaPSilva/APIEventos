﻿using Dapper;
using DUDS.Models.Administrador;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class AdministradorService : GenericService<AdministradorModel>, IAdministradorService
    {
        public AdministradorService() : base(new AdministradorModel(),
                                        "tbl_administrador")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(AdministradorModel administrador)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, administrador) > 0;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            return DisableAsync(id);
        }

        public async Task<bool> UpdateAsync(AdministradorModel administrador)
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
                return await connection.ExecuteAsync(query, administrador) > 0;
            }
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> DisableAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<AdministradorModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IAdministradorService.QUERY_BASE +
                            @" 
                              WHERE 
	                             ativo = 1
                              ORDER BY    
                                 nome_administrador";
                 
                return await connection.QueryAsync<AdministradorModel>(query);
            }
        }

        public async Task<AdministradorModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IAdministradorService.QUERY_BASE + 
                            @"
                              WHERE 
	                             id = @id";

                return await connection.QueryFirstOrDefaultAsync<AdministradorModel>(query, new { id });
            }
        }

        public async Task<AdministradorModel> GetAdministradorExistsBase(string cnpj, string nome)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IAdministradorService.QUERY_BASE + 
                            @"
                              WHERE 
	                             cnpj = @cnpj OR
                                 nome_administrador = @nome_administrador";

                return await connection.QueryFirstOrDefaultAsync<AdministradorModel>(query, new { cnpj, nome_administrador = nome });
            }
        }
    }
}
