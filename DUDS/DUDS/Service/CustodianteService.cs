using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class CustodianteService : GenericService<CustodianteModel>, ICustodianteService
    {
        public CustodianteService() : base(new CustodianteModel(),
            "tbl_custodiante")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(CustodianteModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> UpdateAsync(CustodianteModel item)
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

        public Task<bool> DeleteAsync(int id)
        {
            return DisableAsync(id);
        }

        public async Task<bool> DisableAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<CustodianteModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICustodianteService.QUERY_BASE + 
                            @"
                              WHERE 
	                              custodiante.Ativo = 1
                              ORDER BY    
                                  custodiante.NomeCustodiante";

                return await connection.QueryAsync<CustodianteModel>(query);
            }
        }

        public async Task<CustodianteModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICustodianteService.QUERY_BASE + 
                        @"
                           WHERE 
	                          custodiante.Id = @id";

                return await connection.QueryFirstOrDefaultAsync<CustodianteModel>(query, new { id });
            }
        }

        public async Task<CustodianteModel> GetCustodianteExistsBase(string cnpj)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICustodianteService.QUERY_BASE + 
                        @"
                           WHERE 
	                           gestor.Cnpj = @cnpj";

                return await connection.QueryFirstOrDefaultAsync<CustodianteModel>(query, new { cnpj });
            }
        }
    }
}
