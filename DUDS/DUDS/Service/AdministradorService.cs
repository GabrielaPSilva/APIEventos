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
            using (var connection = await SqlHelpers.Standard.ConnectionFactory.ConexaoAsync("db_dahlia_desenv"))
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
    }
}
