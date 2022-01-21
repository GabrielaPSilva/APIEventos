using Dapper;
using DUDS.Models.Distribuidor;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class DistribuidorAdministradorService : GenericService<DistribuidorAdministradorModel>, IDistribuidorAdministradorService
    {
        public DistribuidorAdministradorService() : base(new DistribuidorAdministradorModel(),
                                                    "tbl_distribuidor_administrador")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(DistribuidorAdministradorModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> UpdateAsync(DistribuidorAdministradorModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.UPDATE_COMMAND.Replace("TABELA", TableName);
                List<string> str = new List<string>();
                for (int i = 0; i < _propertiesUpdate.Count; i++)
                {
                    str.Add(_fieldsUpdate[i] + " = " + _propertiesUpdate[i]);
                }
                query = query.Replace("VALORES", String.Join(",", str));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DELETE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<DistribuidorAdministradorViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IDistribuidorAdministradorService.QUERY_BASE +
                            @"
                              WHERE 
	                              tbl_distribuidor.Ativo = 1
                              ORDER BY    
                                  tbl_distribuidor.NomeDistribuidor";

                return await connection.QueryAsync<DistribuidorAdministradorViewModel>(query);
            }
        }

        public async Task<IEnumerable<DistribuidorAdministradorViewModel>> GetDistribuidorByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IDistribuidorAdministradorService.QUERY_BASE +
                            @"
                              WHERE 
	                              tbl_distribuidor_administrador.CodDistribuidor = @id
                              ORDER BY    
                                  tbl_distribuidor.NomeDistribuidor";

                return await connection.QueryAsync<DistribuidorAdministradorViewModel>(query, new { id });
            }
        }

        public async Task<DistribuidorAdministradorViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IDistribuidorAdministradorService.QUERY_BASE +
                          @"
                            WHERE 
	                            tbl_distribuidor_administrador.Id = @id
                            ORDER BY    
                                tbl_distribuidor.NomeDistribuidor";

                return await connection.QueryFirstOrDefaultAsync<DistribuidorAdministradorViewModel>(query, new { id });
            }
        }
    }
}
