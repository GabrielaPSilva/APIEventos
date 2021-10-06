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

        //public async Task<AdministradorModel> GetAdministradorById(int codigo)
        //{
        //    using (var connection = await ConnectionFactorySqlServer.ConexaoAsync("BaseEducacional"))
        //    {
        //        #region QUERY

        //        const string query = @"
        //                        SELECT 
        //                            * 
        //                        FROM 
        //                            MECInstituicao
        //                        WHERE
        //                            Codigo = @codigo";

        //        #endregion

        //        return await connection.QueryFirstOrDefaultAsync<InstituicaoMOD>(query, new { codigo });
        //    }
        //}

        //public async Task<bool> CadastrarInstituicaoAsync(InstituicaoMOD instituicao)
        //{
        //    using (var connection = await ConnectionFactorySqlServer.ConexaoAsync("BaseEducacional"))
        //    {
        //        #region QUERY

        //        const string query = @"
        //                        INSERT INTO
        //                            MECInstituicao 
        //                           (Nome, CodigoMEC, Mantenedora, CNPJ, CEP, Numero, Ativo)
        //                        VALUES
        //                           (@Nome, @CodigoMEC, @Mantenedora, @CNPJ, @CEP, @Numero, 1)";

        //        #endregion

        //        return await connection.ExecuteAsync(query, instituicao) > 0;
        //    }
        //}
    }
}
