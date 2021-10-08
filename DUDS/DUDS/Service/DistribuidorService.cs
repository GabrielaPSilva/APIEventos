using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class DistribuidorService : IDistribuidorService
    {
        public async Task<bool> ActivateDistribuidor(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_distribuidor
                                SET 
                                    ativo = 1
                                WHERE    
                                    id = @id";

                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public Task<bool> AddDistribuidor(DistribuidorModel distribuidor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddDistribuidorAdministrador(DistribuidorAdministradorModel distribuidorAdministrador)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DisableDistribuidorr(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                UPDATE
                                    tbl_distribuidor
                                SET 
                                    ativo = 0
                                WHERE    
                                    id = @id";

                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public Task<IEnumerable<DistribuidorModel>> GetDistribuidor()
        {
            throw new NotImplementedException();
        }

        public Task<DistribuidorModel> GetDistribuidorById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DistribuidorModel> GetDistribuidorExistsBase(string cnpj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDistribuidor(DistribuidorModel distribuidor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDistribuidorAdministrador(DistribuidorAdministradorModel distribuidorAdministrador)
        {
            throw new NotImplementedException();
        }
    }
}
