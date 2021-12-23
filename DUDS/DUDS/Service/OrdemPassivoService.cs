using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class OrdemPassivoService : GenericService<OrdemPassivoModel>, IOrdemPassivoService
    {
        public OrdemPassivoService() : base(new OrdemPassivoModel(),
                   "tbl_ordem_passivo",
                   new List<string> { "'id'" },
                   new List<string> { "Id","NomeInvestidor", "NomeFundo", "NomeAdministrador" },
                   new List<string> { "'id'" },
                   new List<string> { "Id", "NomeInvestidor", "NomeFundo", "NomeAdministrador" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddAsync(OrdemPassivoModel item)
        {
            if (item == null) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<IEnumerable<OrdemPassivoModel>> AddBulkAsync(List<OrdemPassivoModel> item)
        {
            ConcurrentBag<OrdemPassivoModel> vs = new ConcurrentBag<OrdemPassivoModel>();
            /*
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                _ = Parallel.ForEach(item, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, x =>
                {
                    var result = AddAsync(x);
                    if (result.Result) { vs.Add(result.Result); }
                });

                // return GetInvestidorByDataCriacao(investidor.FirstOrDefault().DataCriacao).Result.ToArray().Length == investidor.Count;
                return vs.Count == item.Count;
            }
            */
            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess };
            await Parallel.ForEachAsync(item, parallelOptions, async (x, cancellationToken) =>
            {
                var result = await AddAsync(x);
                if (!result) { vs.Add(x); }
            }
            );
            return vs;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteByDataRefAsync(DateTime dataEntrada)
        {
            // if (dataRef == null) return false;

            List<OrdemPassivoModel> result = await GetByDataEntradaAsync(dataEntrada: dataEntrada) as List<OrdemPassivoModel>;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = "DELETE FROM tbl_ordem_passivo WHERE data_entrada = @data_entrada";
                int rowsAffected = await connection.ExecuteAsync(query, new { data_entrada = dataEntrada });
                return rowsAffected > 0 && rowsAffected == result.Count;
            }
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<OrdemPassivoModel>> GetAllAsync()
        {
            return GetByDataEntradaAsync(null);
        }

        public async Task<IEnumerable<OrdemPassivoModel>> GetByDataEntradaAsync(DateTime? dataEntrada)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"
                            SELECT
	                            tbl_ordem_passivo.*,
	                            tbl_fundo.nome_reduzido AS nome_fundo,
	                            tbl_investidor.nome_investidor,
	                            tbl_administrador.nome_administrador
                            FROM
	                            tbl_ordem_passivo
		                        INNER JOIN tbl_fundo ON tbl_ordem_passivo.cod_fundo = tbl_fundo.id
                                INNER JOIN tbl_investidor_distribuidor ON tbl_ordem_passivo.cod_investidor_distribuidor = tbl_investidor_distribuidor.id
		                        INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
		                        INNER JOIN tbl_administrador ON tbl_ordem_passivo.cod_administrador = tbl_administrador.id
                                
                            WHERE
                                (@data_entrada IS NULL OR tbl_ordem_passivo.data_entrada = @data_entrada)
                            ORDER BY
                                tbl_fundo.nome_reduzido,
	                            tbl_investidor.nome_investidor,
                                tbl_ordem_passivo.descricao_operacao";

                return await connection.QueryAsync<OrdemPassivoModel>(query, new{data_entrada = dataEntrada});
            }
        }

        public Task<OrdemPassivoModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateAsync(OrdemPassivoModel item)
        {
            if (item == null) return false;

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
    }
}
