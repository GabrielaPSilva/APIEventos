using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class InvestidorDistribuidorService : GenericService<InvestidorDistribuidorModel>, IInvestidorDistribuidorService
    {
        public InvestidorDistribuidorService() : base(new InvestidorDistribuidorModel(),
                                                         "tbl_investidor_distribuidor",
                                                         new List<string> { "'id'", "'data_criacao'" },
                                                         new List<string> { "Id", "DataCriacao", "NomeInvestidor", "NomeDistribuidor", "NomeAdministrador" },
                                                         new List<string> { "'id'", "'data_criacao'", "'usuario_criacao'" },
                                                         new List<string> { "Id", "DataCriacao", "UsuarioCriacao", "NomeInvestidor", "NomeDistribuidor", "NomeAdministrador" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddAsync(InvestidorDistribuidorModel investDistribuidor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, investDistribuidor) > 0;
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

        public async Task<IEnumerable<InvestidorDistribuidorModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor_distribuidor.*,
	                            tbl_investidor.nome_investidor,
	                            tbl_distribuidor.nome_distribuidor,
	                            tbl_administrador.nome_administrador
                            FROM 
	                            tbl_investidor_distribuidor
	                                INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
	                                INNER JOIN tbl_distribuidor ON tbl_investidor_distribuidor.cod_distribuidor = tbl_distribuidor.id
	                                INNER JOIN tbl_administrador ON tbl_investidor_distribuidor.cod_administrador = tbl_administrador.id";

                return await connection.QueryAsync<InvestidorDistribuidorModel>(query);
            }
        }

        public async Task<IEnumerable<InvestidorDistribuidorModel>> GetInvestidorByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor_distribuidor.*,
	                            tbl_investidor.nome_investidor,
	                            tbl_distribuidor.nome_distribuidor,
	                            tbl_administrador.nome_administrador
                            FROM 
	                            tbl_investidor_distribuidor
	                                INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
	                                INNER JOIN tbl_distribuidor ON tbl_investidor_distribuidor.cod_distribuidor = tbl_distribuidor.id
	                                INNER JOIN tbl_administrador ON tbl_investidor_distribuidor.cod_administrador = tbl_administrador.id
                            WHERE 
	                            tbl_investidor_distribuidor.cod_investidor = @cod_investidor";

                return await connection.QueryAsync<InvestidorDistribuidorModel>(query, new { cod_investidor = id });
            }
        }

        public async Task<IEnumerable<InvestidorDistribuidorModel>> GetByIdsAsync(int codInvestidor, int codDistribuidor, int codAdministrador)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor_distribuidor.*,
	                            tbl_investidor.nome_investidor,
	                            tbl_distribuidor.nome_distribuidor,
	                            tbl_administrador.nome_administrador
                            FROM 
	                            tbl_investidor_distribuidor
	                                INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
	                                INNER JOIN tbl_distribuidor ON tbl_investidor_distribuidor.cod_distribuidor = tbl_distribuidor.id
	                                INNER JOIN tbl_administrador ON tbl_investidor_distribuidor.cod_administrador = tbl_administrador.id
                            WHERE 
	                            tbl_investidor_distribuidor.cod_investidor = @cod_investidor AND
                                tbl_investidor_distribuidor.cod_distribuidor = @cod_distribuidor AND
                                tbl_investidor_distribuidor.cod_administrador = @cod_administrador";

                return await connection.QueryAsync<InvestidorDistribuidorModel>(query, new { cod_investidor = codInvestidor, cod_distribuidor = codDistribuidor, cod_administrador = codAdministrador });
            }
        }

        public async Task<InvestidorDistribuidorModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor_distribuidor.*,
	                            tbl_investidor.nome_investidor,
	                            tbl_distribuidor.nome_distribuidor,
	                            tbl_administrador.nome_administrador
                            FROM 
	                            tbl_investidor_distribuidor
	                                INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
	                                INNER JOIN tbl_distribuidor ON tbl_investidor_distribuidor.cod_distribuidor = tbl_distribuidor.id
	                                INNER JOIN tbl_administrador ON tbl_investidor_distribuidor.cod_administrador = tbl_administrador.id
                            WHERE 
	                            tbl_investidor_distribuidor.id = @id";

                return await connection.QueryFirstOrDefaultAsync<InvestidorDistribuidorModel>(query, new { id });
            }
        }

        public async Task<bool> UpdateAsync(InvestidorDistribuidorModel investDistribuidor)
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
                return await connection.ExecuteAsync(query, investDistribuidor) > 0;
            }
        }
    }
}
