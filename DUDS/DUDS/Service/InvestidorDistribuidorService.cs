using Dapper;
using DUDS.Models;
using DUDS.Models.Investidor;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class InvestidorDistribuidorService : GenericService<InvestidorDistribuidorModel>, IInvestidorDistribuidorService
    {
        public InvestidorDistribuidorService() : base(new InvestidorDistribuidorModel(), "tbl_investidor_distribuidor")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", _tableName);
                        var retorno = await connection.ExecuteAsync(sql: query, param: new { id }, transaction: transaction);
                        transaction.Commit();
                        return retorno > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<bool> AddAsync(InvestidorDistribuidorModel investDistribuidor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                        var retorno = await connection.ExecuteAsync(sql: query, param: investDistribuidor, transaction: transaction);
                        transaction.Commit();
                        return retorno > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<IEnumerable<InvestidorDistribuidorModel>> AddInvestidorDistribuidores(List<InvestidorDistribuidorModel> investDistribuidor)
        {
            ConcurrentBag<InvestidorDistribuidorModel> vs = new ConcurrentBag<InvestidorDistribuidorModel>();
            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess };
            await Parallel.ForEachAsync(investDistribuidor, parallelOptions, async (x, cancellationToken) =>
            {
                var result = await AddAsync(x);
                if (!result) { vs.Add(x); }
            }
            );
            return vs;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.DELETE_COMMAND.Replace("TABELA", _tableName);
                        var retorno = await connection.ExecuteAsync(sql: query, param: new { id }, transaction: transaction);
                        transaction.Commit();
                        return retorno > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
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
                return await connection.QueryAsync<InvestidorDistribuidorModel>(IInvestidorDistribuidorService.QUERY_BASE);
            }
        }

        public async Task<IEnumerable<InvestidorDistribuidorViewModel>> GetInvestidorDistribuidorByCodInvestidorAsync(int codInvestidor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IInvestidorDistribuidorService.QUERY_BASE +
                    @"
                    WHERE 
	                    tbl_investidor_distribuidor.cod_investidor = @cod_investidor";

                return await connection.QueryAsync<InvestidorDistribuidorViewModel>(query, new { cod_investidor = codInvestidor });
            }
        }

        public async Task<IEnumerable<InvestidorDistribuidorViewModel>> GetInvestidorDistribuidorByCodAdministradorAsync(int codInvestidorAdministrador)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IInvestidorDistribuidorService.QUERY_BASE +
                    @"
                    WHERE 
	                    tbl_investidor_distribuidor.cod_invest_administrador = @cod_invest_administrador";

                return await connection.QueryAsync<InvestidorDistribuidorViewModel>(query, new { cod_invest_administrador = codInvestidorAdministrador });
            }
        }

        public async Task<IEnumerable<InvestidorDistribuidorViewModel>> GetByIdsAsync(int codInvestidor, int codDistribuidorAdministrador, int codAdministrador)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IInvestidorDistribuidorService.QUERY_BASE + 
                    @"
                    WHERE 
	                    tbl_investidor_distribuidor.cod_investidor = @cod_investidor AND
                        tbl_investidor_distribuidor.cod_distribuidor_administrador = @cod_distribuidor_administrador AND
                        tbl_investidor_distribuidor.cod_administrador = @cod_administrador";

                return await connection.QueryAsync<InvestidorDistribuidorViewModel>(query, new { cod_investidor = codInvestidor, cod_distribuidor_administrador = codDistribuidorAdministrador, cod_administrador = codAdministrador });
            }
        }

        public async Task<InvestidorDistribuidorModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IInvestidorDistribuidorService.QUERY_BASE + 
                    @"
                    WHERE 
	                    tbl_investidor_distribuidor.id = @id";

                return await connection.QueryFirstOrDefaultAsync<InvestidorDistribuidorModel>(query, new { id });
            }
        }

        /*
        public async Task<IEnumerable<InvestidorDistribuidorModel>> GetInvestidorDistribuidorByDataCriacao(DateTime dataCriacao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor_distribuidor.*,
	                            tbl_investidor.nome_investidor,
	                            tbl_distribuidor.nome_distribuidor,
	                            tbl_administrador.nome_administrador,
	                            tbl_tipo_contrato.tipo_contrato,
	                            tbl_grupo_rebate.nome_grupo_rebate
                            FROM 
	                            tbl_investidor_distribuidor
	                            INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
	                            INNER JOIN tbl_distribuidor_administrador ON tbl_investidor_distribuidor.cod_distribuidor_administrador = tbl_distribuidor_administrador.id
                                INNER JOIN tbl_distribuidor ON tbl_distribuidor.id = tbl_distribuidor_administrador.cod_distribuidor
	                            INNER JOIN tbl_administrador ON tbl_investidor_distribuidor.cod_administrador = tbl_administrador.id
                            INNER JOIN tbl_tipo_contrato ON tbl_investidor_distribuidor.cod_tipo_contrato = tbl_tipo_contrato.id
		                        INNER JOIN tbl_grupo_rebate ON tbl_investidor_distribuidor.cod_grupo_rebate = tbl_grupo_rebate.id
                            WHERE 
                                tbl_investidor_distribuidor.data_criacao = @data_criacao";

                return await connection.QueryAsync<InvestidorDistribuidorModel>(query, new { data_criacao = dataCriacao });
            }
        }
        */

        public async Task<bool> UpdateAsync(InvestidorDistribuidorModel investDistribuidor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.UPDATE_COMMAND.Replace("TABELA", _tableName);
                        List<string> str = new List<string>();
                        for (int i = 0; i < _propertiesUpdate.Count; i++)
                        {
                            str.Add(_fieldsUpdate[i] + " = " + _propertiesUpdate[i]);
                        }
                        query = query.Replace("VALORES", String.Join(",", str));
                        var retorno = await connection.ExecuteAsync(sql: query, param: investDistribuidor, transaction: transaction);
                        transaction.Commit();
                        return retorno > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
