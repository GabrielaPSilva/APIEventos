using Dapper;
using DUDS.DAL.Interfaces;
using DUDS.MOD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DUDS.DAL
{
    public class LogErrorDAL : ILogErrorDAL
    {
        public async Task<bool> CadastrarLogErroAsync(LogErrorMOD logErro)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "srvdbdahlia02.database.windows.net";
            builder.UserID = "sadb";
            builder.Password = "S@$NHujY&jkmjkl";
            builder.InitialCatalog = "db_dahlia_dev";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                string insert = @"
                                INSERT INTO tbl_log_erros
                                    (sistema,
                                    metodo,
                                    linha,
                                    mensagem,
                                    descricao,
                                    usuario_modificacao,
                                    data_cadastro)
                                VALUES
                                    (@sistema,
                                    @metodo,
                                    @linha,
                                    @mensagem,
                                    @descricao,
                                    1,
                                    GETDATE())";

                try
                {
                    return connection.Execute(insert, logErro) > 0;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
