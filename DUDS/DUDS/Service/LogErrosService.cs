using Dapper;
using DUDS.Models;
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
                                   (sistema, metodo, linha, mensagem, descricao, usuario_criacao)
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
	                            log_erros.*
                              FROM
	                            tbl_log_erros log_erros
                            WHERE 
	                            log_erros.id = @id";

                return await connection.QueryFirstOrDefaultAsync<LogErrosModel>(query, new { id });
            }
        }
    }
}
