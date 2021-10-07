using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class CustodianteService : ICustodianteService
    {
        public CustodianteService()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> ActivateCustodiante(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_custodiante
                                SET 
                                    ativo = 1
                                WHERE    
                                    id = @id";

                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddCustodiante(CustodianteModel gestor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                INSERT INTO
                                    tbl_custodiante 
                                   (nome_custodiante, cnpj, usuario_criacao)
                                VALUES
                                   (@NomeGestor, @Cnpj, @UsuarioCriacao)";

                return await connection.ExecuteAsync(query, gestor) > 0;
            }
        }

        public async Task<bool> DisableCustodiante(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_custodiante
                                SET 
                                    ativo = 0
                                WHERE    
                                    id = @id";

                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<CustodianteModel>> GetCustodiante()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {

                var query = @"SELECT
                                custodiante.*
                              FROM
	                            tbl_custodiante custodiante
                            WHERE 
	                            custodiante.ativo = 1
                            ORDER BY    
                                custodiante.nome_custodiante";

                return await connection.QueryAsync<CustodianteModel>(query);
            }
        }

        public async Task<CustodianteModel> GetCustodianteById(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {

                var query = @"SELECT
                                custodiante.*
                              FROM
	                            tbl_custodiante custodiante
                            WHERE 
	                            custodiante.id = @id";

                return await connection.QueryFirstAsync<CustodianteModel>(query, new { id });
            }
        }

        public async Task<CustodianteModel> GetCustodianteExistsBase(string cnpj)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_custodiante custodiante.*
                              FROM
	                            tbl_custodiante custodiante
                            WHERE 
	                            gestor.cnpj = @cnpj";

                return await connection.QueryFirstOrDefaultAsync<CustodianteModel>(query, new { cnpj });
            }
        }

        public async Task<bool> UpdateCustodiante(CustodianteModel gestor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_custodiante
                                SET 
                                    nome_custodiante = @NomeCustodiante,
                                    cnpj = @Cnpj
                                WHERE    
                                    id = @Id";

                return await connection.ExecuteAsync(query, gestor) > 0;
            }
        }
    }
}
