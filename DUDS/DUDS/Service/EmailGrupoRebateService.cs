using Dapper;
using DUDS.Models.Rebate;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class EmailGrupoRebateService : GenericService<EmailGrupoRebateModel>, IEmailGrupoRebateService
    {
        public EmailGrupoRebateService() : base(new EmailGrupoRebateModel(),
                                           "tbl_email_grupo_rebate")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(EmailGrupoRebateModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> UpdateAsync(EmailGrupoRebateModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.UPDATE_COMMAND.Replace("TABELA", _tableName);
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
            return await DisableAsync(id);
        }

        public async Task<bool> DisableAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<EmailGrupoRebateViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IEmailGrupoRebateService.QUERY_BASE + 
                            @"
                              WHERE 
	                             ativo = 1
                              ORDER BY    
                                 nome_grupo_rebate";

                return await connection.QueryAsync<EmailGrupoRebateViewModel>(query);
            }
        }

        public async Task<EmailGrupoRebateViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IEmailGrupoRebateService.QUERY_BASE + 
                             @"
                               WHERE 
	                             id = @id";

                return await connection.QueryFirstOrDefaultAsync<EmailGrupoRebateViewModel>(query, new { id });
            }
        }
    }
}

