using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class GestorService : IGestorService
    {
        public GestorService()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> ActivateGestor(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_gestor
                                SET 
                                    ativo = 1
                                WHERE    
                                    id = @id";

                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddGestor(GestorModel gestor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                INSERT INTO
                                    tbl_gestor 
                                   (nome_gestor, cnpj, usuario_criacao, cod_tipo_classificacao)
                                VALUES
                                   (@NomeGestor, @Cnpj, @UsuarioCriacao, @CodTipoClassificacao)";

                return await connection.ExecuteAsync(query, gestor) > 0;
            }
        }

        public async Task<bool> DisableGestor(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_gestor
                                SET 
                                    ativo = 0
                                WHERE    
                                    id = @id";

                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<GestorModel>> GetGestor()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {

                var query = @"SELECT
                                gestor.*,
                                tipo_classificacao.classificacao as Classificacao
                              FROM
	                            tbl_gestor gestor
                                inner join tbl_tipo_classificacao tipo_classificacao on gestor.cod_tipo_classificacao = tipo_classificacao.id
                            WHERE 
	                            gestor.ativo = 1
                            ORDER BY    
                                gestor.nome_gestor";

                return await connection.QueryAsync<GestorModel>(query);
            }
        }

        public async Task<GestorModel> GetGestorById(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            gestor.*,
                                tipo_classificacao.classificacao
                              FROM
	                            tbl_gestor gestor
                                inner join tbl_tipo_classificacao tipo_classificacao on gestor.cod_tipo_classificacao = tipo_classificacao.id
                            WHERE 
	                            gestor.id = @id";

                return await connection.QueryFirstOrDefaultAsync<GestorModel>(query, new { id });
            }
        }

        public async Task<GestorModel> GetGestorExistsBase(string cnpj)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            gestor.*,
                                tipo_classificacao.classificacao
                              FROM
	                            tbl_gestor gestor
                                inner join tbl_tipo_classificacao tipo_classificacao on gestor.cod_tipo_classificacao = tipo_classificacao.id
                            WHERE 
	                            gestor.cnpj = @cnpj";

                return await connection.QueryFirstOrDefaultAsync<GestorModel>(query, new { cnpj });
            }
        }

        public async Task<bool> UpdateGestor(GestorModel gestor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_gestor
                                SET 
                                    nome_gestor = @NomeGestor,
                                    cnpj = @Cnpj,
                                    cod_tipo_classificacao = @CodTipoClassificacao
                                WHERE    
                                    id = @Id";

                return await connection.ExecuteAsync(query, gestor) > 0;
            }
        }
    }
}
