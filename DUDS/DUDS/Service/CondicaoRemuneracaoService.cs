﻿using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class CondicaoRemuneracaoService : GenericService<CondicaoRemuneracaoModel>, ICondicaoRemuneracaoService
    {
        public CondicaoRemuneracaoService() : base(new CondicaoRemuneracaoModel(),
            "tbl_condicao_remuneracao",
            new List<string> { "'id'", "'data_criacao'" },
            new List<string> { "Id", "DataCriacao", "NomeFundo" },
            new List<string> { "'id'", "'data_criacao'", "'usuario_criacao'" },
            new List<string> { "Id", "DataCriacao", "UsuarioCriacao", "NomeFundo" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(CondicaoRemuneracaoModel item)
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

        public async Task<IEnumerable<CondicaoRemuneracaoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            condicao_remuneracao.*,
	                            fundo.nome_reduzido as nome_fundo
                             FROM
	                            tbl_condicao_remuneracao condicao_remuneracao
	                            INNER JOIN tbl_fundo fundo ON fundo.id = condicao_remuneracao.cod_fundo
                                INNER JOIN tbl_contrato_remuneracao contrato_remuneracao ON contrato_remuneracao.id = condicao_remuneracao.cod_contrato_remuneracao
                                INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.id = contrato_remuneracao.cod_contrato_fundo
                                INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.id = contrato_fundo.cod_sub_contrato
                                INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato
                             WHERE
	                            contrato.ativo = 1";

                return await connection.QueryAsync<CondicaoRemuneracaoModel>(query);
            }
        }

        public async Task<CondicaoRemuneracaoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                        SELECT 
	                                        condicao_remuneracao.*,
	                                        fundo.nome_reduzido as nome_fundo
                                         FROM
	                                        tbl_condicao_remuneracao condicao_remuneracao
	                                        INNER JOIN tbl_fundo fundo ON fundo.id = condicao_remuneracao.cod_fundo
                                            INNER JOIN tbl_contrato_remuneracao contrato_remuneracao ON contrato_remuneracao.id = condicao_remuneracao.cod_contrato_remuneracao
                                            INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.id = contrato_remuneracao.cod_contrato_fundo
                                            INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.id = contrato_fundo.cod_sub_contrato
                                            INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato
                                        WHERE
                                            condicao_remuneracao.id = @id";

                return await connection.QueryFirstOrDefaultAsync<CondicaoRemuneracaoModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<CondicaoRemuneracaoModel>> GetContratoRemuneracaoByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            condicao_remuneracao.*,
	                            fundo.nome_reduzido as nome_fundo
                             FROM
	                            tbl_condicao_remuneracao condicao_remuneracao
	                            INNER JOIN tbl_fundo fundo ON fundo.id = condicao_remuneracao.cod_fundo
                                INNER JOIN tbl_contrato_remuneracao contrato_remuneracao ON contrato_remuneracao.id = condicao_remuneracao.cod_contrato_remuneracao
                                INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.id = contrato_remuneracao.cod_contrato_fundo
                                INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.id = contrato_fundo.cod_sub_contrato
                                INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato
                             WHERE
	                            contrato_remuneracao.id = @id
                             ORDER BY
                                condicao_remuneracao.id,
                                fundo.nome_reduzido";

                return await connection.QueryAsync<CondicaoRemuneracaoModel>(query, new { id });
            }
        }

        public async Task<bool> UpdateAsync(CondicaoRemuneracaoModel item)
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
