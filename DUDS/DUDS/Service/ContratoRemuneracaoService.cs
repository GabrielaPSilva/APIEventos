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
    public class ContratoRemuneracaoService : GenericService<ContratoRemuneracaoModel>, IContratoRemuneracaoService
    {
        public ContratoRemuneracaoService() : base(new ContratoRemuneracaoModel(),
            "tbl_contrato_remuneracao",
            new List<string> { "'id'", "'data_criacao'" },
            new List<string> { "Id", "DataCriacao", "ListaCondicaoRemuneracao" },
            new List<string> { "'id'", "'data_criacao'", "'usuario_criacao'" },
            new List<string> { "Id", "DataCriacao", "UsuarioCriacao", "ListaCondicaoRemuneracao" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(ContratoRemuneracaoModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
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

        public async Task<IEnumerable<ContratoRemuneracaoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    contrato_remuneracao.*
                                     FROM
	                                    tbl_contrato_remuneração contrato_remuneracao
                                        INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.id = contrato_remuneracao.cod_contrato_fundo
                                        INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.id = contrato_fundo.cod_sub_contrato
                                        INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato
                                     WHERE
                                        contrato.ativo = 1
                                     ORDER BY
                                        contrato_remuneracao.id";

                return await connection.QueryAsync<ContratoRemuneracaoModel>(query);
            }
        }

        public async Task<ContratoRemuneracaoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                        contrato_remuneracao.*
                                       FROM
	                                        tbl_contrato_remuneração contrato_remuneracao
                                            INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.id = contrato_remuneracao.cod_contrato_fundo
                                            INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.id = contrato_fundo.cod_sub_contrato
                                            INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato
                                       WHERE
                                            contrato_remuneracao.id = @id
                                       ORDER BY
                                            contrato_remuneracao.id";
                return await connection.QueryFirstOrDefaultAsync<ContratoRemuneracaoModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<ContratoRemuneracaoModel>> GetContratoFundoByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    contrato_remuneracao.*
                                     FROM
	                                    tbl_contrato_remuneração contrato_remuneracao
                                        INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.id = contrato_remuneracao.cod_contrato_fundo
                                        INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.id = contrato_fundo.cod_sub_contrato
                                        INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato
                                     WHERE
                                        contrato_fundo.id = @id
                                     ORDER BY
                                        contrato_remuneracao.id";

                List<ContratoRemuneracaoModel> contratoRemuneracaoModels = await connection.QueryAsync<ContratoRemuneracaoModel>(query, new { id }) as List<ContratoRemuneracaoModel>;
                CondicaoRemuneracaoService condicaoRemuneracaoService = new CondicaoRemuneracaoService();

                Parallel.ForEach(contratoRemuneracaoModels, new ParallelOptions { MaxDegreeOfParallelism = maxParalleProcess }, async item =>
                {
                    List<CondicaoRemuneracaoModel> condicaoRemuneracaoModels = await condicaoRemuneracaoService.GetContratoRemuneracaoByIdAsync(item.Id) as List<CondicaoRemuneracaoModel>;
                    item.ListaCondicaoRemuneracao = condicaoRemuneracaoModels;
                });

                //foreach (ContratoRemuneracaoModel item in contratoRemuneracaoModels)
                //{
                //    List<CondicaoRemuneracaoModel> condicaoRemuneracaoModels = await condicaoRemuneracaoService.GetContratoRemuneracaoByIdAsync(item.Id) as List<CondicaoRemuneracaoModel>;
                //    item.ListaCondicaoRemuneracao = condicaoRemuneracaoModels;
                //}

                return contratoRemuneracaoModels;
            }
        }

        public async Task<bool> UpdateAsync(ContratoRemuneracaoModel item)
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
    }
}
