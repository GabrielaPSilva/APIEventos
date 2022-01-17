﻿using Dapper;
using DUDS.Models.Contrato;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ContratoService : GenericService<ContratoModel>, IContratoService
    {
        public ContratoService() : base(new ContratoModel(),"tbl_contrato")
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

        public async Task<bool> AddAsync(ContratoModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            return DisableAsync(id);
        }

        public async Task<bool> DisableAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<ContratoViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoService.QUERY_BASE +
                    @"
                    WHERE
	                    tbl_contrato.ativo = 1";
                List<ContratoViewModel> listaContratoModel = await connection.QueryAsync<ContratoViewModel>(query) as List<ContratoViewModel>;

                /*
                SubContratoService subContratoService = new SubContratoService();
                Parallel.ForEach(listaContratoModel, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, async x =>
                {
                    List<SubContratoModel> listaSubContrato = await subContratoService.GetContratoByIdAsync(x.Id) as List<SubContratoModel>;
                    x.ListaSubContrato = listaSubContrato;
                });
                */
                //foreach (ContratoModel item in listaContratoModel)
                //{
                //    List<SubContratoModel> listaSubContrato = await subContratoService.GetSubContratoCompletoByIdAsync(item.Id) as List<SubContratoModel>;
                //    item.ListaSubContrato = listaSubContrato;
                //}

                return listaContratoModel;
            }
        }

        public async Task<ContratoViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoService.QUERY_BASE + 
                    @"
                    WHERE
                        id = @id";

                return await connection.QueryFirstOrDefaultAsync<ContratoViewModel>(query, new { id });
            }
        }

        public async Task<bool> UpdateAsync(ContratoModel item)
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

        public async Task<IEnumerable<EstruturaContratoViewModel>> GetContratosRebateAsync(string subContratoStatus)
        {
            string whereClause;
            if (subContratoStatus == "Ativo")
            {
                subContratoStatus = "Inativo";
                whereClause = "sub_contrato.status <> @Status";
            }
            else
            {
                whereClause = "sub_contrato.status = @Status";
            }
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = @"
                            SELECT 
                                contrato_remuneracao.percentual_adm,
                                contrato_remuneracao.percentual_pfee,
                                contrato.cod_tipo_contrato,
                                contrato.cod_gestor,
                                contrato.cod_distribuidor,
                                sub_contrato.versao,
                                sub_contrato.status,
                                sub_contrato.clausula_retroatividade,
                                sub_contrato.data_retroatividade,
                                sub_contrato.data_vigencia_inicio,
                                sub_contrato.data_vigencia_fim,
                                contrato_alocador.cod_investidor AS cod_investidor_contrato,
                                contrato_fundo.cod_fundo,
                                contrato_fundo.cod_tipo_condicao,
                                investidor_distribuidor.cod_invest_administrador AS cod_invest_administrador,
                                investidor_distribuidor.cod_administrador AS administrador_codigo_investidor,
                                investidor_distribuidor.cod_distribuidor_administrador AS distribuidor_codigo_investidor,
                                contrato.id AS cod_contrato,
                                sub_contrato.id AS cod_sub_contrato,
                                contrato_fundo.id AS cod_contrato_fundo,
                                contrato_remuneracao.id AS cod_contrato_remuneracao
                            FROM
                                tbl_contrato contrato
                                INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.cod_contrato = contrato.id
                                INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.cod_sub_contrato = sub_contrato.id
                                INNER JOIN tbl_contrato_remuneracao contrato_remuneracao ON contrato_remuneracao.cod_contrato_fundo = contrato_fundo.id
                                LEFT JOIN tbl_contrato_alocador contrato_alocador ON contrato_alocador.cod_sub_contrato = sub_contrato.id
                                LEFT JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.cod_investidor = contrato_alocador.cod_investidor
                            WHERE
                                WHERE_CLAUSE";
                query = query.Replace("WHERE_CLAUSE", whereClause);

                List<EstruturaContratoViewModel> listaContratoModel = await connection.QueryAsync<EstruturaContratoViewModel>(query, new { Status = subContratoStatus }) as List<EstruturaContratoViewModel>;
                return listaContratoModel;
            }
        }
    }
}
