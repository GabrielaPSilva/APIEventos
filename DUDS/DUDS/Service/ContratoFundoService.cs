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
    public class ContratoFundoService: GenericService<ContratoFundoModel>, IContratoFundoService
    {
        public ContratoFundoService() : base(new ContratoFundoModel(),
            "tbl_contrato_fundo",
            new List<string> { "'id'", "'data_criacao'" },
            new List<string> { "Id", "DataCriacao", "NomeFundo", "TipoCondicao", "ListaContratoRemuneracao" },
            new List<string> { "'id'", "'data_criacao'", "'usuario_criacao'" },
            new List<string> { "Id", "DataCriacao", "UsuarioCriacao", "NomeFundo", "TipoCondicao", "ListaContratoRemuneracao" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(ContratoFundoModel item)
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

        public async Task<IEnumerable<ContratoFundoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    contrato_fundo.*,
                                        fundo.nome_reduzido as nome_fundo,
                                        tipo_condicao.tipo_condicao
                                     FROM
	                                    tbl_contrato_fundo contrato_fundo
                                        INNER JOIN tbl_fundo fundo ON fundo.id = contrato_fundo.cod_fundo
                                        INNER JOIN tbl_tipo_condicao tipo_condicao ON tipo_condicao.id = contrato_fundo.cod_tipo_condicao
                                        INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrado.id = contrato_fundo.cod_sub_contrato
                                        INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato
                                     WHERE
                                        contrato.ativo = 1
                                     ORDER BY
                                        fundo.nome_reduzido";

                return await connection.QueryAsync<ContratoFundoModel>(query);
            }
        }

        public async Task<ContratoFundoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    contrato_fundo.*,
                                        fundo.nome_reduzido as nome_fundo,
                                        tipo_condicao.tipo_condicao
                                     FROM
	                                    tbl_contrato_fundo contrato_fundo
                                        INNER JOIN tbl_fundo fundo ON fundo.id = contrato_fundo.cod_fundo
                                        INNER JOIN tbl_tipo_condicao tipo_condicao ON tipo_condicao.id = contrato_fundo.cod_tipo_condicao
                                       WHERE
                                        contrato_fundo.id = @id
                                       ORDER BY
                                        fundo.nome_reduzido";
                return await connection.QueryFirstOrDefaultAsync<ContratoFundoModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<ContratoFundoModel>> GetSubContratoByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    contrato_fundo.*,
                                        fundo.nome_reduzido as nome_fundo,
                                        tipo_condicao.tipo_condicao
                                     FROM
	                                    tbl_contrato_fundo contrato_fundo
                                        INNER JOIN tbl_fundo fundo ON fundo.id = contrato_fundo.cod_fundo
                                        INNER JOIN tbl_tipo_condicao tipo_condicao ON tipo_condicao.id = contrato_fundo.cod_tipo_condicao
                                        INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrado.id = contrato_fundo.cod_sub_contrato
                                        INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato
                                     WHERE
                                        sub_contrato.id = @id
                                     ORDER BY
                                        fundo.nome_reduzido";
                List<ContratoFundoModel> contratoFundoModels = await connection.QueryAsync<ContratoFundoModel>(query, new { id }) as List<ContratoFundoModel>;
                ContratoRemuneracaoService contratoRemuneracaoService = new ContratoRemuneracaoService();

                foreach (var item in contratoFundoModels)
                {
                    List<ContratoRemuneracaoModel> contratoRemuneracaoModels = await contratoRemuneracaoService.GetContratoFundoByIdAsync(item.Id) as List<ContratoRemuneracaoModel>;
                    item.ListaContratoRemuneracao = contratoRemuneracaoModels;
                }

                return contratoFundoModels;
            }
        }

        public async Task<bool> UpdateAsync(ContratoFundoModel item)
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
