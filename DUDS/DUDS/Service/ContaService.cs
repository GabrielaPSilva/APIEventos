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
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, conta) > 0;
            }
        }

        public async Task<bool> UpdateAsync(ContaModel conta)
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
                return await connection.ExecuteAsync(query, conta) > 0;
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

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DELETE_COMMAND.Replace("TABELA", _tableName);
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
	                              tbl_contas.ativo = 1";

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
	                              tbl_contas.id = @id";

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
	                             (tbl_contas.cod_fundo = @cod_fundo OR
                                 tbl_contas.cod_investidor = @cod_investidor) AND
                                 tbl_contas.cod_tipo_conta = @cod_tipo_conta";

                return await connection.QueryFirstOrDefaultAsync<ContaViewModel>(query, new { cod_fundo = codFundo, cod_investidor = codInvestidor, cod_tipo_conta = codTipoConta });
            }
        }

        public async Task<IEnumerable<ContaViewModel>> GetFundoByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IContaService.QUERY_BASE + 
                            @"
                              WHERE 
	                              tbl_contas.cod_fundo = @cod_fundo";

                return await connection.QueryAsync<ContaViewModel>(query, new { cod_fundo = id });
            }
        }
    }
}
