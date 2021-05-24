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
                                INSERT INTO LogErro
                                    (Sistema,
                                    Arquivo,
                                    Metodo,
                                    Linha,
                                    Mensagem,
                                    Descricao,
                                    Tabela,
                                    CodigoTabela,
                                    DataHoraCadastro)
                                VALUES
                                    (@Sistema,
                                    NULL,
                                    NULL,
                                    NULL,
                                    @Mensagem,
                                    @Descricao,
                                    @Tabela,
                                    NULL,
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
