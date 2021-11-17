using Dapper;
using DUDS.Models;
using DUDS.Models.Filtros;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class CalculoRebateService : GenericService<CalculoRebateModel>, ICalculoRebateService
    {
        public CalculoRebateService() : base(new CalculoRebateModel(),
            "tbl_calculo_pgto_adm_pfee",
            new List<string> { "'id'", "'data_criacao'", "'ativo'" },
            new List<string> { "Id", "DataCriacao", "Ativo", "NomeInvestidor", "CodGrupoRebate", "NomeGrupoRebate", "CodTipoContrato", "NomeTipoContrato", "NomeFundo" },
            new List<string> { "'id'", "'data_criacao'", "'ativo'", "'usuario_criacao'" },
            new List<string> { "Id", "DataCriacao", "Ativo", "NomeInvestidor", "CodGrupoRebate", "NomeGrupoRebate", "CodTipoContrato", "NomeTipoContrato", "NomeFundo", "UsuarioCriacao" })
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

        public async Task<bool> AddAsync(CalculoRebateModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> AddBulkAsync(List<CalculoRebateModel> calculoPgtoTaxaAdmPfee)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                Parallel.ForEach(calculoPgtoTaxaAdmPfee, async x =>
                {
                    _ = await AddAsync(x);
                });

                return await GetCountCalculoRebateAsync(new FiltroModel { Competencia = calculoPgtoTaxaAdmPfee.FirstOrDefault().Competencia }) == calculoPgtoTaxaAdmPfee.Count;
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

        public async Task<bool> DeleteByCompetenciaAsync(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = "DELETE FROM tbl_calculo_pgto_adm_pfee WHERE competencia = @competencia";
                return await connection.ExecuteAsync(query, new { competencia }) > 0;
            }
        }

        public async Task<bool> DisableAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public Task<IEnumerable<CalculoRebateModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CalculoRebateModel>> GetByCompetenciaAsync(string competencia, int codGrupoRebate)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT
										 calculo_pgto_adm_pfee.id,
	                                     calculo_pgto_adm_pfee.competencia,
	                                     calculo_pgto_adm_pfee.cod_investidor_distribuidor,
	                                     investidor.nome_investidor,
	                                     investidor_distribuidor.cod_grupo_rebate,
	                                     grupo_rebate.nome_grupo_rebate,
	                                     investidor_distribuidor.cod_tipo_contrato,
	                                     tipo_contrato.tipo_contrato AS nome_tipo_contrato,
	                                     calculo_pgto_adm_pfee.cod_fundo,
	                                     fundo.nome_reduzido AS nome_fundo,
	                                     calculo_pgto_adm_pfee.cod_contrato,
	                                     calculo_pgto_adm_pfee.cod_sub_contrato,
	                                     calculo_pgto_adm_pfee.cod_contrato_fundo,
	                                     calculo_pgto_adm_pfee.cod_contrato_remuneracao,
	                                     calculo_pgto_adm_pfee.cod_condicao_remuneracao,
	                                     calculo_pgto_adm_pfee.cod_administrador,
	                                     calculo_pgto_adm_pfee.valor_adm,
	                                     calculo_pgto_adm_pfee.valor_pfee_resgate,
	                                     calculo_pgto_adm_pfee.valor_pfee_semestre,
                                         calculo_pgto_adm_pfee.perc_adm,
                                         calculo_pgto_adm_pfee.perc_pfee,
	                                     calculo_pgto_adm_pfee.rebate_adm,
	                                     calculo_pgto_adm_pfee.rebate_pfee_resgate,
	                                     calculo_pgto_adm_pfee.rebate_pfee_semestre
                                    FROM
	                                    tbl_calculo_pgto_adm_pfee calculo_pgto_adm_pfee
	                                    INNER JOIN tbl_investidor investidor ON investidor.id = calculo_pgto_adm_pfee.cod_investidor
										INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor.id = investidor_distribuidor.cod_investidor
	                                    INNER JOIN tbl_grupo_rebate grupo_rebate ON grupo_rebate.id = investidor_distribuidor.cod_grupo_rebate
	                                    INNER JOIN tbl_fundo fundo ON fundo.id = calculo_pgto_adm_pfee.cod_fundo
	                                    INNER JOIN tbl_tipo_contrato tipo_contrato ON tipo_contrato.id = investidor_distribuidor.cod_tipo_contrato
								    WHERE
										calculo_pgto_adm_pfee.competencia = @competencia AND
                                        grupo_rebate.id = @id
                                    ORDER BY
	                                    fundo.nome_reduzido,
	                                    grupo_rebate.nome_grupo_rebate,
	                                    tipo_contrato.tipo_contrato
								    OFFSET
									    @itensPorPagina * (@pagina - 1)
									ROWS FETCH NEXT
									    @itensPorPagina
								    ROWS ONLY";

                return await connection.QueryAsync<CalculoRebateModel>(query, new { competencia, id = codGrupoRebate });
            }
        }

        public async Task<CalculoRebateModel> GetByIdAsync(Guid id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT
										calculo_pgto_adm_pfee.id,
	                                    calculo_pgto_adm_pfee.competencia,
	                                    calculo_pgto_adm_pfee.cod_investidor_distribuidor,
	                                    investidor.nome_investidor,
	                                    investidor_distribuidor.cod_grupo_rebate,
	                                    grupo_rebate.nome_grupo_rebate,
	                                    investidor_distribuidor.cod_tipo_contrato,
	                                    tipo_contrato.tipo_contrato AS nome_tipo_contrato,
	                                    calculo_pgto_adm_pfee.cod_fundo,
	                                    fundo.nome_reduzido AS nome_fundo,
	                                    calculo_pgto_adm_pfee.cod_contrato,
	                                    calculo_pgto_adm_pfee.cod_sub_contrato,
	                                    calculo_pgto_adm_pfee.cod_contrato_fundo,
	                                    calculo_pgto_adm_pfee.cod_contrato_remuneracao,
	                                    calculo_pgto_adm_pfee.cod_condicao_remuneracao,
	                                    calculo_pgto_adm_pfee.cod_administrador,
	                                    calculo_pgto_adm_pfee.valor_adm,
	                                    calculo_pgto_adm_pfee.valor_pfee_resgate,
	                                    calculo_pgto_adm_pfee.valor_pfee_semestre,
                                        calculo_pgto_adm_pfee.perc_adm,
                                        calculo_pgto_adm_pfee.perc_pfee,
	                                    calculo_pgto_adm_pfee.rebate_adm,
	                                    calculo_pgto_adm_pfee.rebate_pfee_resgate,
	                                    calculo_pgto_adm_pfee.rebate_pfee_semestre
                                    FROM
	                                    tbl_calculo_pgto_adm_pfee calculo_pgto_adm_pfee
	                                    INNER JOIN tbl_investidor investidor ON investidor.id = calculo_pgto_adm_pfee.cod_investidor
										INNER JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor.id = investidor_distribuidor.cod_investidor
	                                    INNER JOIN tbl_grupo_rebate grupo_rebate ON grupo_rebate.id = investidor_distribuidor.cod_grupo_rebate
	                                    INNER JOIN tbl_fundo fundo ON fundo.id = calculo_pgto_adm_pfee.cod_fundo
	                                    INNER JOIN tbl_tipo_contrato tipo_contrato ON tipo_contrato.id = investidor_distribuidor.cod_tipo_contrato
                                    WHERE
	                                    calculo_pgto_adm_pfee.id = @id
                                    ORDER BY
	                                    fundo.nome_reduzido,
	                                    grupo_rebate.nome_grupo_rebate,
	                                    tipo_contrato.tipo_contrato";

                return await connection.QueryFirstOrDefaultAsync<CalculoRebateModel>(query, new { id });
            }
        }

        public Task<CalculoRebateModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DescricaoCalculoRebateModel>> GetDescricaoRebateAsync(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao, string codCondicaoRemuneracao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT
										tipo_contrato.id AS cod_tipo_contrato,
										tipo_contrato.tipo_contrato,
										gestor.id AS cod_gestor,
										gestor.nome_gestor,
										distribuidor.id AS cod_distribuidor,
										distribuidor.nome_distribuidor,
										sub_contrato.versao AS versao_contrato,
										sub_contrato.status AS status_contrato,
										sub_contrato.id_docusign,
										sub_contrato.data_vigencia_inicio,
										sub_contrato.data_vigencia_fim,
										sub_contrato.data_retroatividade,
										fundo.id AS cod_fundo,
										fundo.nome_reduzido AS nome_fundo,
										tipo_condicao.id AS cod_tipo_condicao,
										tipo_condicao.tipo_condicao,
										contrato_remuneracao.percentual_adm,
										contrato_remuneracao.percentual_pfee,
										contrato.id AS cod_contrato,
										sub_contrato.id AS cod_sub_contrato,
										contrato_fundo.id AS cod_contrato_fundo,
										contrato_remuneracao.id AS cod_contrato_remuneracao
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

                return await connection.QueryAsync<DescricaoCalculoRebateModel>(query, new
                {
                    cod_contrato = codContrato,
                    cod_sub_contrato = codSubContrato,
                    cod_contrato_fundo = codContratoFundo,
                    cod_contrato_remuneracao = codContratoRemuneracao,
                    cod_condicao_remuneracao = codCondicaoRemuneracao
                });
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
                              WHERE
                                 tbl_calculo_pgto_adm_pfee.competencia = @Competencia";
               
                return await connection.QueryFirstOrDefaultAsync<int>(query, new { filtro.Competencia });
            }
        }

        public Task<bool> UpdateAsync(CalculoRebateModel item)
        {
            throw new NotImplementedException();
        }
    }
}
