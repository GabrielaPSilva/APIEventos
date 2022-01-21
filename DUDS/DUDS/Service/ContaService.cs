using Dapper;
using DUDS.Models.Conta;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ContaService : GenericService<ContaModel>, IContaService
    {
        public ContaService() : base(new ContaModel(),
                                     "tbl_contas")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(ContaModel conta)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, conta) > 0;
            }
        }

        public async Task<bool> UpdateAsync(ContaModel conta)
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
                return await connection.ExecuteAsync(query, conta) > 0;
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

        public async Task<IEnumerable<ContaViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IContaService.QUERY_BASE +
                            @"
                              WHERE
	                              tbl_contas.Ativo = 1";

                return await connection.QueryAsync<ContaViewModel>(query);
            }
        }

        public async Task<ContaViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IContaService.QUERY_BASE +
                            @"
                              WHERE 
	                              tbl_contas.Id = @id";

                return await connection.QueryFirstOrDefaultAsync<ContaViewModel>(query, new { id });
            }
        }

        public async Task<ContaViewModel> GetContaExistsBase(int codFundo, int codInvestidor, int codTipoConta)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IContaService.QUERY_BASE +
                            @"
                              WHERE 
	                             (tbl_contas.CodFundo = @CodFundo OR
                                 tbl_contas.CodInvestidor = @CodInvestidor) AND
                                 tbl_contas.CodTipoConta = @CodTipoConta";

                return await connection.QueryFirstOrDefaultAsync<ContaViewModel>(query,
                    new
                    {
                        CodFundo = codFundo,
                        CodInvestidor = codInvestidor,
                        CodTipoConta = codTipoConta
                    });
            }
        }

        public async Task<IEnumerable<ContaViewModel>> GetFundoByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IContaService.QUERY_BASE +
                            @"
                              WHERE 
	                              tbl_contas.CodFundo = @CodFundo";

                return await connection.QueryAsync<ContaViewModel>(query, new { CodFundo = id });
            }
        }
    }
}
