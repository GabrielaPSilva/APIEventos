using Dapper;
using DUDS.Models.Administrador;
using DUDS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIEventosTest
{
    public class EventosServiceFake : IEventosService
    {
        private readonly List<EventosModel> _eventos;
        public EventosServiceFake()
        {
            _eventos = new List<EventosModel>()
            {
                new EventosModel() { IdPais = 1, NomeEvento = "Passeio",
                                   Observacao = null, DataEvento = DateTime.Now.Date },
                new EventosModel() { IdPais = 2, NomeEvento = "Viagem",
                                   Observacao = null, DataEvento = DateTime.Now.Date },
                new EventosModel() { IdPais = 3, NomeEvento = "Dia das mães",
                                   Observacao = null, DataEvento = DateTime.Now.Date },
                new EventosModel() { IdPais = 4, NomeEvento = "Dia dos pais",
                                   Observacao = null, DataEvento = DateTime.Now.Date },
                new EventosModel() { IdPais = 5, NomeEvento = "Aniversário amiga da escola",
                                   Observacao = "Isabela", DataEvento = DateTime.Now.Date }
            };
        }
        static int GeraId()
        {
            Random random = new Random();
            return random.Next(1, 100);
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

        public async Task<bool> AddAsync(int idPais, string nomeEvento, string observacao, string dataEvento)
        {
            using var connection = await AbrirConexaoAsync();
            {
                const string query = @"
                                INSERT INTO
                                    tbl_eventos 
                                   (IdPais, NomeEvento, Observacao, DataEvento)
                                VALUES
                                   (@idPais, @nomeEvento, @observacao, @dataEvento)";

                return await connection.ExecuteAsync(query, new { idPais, nomeEvento, observacao, dataEvento }) > 0;
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
