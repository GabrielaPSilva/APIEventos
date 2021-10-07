using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class AdministradorService : IAdministradorService
    {
        public AdministradorService()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IEnumerable<AdministradorModel>> GetAdministrador()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            *
                              FROM
	                             tbl_administrador
                              WHERE 
	                             ativo = 1
                              ORDER BY    
                                 nome_administrador";

                return await connection.QueryAsync<AdministradorModel>(query);
            }
        }

        public async Task<AdministradorModel> GetAdministradorById(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                SELECT 
                                    * 
                                FROM 
                                    tbl_administrador
                                WHERE
                                    id = @id";

                return await connection.QueryFirstOrDefaultAsync<AdministradorModel>(query, new { id });
            }
        }

        public async Task<AdministradorModel> GetAdministradorExistsBase(string cnpj, string nome)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                SELECT 
                                    * 
                                FROM 
                                    tbl_administrador
                                WHERE
                                    cnpj = @cnpj OR
                                    nome_administrador = @nome_administrador";

                return await connection.QueryFirstOrDefaultAsync<AdministradorModel>(query, new { cnpj, nome_administrador = nome });
            }
        }

        public async Task<bool> AddAdministrador(AdministradorModel administrador)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                INSERT INTO
                                    tbl_administrador 
                                   (nome_administrador, cnpj)
                                VALUES
                                   (@NomeAdministrador, @Cnpj)";

               return await connection.ExecuteAsync(query, administrador) > 0;
            }
        }

        public async Task<bool> UpdateAdministrador(AdministradorModel administrador)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_administrador
                                SET 
                                    nome_administrador = @NomeAdministrador,
                                    cnpj = @Cnpj
                                WHERE    
                                    id = @id";

                return await connection.ExecuteAsync(query, administrador) > 0;
            }
        }

        public async Task<bool> DisableAdministrador(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_administrador
                                SET 
                                    ativo = 0
                                WHERE    
                                    id = @id";

                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> ActivateAdministrador(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_administrador
                                SET 
                                    ativo = 1
                                WHERE    
                                    id = @id";

                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }
    }
}
