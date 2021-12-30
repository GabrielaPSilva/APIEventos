﻿using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Z.Dapper.Plus;

namespace DUDS.Service
{
    public class InvestidorService : GenericService<InvestidorModel>, IInvestidorService
    {
        public InvestidorService() : base(new InvestidorModel(),
                                            "tbl_investidor",
                                            new List<string> { "'id'", "'data_criacao'", "'ativo'" },
                                            new List<string> { "Id", "DataCriacao", "Ativo", "NomeAdministrador", "NomeGestor", "ListaInvestDistribuidor" },
                                            new List<string> { "'id'", "'data_criacao'", "'ativo'", "'usuario_criacao'" },
                                            new List<string> { "Id", "DataCriacao", "Ativo", "UsuarioCriacao", "NomeAdministrador", "NomeGestor", "ListaInvestDistribuidor" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }


        #region Investidor
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

        public async Task<bool> AddAsync(InvestidorModel investidor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                    try
                    {
                        string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                        var retorno = await connection.ExecuteAsync(sql: query, param: investidor);                        
                        return retorno > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                
            }
        }

        public async Task<IEnumerable<InvestidorModel>> AddInvestidores(List<InvestidorModel> investidores)
        {
            ConcurrentBag<InvestidorModel> vs = new ConcurrentBag<InvestidorModel>();
            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess };
            await Parallel.ForEachAsync(investidores, parallelOptions, async (x, cancellationToken) =>
            {
                var result = await AddAsync(x);
                if (!result) { vs.Add(x); }
            }
            );
            return vs;
        }

        public Task<bool> DeleteAsync(int id)
        {
            return DisableAsync(id);
        }

        public async Task<bool> DisableAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", _tableName);
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

        public async Task<IEnumerable<InvestidorModel>> GetAllAsync()
        {
            List<InvestidorModel> investidores;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
	                            tbl_investidor.*,
	                            tbl_administrador.nome_administrador AS NomeAdministrador,
	                            tbl_gestor.nome_gestor AS NomeGestor
                             FROM
	                            tbl_investidor
                                LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
		                        LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
                             WHERE
	                            tbl_investidor.ativo = 1
                             ORDER BY
	                            tbl_investidor.nome_investidor";

                investidores = await connection.QueryAsync<InvestidorModel>(query) as List<InvestidorModel>;
                return investidores;

                /*
                Parallel.ForEach(investidores, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, x =>
                {
                    List<InvestidorDistribuidorModel> investDistriList = investDistriService.GetInvestidorByIdAsync(x.Id) as List<InvestidorDistribuidorModel>;
                    x.ListaInvestDistribuidor = investDistriList;
                });

                /*
                var investidores = await connection.QueryAsync<InvestidorModel, List<InvestidorDistribuidorModel>, InvestidorModel>(query,
                    (investidor, investidorDistribuidor) =>
                    {
                        investidor.ListaInvestDistribuidor = investidorDistribuidor;
                        return investidor;
                    }, new {},
                    splitOn: "id"
                );
                */
            }
            /*
            InvestidorDistribuidorService investDistriService = new InvestidorDistribuidorService();

            var tasks = investidores.Select(async invest =>
            {
                var result = await investDistriService.GetInvestidorByIdAsync(invest.Id) as List<InvestidorDistribuidorModel>;
                invest.ListaInvestDistribuidor = result;
            });
            await Task.WhenAll(tasks);
            
            return investidores;
            */
        }

        public async Task<InvestidorModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor.*,
	                            tbl_administrador.nome_administrador,
	                            tbl_gestor.nome_gestor
                             FROM
	                            tbl_investidor
		                        LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
		                        LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
                              WHERE 
	                            tbl_investidor.id = @id";


                InvestidorModel investidor = await connection.QueryFirstOrDefaultAsync<InvestidorModel>(query, new { id });

                if (investidor != null)
                {
                    InvestidorDistribuidorService investDistribuidorService = new InvestidorDistribuidorService();
                    List<InvestidorDistribuidorModel> investDistribuidorList = await investDistribuidorService.GetInvestidorByIdAsync(investidor.Id) as List<InvestidorDistribuidorModel>;
                    investidor.ListaInvestDistribuidor = investDistribuidorList;
                }
                //return null;
                return investidor;
            }
        }

        public async Task<InvestidorModel> GetInvestidorExistsBase(string cnpj)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor.*,
	                            tbl_administrador.nome_administrador,
	                            tbl_gestor.nome_gestor
                             FROM
	                            tbl_investidor
		                        LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
		                        LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
                            WHERE 
	                            tbl_investidor.cnpj = @cnpj";

                InvestidorModel investidor = await connection.QueryFirstOrDefaultAsync<InvestidorModel>(query, new { cnpj });

                if (investidor == null)
                {
                    return null;
                }

                InvestidorDistribuidorService investDistribuidorService = new InvestidorDistribuidorService();
                List<InvestidorDistribuidorModel> investDistribuidorList = await investDistribuidorService.GetInvestidorByIdAsync(investidor.Id) as List<InvestidorDistribuidorModel>;
                investidor.ListaInvestDistribuidor = investDistribuidorList;

                return investidor;
            }
        }

        public async Task<IEnumerable<InvestidorModel>> GetInvestidorByDataCriacao(DateTime dataCriacao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor.*,
	                            tbl_administrador.nome_administrador,
	                            tbl_gestor.nome_gestor
                             FROM
	                            tbl_investidor
		                        LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
		                        LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
                             WHERE
                                tbl_investidor.data_criacao = @data_criacao";

                return await connection.QueryAsync<InvestidorModel>(query, new { data_criacao = dataCriacao });
            }
        }

        public async Task<bool> UpdateAsync(InvestidorModel investidor)
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
                        var retorno = await connection.ExecuteAsync(sql: query, param: investidor, transaction: transaction);
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
        #endregion
    }
}
