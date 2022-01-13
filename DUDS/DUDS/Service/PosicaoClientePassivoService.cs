using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class PosicaoClientePassivoService : GenericService<PosicaoClienteModel>, IPosicaoClientePassivoService
    {
        public PosicaoClientePassivoService() : base(new PosicaoClientePassivoModel(),
                   "tbl_posicao_cliente",
                   new List<string> { "'id'" },
                   new List<string> { "Id", "NomeInvestidor", "NomeFundo", "NomeAdministradorInvestidor", "NomeAdministradorDados", "NomeDistribuidor", "NomeGestor" },
                   new List<string> { "'id'" },
                   new List<string> { "Id", "NomeInvestidor", "NomeFundo", "NomeAdministradorInvestidor", "NomeAdministradorDados", "NomeDistribuidor", "NomeGestor" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddAsync(PosicaoClienteModel item)
        {
            if (item == null) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                        var retorno = await connection.ExecuteAsync(sql: query, param: item, transaction: transaction, commandTimeout: 180);
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

        public async Task<IEnumerable<PosicaoClienteModel>> AddBulkAsync(List<PosicaoClienteModel> item)
        {
            ConcurrentBag<PosicaoClienteModel> vs = new ConcurrentBag<PosicaoClienteModel>();
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

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeleteByDataRefAsync(DateTime dataRef)
        {
            List<PosicaoClienteModel> result = await GetByParametersAsync(dataInicio: dataRef, dataFim: null, codDistribuidor: null, codGestor: null, codInvestidorDistribuidor: null) as List<PosicaoClienteModel>;
            if (result == null) return false;
            if (result.Count == 0) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string query = "DELETE FROM tbl_posicao_cliente WHERE data_ref = @data_ref";
                        int rowsAffected = await connection.ExecuteAsync(sql: query, param: new { data_ref = dataRef }, transaction: transaction,commandTimeout:180);
                        transaction.Commit();
                        return rowsAffected > 0 && rowsAffected == result.Count;
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
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PosicaoClienteModel>> GetAllAsync()
        {
            throw new System.NotImplementedException();
            /*
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"
                            SELECT
	                            tbl_posicao_cliente.*,
	                            tbl_fundo.nome_reduzido AS nome_fundo,
	                            tbl_investidor.nome_investidor,
	                            tbl_administrador.nome_administrador
                            FROM
	                            tbl_posicao_cliente
		                        INNER JOIN tbl_fundo ON tbl_posicao_cliente.cod_fundo = tbl_fundo.id
                                INNER JOIN tbl_investidor_distribuidor ON tbl_posicao_cliente.cod_investidor_distribuidor = tbl_investidor_distribuidor.id
		                        INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
		                        INNER JOIN tbl_administrador ON tbl_posicao_cliente.cod_administrador = tbl_administrador.id";

                return await connection.QueryAsync<PosicaoClientePassivo>(query);
            }
            */
        }

        /*
        public async Task<IEnumerable<PosicaoClientePassivoModel>> GetByDataRefAsync(DateTime dataRef)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"
                            SELECT
	                            tbl_posicao_cliente.*,
	                            tbl_fundo.nome_reduzido AS nome_fundo,
	                            tbl_investidor.nome_investidor,
	                            tbl_administrador.nome_administrador
                            FROM
	                            tbl_posicao_cliente
		                        INNER JOIN tbl_fundo ON tbl_posicao_cliente.cod_fundo = tbl_fundo.id
                                INNER JOIN tbl_investidor_distribuidor ON tbl_posicao_cliente.cod_investidor_distribuidor = tbl_investidor_distribuidor.id
		                        INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
		                        INNER JOIN tbl_administrador ON tbl_posicao_cliente.cod_administrador = tbl_administrador.id
                            WHERE
                                tbl_posicao_cliente.data_ref = @data_ref";

                return await connection.QueryAsync<PosicaoClientePassivoModel>(query, new {data_ref = dataRef});
            }
        }
        */

        public async Task<IEnumerable<PosicaoClienteModel>> GetByParametersAsync(DateTime? dataInicio, DateTime? dataFim, int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor)
        {
            //if (dataInicio == null && dataFim == null) return new List<PosicaoClientePassivoModel>();
            if (dataInicio != null && dataFim == null) dataFim = dataInicio;
            if (dataInicio == null && dataFim != null) dataInicio = dataFim;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"
                            SELECT
	                            tbl_posicao_cliente.*,
	                            tbl_fundo.nome_reduzido AS nome_fundo,
	                            tbl_investidor.nome_investidor,
	                            adm_investidor.nome_administrador AS nome_administrador_investidor,
	                            tbl_gestor.nome_gestor,
	                            tbl_distribuidor.nome_distribuidor,
	                            tbl_administrador.nome_administrador AS nome_administrador_dados
                            FROM
	                            tbl_posicao_cliente
	                            INNER JOIN tbl_fundo ON tbl_posicao_cliente.cod_fundo = tbl_fundo.id
	                            INNER JOIN tbl_tipo_estrategia ON tbl_fundo.cod_tipo_estrategia = tbl_tipo_estrategia.id
                                INNER JOIN tbl_investidor_distribuidor ON tbl_posicao_cliente.cod_investidor_distribuidor = tbl_investidor_distribuidor.id
	                            INNER JOIN tbl_administrador ON tbl_posicao_cliente.cod_administrador = tbl_administrador.id
                                INNER JOIN tbl_distribuidor_administrador ON tbl_investidor_distribuidor.cod_distribuidor_administrador = tbl_distribuidor_administrador.id
	                            INNER JOIN tbl_distribuidor ON tbl_distribuidor.id = tbl_distribuidor_administrador.cod_distribuidor
	                            INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
                                LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
	                            LEFT JOIN tbl_administrador adm_investidor ON tbl_investidor.cod_administrador = adm_investidor.id
                            WHERE
                                (@data_inicio IS NULL OR tbl_posicao_cliente.data_ref >= @data_inicio) 
                                AND (@data_fim IS NULL OR tbl_posicao_cliente.data_ref <= @data_fim)
                                AND (@cod_distribuidor IS NULL OR tbl_investidor_distribuidor.cod_distribuidor_administrador = @cod_distribuidor)
                                AND (@cod_gestor IS NULL OR tbl_investidor.cod_gestor = @cod_gestor)
                                AND (@cod_investidor_distribuidor IS NULL OR tbl_investidor_distribuidor.id = @cod_investidor_distribuidor)
                            ORDER BY
	                            tbl_fundo.tipo_cota,
	                            tbl_tipo_estrategia.estrategia,
	                            tbl_fundo.data_cota_inicial,
	                            tbl_investidor.tipo_investidor,
	                            adm_investidor.nome_administrador,
	                            tbl_gestor.nome_gestor,
	                            tbl_distribuidor.nome_distribuidor";

                return await connection.QueryAsync<PosicaoClienteModel>(query, new
                {
                    data_inicio = dataInicio,
                    data_fim = dataFim,
                    cod_distribuidor = codDistribuidor,
                    cod_gestor = codGestor,
                    cod_investidor_distribuidor = codInvestidorDistribuidor
                });
            }
        }

        public Task<PosicaoClienteModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateAsync(PosicaoClienteModel item)
        {
            if (item == null) return false;

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
                        var retorno = await connection.ExecuteAsync(sql: query, param: item, transaction:transaction);
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

        public async Task<PosicaoClienteModel> GetMaxValorBrutoAsync(int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor)
        {
            if (!codDistribuidor.HasValue && !codGestor.HasValue && !codInvestidorDistribuidor.HasValue) return new PosicaoClienteModel();

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"
                            SELECT
                                tbl_posicao_cliente.data_ref,
	                            MAX(tbl_posicao_cliente.valor_bruto) AS valor_bruto,
	                            tbl_fundo.nome_reduzido AS nome_fundo,
	                            tbl_investidor.nome_investidor,
	                            tbl_administrador.nome_administrador,
                                tbl_distribuidor.nome_distribuidor,
                                tbl_gestor.nome_gestor
                            FROM
	                            tbl_posicao_cliente
		                        INNER JOIN tbl_fundo ON tbl_posicao_cliente.cod_fundo = tbl_fundo.id
                                INNER JOIN tbl_investidor_distribuidor ON tbl_posicao_cliente.cod_investidor_distribuidor = tbl_investidor_distribuidor.id
		                        INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
		                        INNER JOIN tbl_administrador ON tbl_posicao_cliente.cod_administrador = tbl_administrador.id
                                INNER JOIN tbl_distribuidor ON tbl_investidor_distribuidor.cod_distribuidor = tbl_distribuidor.id
                                INNER JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
                            WHERE
                                (@cod_distribuidor IS NULL or tbl_distribuidor.id = @cod_distribuidor)
                                AND (@cod_gestor IS NULL or tbl_investidor.cod_gestor = @cod_gestor)
                                AND (@cod_investidor_distribuidor IS NULL or tbl_investidor_distribuidor.id = @cod_investidor_distribuidor)";

                return await connection.QueryFirstOrDefaultAsync<PosicaoClienteModel>(query, new
                {
                    cod_distribuidor = codDistribuidor,
                    cod_gestor = codGestor,
                    cod_investidor_distribuidor = codInvestidorDistribuidor
                });
            }
        }
    }
}
