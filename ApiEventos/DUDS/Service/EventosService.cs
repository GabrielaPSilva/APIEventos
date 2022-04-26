using Dapper;
using DUDS.Models.Administrador;
using DUDS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class EventosService : IEventosService
    {
        public EventosService()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IDbConnection> AbrirConexaoAsync(bool webConfig = false)
        {
            string serverAddress, userID, password, banco;

            serverAddress = "srvdbdahlia02.database.windows.net";
            userID = "sadb";
            password = "S@$NHujY&jkmjkl";
            banco = "db_dahlia_dev";

            var connection = new SqlConnection($"Data Source={serverAddress};Initial Catalog={banco};Persist Security Info=True;User ID={userID};Password={password};");

            await connection.OpenAsync();

            return connection;
        }

        public async Task<IEnumerable<EventosModel>> GetAllAsync()
        {
            using var connection = await AbrirConexaoAsync();
            {
                var query =
                            @"SELECT * FROM tbl_eventos";

                return await connection.QueryAsync<EventosModel>(query, commandTimeout: 180);
            }
        }

        public async Task<EventosModel> GetByIdAsync(int id)
        {
            using var connection = await AbrirConexaoAsync();
            {
                var query =
                           @"SELECT * FROM tbl_eventos WHERE Id = @id";

                return await connection.QueryFirstOrDefaultAsync<EventosModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<EventosModel>> GetByIdPaisAsync(int idPais)
        {
            using var connection = await AbrirConexaoAsync();
            {
                var query =
                           @"SELECT * FROM tbl_eventos WHERE IdPais = @idPais";

                return await connection.QueryAsync<EventosModel>(query, new { idPais });
            }
        }

        public async Task<bool> AddAsync(EventosModel eventos)
        {
            using var connection = await AbrirConexaoAsync();
            {
                const string query = @"
                                INSERT INTO
                                    tbl_eventos 
                                   (IdPais, NomeEvento, Observacao, DataEvento)
                                VALUES
                                   (@IdPais, @NomeEvento, @Observacao, @DataEvento)";

                return await connection.ExecuteAsync(query, eventos) > 0;
            }
        }

        public async Task<bool> UpdateAsync(EventosModel eventos)
        {
            using var connection = await AbrirConexaoAsync();
            {
                const string query = @"
                                UPDATE
                                     tbl_eventos
                                SET 
                                    IdPais = @IdPais,
                                    NomeEvento = @NomeEvento, 
                                    Observacao = @Observacao, 
                                    DataEvento = @DataEvento
                                WHERE    
                                    Id = @Id";
                return await connection.ExecuteAsync(query, eventos) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = await AbrirConexaoAsync();
            {
                const string query = @"DELETE tbl_eventos WHERE Id = @id";

                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }
    }
}
