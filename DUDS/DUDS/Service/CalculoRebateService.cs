﻿using Dapper;
using DUDS.Models.Contrato;
using DUDS.Models.Filtros;
using DUDS.Models.Rebate;
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
    public class CalculoRebateService : GenericService<CalculoRebateModel>, ICalculoRebateService
    {
        public CalculoRebateService() : base(new CalculoRebateModel(),
                                        "tbl_calculo_pgto_adm_pfee")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(CalculoRebateModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                        var retorno = await connection.ExecuteAsync(sql: query, param: item, transaction: transaction);
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

        public async Task<IEnumerable<CalculoRebateModel>> AddBulkAsync(List<CalculoRebateModel> item)
        {
            ConcurrentBag<CalculoRebateModel> vs = new ConcurrentBag<CalculoRebateModel>();
            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess };
            await Parallel.ForEachAsync(item, parallelOptions, async (x, cancellationToken) =>
            {
                var result = await AddAsync(x);
                if (!result) { vs.Add(x); }
            }
            );
            return vs;
        }

        public Task<bool> UpdateAsync(CalculoRebateModel item)
        {
            throw new NotImplementedException();
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

        public async Task<bool> DeleteByCompetenciaAsync(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string query = "DELETE FROM tbl_calculo_pgto_adm_pfee WHERE competencia = @competencia";
                        var retorno = await connection.ExecuteAsync(sql: query, param: new { competencia }, transaction: transaction);
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

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
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

        public async Task<IEnumerable<CalculoRebateViewModel>> GetByCompetenciaAsync(string competencia, int codGrupoRebate)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICalculoRebateService.QUERY_BASE + 
                                  @"
								    WHERE
										pagamento.competencia = @competencia AND
                                        grupo_rebate.id = @id
                                    ORDER BY
	                                    fundo.nome_reduzido,
	                                    grupo_rebate.nome_grupo_rebate,
	                                    tipo_contrato.tipo_contrato";

                return await connection.QueryAsync<CalculoRebateViewModel>(query, new { competencia, id = codGrupoRebate });
            }
        }

        public async Task<CalculoRebateViewModel> GetByIdAsync(Guid id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICalculoRebateService.QUERY_BASE + 
                                 @"
                                    WHERE
	                                    calculo_pgto_adm_pfee.id = @id
                                    ORDER BY
	                                    fundo.nome_reduzido,
	                                    grupo_rebate.nome_grupo_rebate,
	                                    tipo_contrato.tipo_contrato";

                return await connection.QueryFirstOrDefaultAsync<CalculoRebateViewModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<DescricaoCalculoRebateViewModel>> GetDescricaoRebateAsync(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string Descricao = @"SELECT
		                                    contrato.id AS CodContrato,
										    tipo_contrato.id AS CodTipoContrato,
										    tipo_contrato.tipo_contrato,
                                            sub_contrato.id AS CodSubContrato,
                                            sub_contrato.versao AS VersaoContrato,
										    sub_contrato.status AS StatusContrato,
										    sub_contrato.id_docusign,
										    sub_contrato.data_vigencia_inicio,
										    sub_contrato.data_vigencia_fim,
										    sub_contrato.data_retroatividade,
		                                    sub_contrato.clausula_retroatividade,
                                            contrato_fundo.id AS CodContratoFundo,
                                            tipo_condicao.id AS CodTipoCondicao,
										    tipo_condicao.tipo_condicao,
                                            fundo.id AS CodFundo,
										    fundo.nome_reduzido AS NomeFundo,
										    contrato_remuneracao.id AS CodContratoRemuneracao,
                                            contrato_remuneracao.percentual_adm,
										    contrato_remuneracao.percentual_pfee,
		                                    distribuidor.id AS CodDistribuidor,
										    distribuidor.nome_distribuidor AS NomeDistribuidor,
										    gestor.id AS CodGestor,
										    gestor.nome_gestor AS NomeGestor
									    FROM
										    tbl_contrato contrato
										        INNER JOIN tbl_tipo_contrato tipo_contrato ON tipo_contrato.id = contrato.cod_tipo_contrato
										        INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.cod_contrato = contrato.id
										        INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.cod_sub_contrato = sub_contrato.id
										        INNER JOIN tbl_tipo_condicao tipo_condicao ON tipo_condicao.id = contrato_fundo.cod_tipo_condicao
										        INNER JOIN tbl_fundo fundo ON fundo.id = contrato_fundo.cod_fundo
										        INNER JOIN tbl_contrato_remuneracao contrato_remuneracao ON contrato_remuneracao.cod_contrato_fundo = contrato_fundo.id
										        LEFT JOIN tbl_distribuidor distribuidor ON distribuidor.id = contrato.cod_distribuidor
										        LEFT JOIN tbl_gestor gestor ON gestor.id = contrato.cod_gestor
									    WHERE
										    contrato.id = @cod_contrato
										    AND sub_contrato.id = @cod_sub_contrato
										    AND contrato_fundo.id = @cod_contrato_fundo
										    AND contrato_remuneracao.id = @cod_contrato_remuneracao";

                var Condicao = ICondicaoRemuneracaoService.QUERY_BASE + 
                                        @"
                                          WHERE
	                                          condicao_remuneracao.cod_contrato_remuneracao = @cod_contrato_remuneracao";

                List<DescricaoCalculoRebateViewModel> descricoes = await connection.QueryAsync<DescricaoCalculoRebateViewModel>(Descricao, new
                {
                    cod_contrato = codContrato,
                    cod_sub_contrato = codSubContrato,
                    cod_contrato_fundo = codContratoFundo,
                    cod_contrato_remuneracao = codContratoRemuneracao
                }) as List<DescricaoCalculoRebateViewModel>;

                foreach (var item in descricoes)
                {
                    List<CondicaoRemuneracaoViewModel> condicao = await connection.QueryAsync<CondicaoRemuneracaoViewModel>(Condicao, new { cod_contrato_remuneracao = item.CodContratoRemuneracao }) as List<CondicaoRemuneracaoViewModel>;

                    item.ListaCondicaoRemuneracao = condicao;
                }
                return descricoes;
            }
        }

        public async Task<int> GetCountCalculoRebateAsync(FiltroModel filtro)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
                                 COUNT(1)
                              FROM
                                 tbl_calculo_pgto_adm_pfee
									INNER JOIN tbl_pgto_adm_pfee ON tbl_calculo_pgto_adm_pfee.cod_pgto_adm_pfee = tbl_pgto_adm_pfee.id
                              WHERE
                                 tbl_pgto_adm_pfee.competencia = @Competencia";

                return await connection.QueryFirstOrDefaultAsync<int>(query, new { filtro.Competencia });
            }
        }
    }
}
