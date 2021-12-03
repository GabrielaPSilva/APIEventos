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
    public class PosicaoClientePassivoService : GenericService<PosicaoClientePassivoModel>, IPosicaoClientePassivoService
    {
        public PosicaoClientePassivoService() : base(new PosicaoClientePassivoModel(),
                   "tbl_posicao_cliente",
                   new List<string> { "'id'" },
                   new List<string> { "Id", "NomeInvestidor", "NomeFundo", "NomeAdministrador", "NomeDistribuidor", "NomeGestor" },
                   new List<string> { "'id'" },
                   new List<string> { "Id", "NomeInvestidor", "NomeFundo", "NomeAdministrador", "NomeDistribuidor", "NomeGestor" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddAsync(PosicaoClientePassivoModel item)
        {
            if (item == null) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> AddBulkAsync(List<PosicaoClientePassivoModel> item)
        {
            if (item == null) return false;
            if (item.Count == 0) return false;

            ConcurrentBag<bool> vs = new ConcurrentBag<bool>();
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
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new System.NotImplementedException();
            /*
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DELETE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
            */
        }

        public async Task<bool> DeleteByDataRefAsync(DateTime? dataRef)
        {
            if (dataRef == null) return false;

            List<PosicaoClientePassivoModel> result = await GetByParametersAsync(dataInicio: dataRef, dataFim: null, codDistribuidor: null, codGestor: null, codInvestidorDistribuidor: null) as List<PosicaoClientePassivoModel>;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = "DELETE FROM tbl_posicao_cliente WHERE competencia = @data_ref";
                int rowsAffected = await connection.ExecuteAsync(query, new { data_ref = dataRef });
                return rowsAffected > 0 && rowsAffected == result.Count;
            }
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PosicaoClientePassivoModel>> GetAllAsync()
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

        public async Task<IEnumerable<PosicaoClientePassivoModel>> GetByParametersAsync(DateTime? dataInicio, DateTime? dataFim, int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor)
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
                                (@data_inicio IS NULL OR tbl_posicao_cliente.data_ref >= @data_inicio) 
                                AND (@data_fim IS NULL OR tbl_posicao_cliente.data_ref <= @data_fim)
                                AND (@cod_distribuidor IS NULL OR tbl_investidor_distribuidor.cod_distribuidor = @cod_distribuidor)
                                AND (@cod_gestor IS NULL OR tbl_investidor.cod_gestor = @cod_gestor)
                                AND (@cod_investidor_distribuidor IS NULL OR tbl_investidor_distribuidor.id = @cod_investidor_distribuidor)";

                return await connection.QueryAsync<PosicaoClientePassivoModel>(query, new
                {
                    data_inicio = dataInicio,
                    data_fim = dataFim,
                    cod_distribuidor = codDistribuidor,
                    cod_gestor = codGestor,
                    cod_investidor_distribuidor = codInvestidorDistribuidor
                });
            }
        }

        public Task<PosicaoClientePassivoModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateAsync(PosicaoClientePassivoModel item)
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

        public async Task<PosicaoClientePassivoModel> GetMaxValorBrutoAsync(int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor)
        {
            if (!codDistribuidor.HasValue && !codGestor.HasValue && !codInvestidorDistribuidor.HasValue) return new PosicaoClientePassivoModel();

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

                return await connection.QueryFirstOrDefaultAsync<PosicaoClientePassivoModel>(query, new
                {
                    cod_distribuidor = codDistribuidor,
                    cod_gestor = codGestor,
                    cod_investidor_distribuidor = codInvestidorDistribuidor
                });
            }
        }
    }
}
