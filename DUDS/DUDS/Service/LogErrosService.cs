using Dapper;
using DUDS.Models.LogErros;
using DUDS.Service.Interface;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class LogErrosService : ILogErrosService
    {
        public async Task<bool> AddLogErro(LogErrosModel log)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                INSERT INTO
                                    tbl_log_erros 
                                   (Sistema, Metodo, Linha, Mensagem, Descricao, UsuarioCriacao)
                                VALUES
                                   (@Sistema, @Metodo, @Linha, @Mensagem, @Descricao, @UsuarioCriacao)";

                return await connection.ExecuteAsync(query, log) > 0;
            }
        }

        public async Task<LogErrosModel> GetLogErroById(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            *
                              FROM
	                            tbl_log_erros
                            WHERE 
	                            Id = @Id";

                return await connection.QueryFirstOrDefaultAsync<LogErrosModel>(query, new { Id = id });
            }
        }
    }
}
