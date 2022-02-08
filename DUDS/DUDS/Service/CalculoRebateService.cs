using Dapper;
using DUDS.Models.Contrato;
using DUDS.Models.Filtros;
using DUDS.Models.Rebate;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
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

        public Task<bool> AddAsync(CalculoRebateModel item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CalculoRebateModel>> AddBulkAsync(List<CalculoRebateModel> item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        SqlBulkCopy bulkCopy = new SqlBulkCopy(connection: (SqlConnection)connection,
                            copyOptions: SqlBulkCopyOptions.Default,
                            externalTransaction: (SqlTransaction)transaction);

                        var dataTable = ToDataTable(item);
                        bulkCopy = SqlBulkCopyConfigure(bulkCopy, dataTable.Rows.Count);
                        //CancellationTokenSource cancelationTokenSource = new CancellationTokenSource();
                        //CancellationToken cancellationToken = cancelationTokenSource.Token;
                        //await bulkCopy.WriteToServerAsync(dataTable, cancellationToken);
                        bulkCopy.WriteToServer(dataTable);
                        transaction.Commit();
                        return item;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        transaction.Rollback();
                        return new List<CalculoRebateModel>();
                    }
                }
            }
        }

        public Task<bool> UpdateAsync(CalculoRebateModel item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        /*
        public async Task<bool> DeleteByCompetenciaAsync(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string query = "DELETE FROM tbl_calculo_pgto_adm_pfee WHERE Competencia = @competencia";
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
        */

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CalculoRebateViewModel>> GetCalculoRebate(string competencia, int? codGrupoRebate)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICalculoRebateService.QUERY_BASE +
                                @"
								    WHERE
										pagamento.Competencia = @competencia AND
                                        (@id IS NULL OR grupo_rebate.Id = @id)
                                    ORDER BY
	                                    fundo.NomeReduzido,
	                                    grupo_rebate.NomeGrupoRebate,
	                                    tipo_contrato.TipoContrato";

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
	                                    calculo_pgto_adm_pfee.CodPgtoAdmPfee = @id
                                    ORDER BY
	                                    fundo.NomeReduzido,
	                                    grupo_rebate.NomeGrupoRebate,
	                                    tipo_contrato.TipoContrato";

                return await connection.QueryFirstOrDefaultAsync<CalculoRebateViewModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<DescricaoCalculoRebateViewModel>> GetDescricaoRebateAsync(int codContrato, int codSubContrato, int codContratoFundo, int codContratoRemuneracao)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string Descricao = @"SELECT
		                                    contrato.Id AS CodContrato,
										    tipo_contrato.Id AS CodTipoContrato,
										    tipo_contrato.TipoContrato,
                                            sub_contrato.Id AS CodSubContrato,
                                            sub_contrato.Versao AS VersaoContrato,
										    sub_contrato.Status AS StatusContrato,
										    sub_contrato.IdDocusign,
										    sub_contrato.DataVigenciaInicio,
										    sub_contrato.DataVigenciaFim,
										    sub_contrato.DataRetroatividade,
		                                    sub_contrato.ClausulaRetroatividade,
                                            contrato_fundo.Id AS CodContratoFundo,
                                            tipo_condicao.Id AS CodTipoCondicao,
										    tipo_condicao.TipoCondicao,
                                            fundo.Id AS CodFundo,
										    fundo.NomeReduzido AS NomeFundo,
										    contrato_remuneracao.Id AS CodContratoRemuneracao,
                                            contrato_remuneracao.PercentualAdm,
										    contrato_remuneracao.PercentualPfee,
		                                    distribuidor.Id AS CodDistribuidor,
										    distribuidor.NomeDistribuidor AS NomeDistribuidor,
										    gestor.Id AS CodGestor,
										    gestor.NomeGestor AS NomeGestor
									    FROM
										    tbl_contrato contrato
										        INNER JOIN tbl_tipo_contrato tipo_contrato ON tipo_contrato.Id = contrato.CodTipoContrato
										        INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.CodContrato = contrato.Id
										        INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.CodSubContrato = sub_contrato.Id
										        INNER JOIN tbl_tipo_condicao tipo_condicao ON tipo_condicao.Id = contrato_fundo.CodTipoCondicao
										        INNER JOIN tbl_fundo fundo ON fundo.Id = contrato_fundo.CodFundo
										        INNER JOIN tbl_contrato_remuneracao contrato_remuneracao ON contrato_remuneracao.CodContratoFundo = contrato_fundo.Id
										        LEFT JOIN tbl_distribuidor distribuidor ON distribuidor.Id = contrato.CodDistribuidor
										        LEFT JOIN tbl_gestor gestor ON gestor.Id = contrato.CodGestor
									    WHERE
										    contrato.Id = @CodContrato
										    AND sub_contrato.Id = @CodSubContrato
										    AND contrato_fundo.Id = @CodContratoFundo
										    AND contrato_remuneracao.Id = @CodContratoRemuneracao";

                var Condicao = @"SELECT 
                                     condicao_remuneracao.*,
                                     tbl_fundo.NomeReduzido AS NomeFundo
                                 FROM 
                                     tbl_condicao_remuneracao condicao_remuneracao
                                        INNER JOIN tbl_fundo ON condicao_remuneracao.CodFundo = tbl_fundo.Id
                                 WHERE
	                                 condicao_remuneracao.CodContratoRemuneracao = @CodContratoRemuneracao";

                List<DescricaoCalculoRebateViewModel> descricoes = await connection.QueryAsync<DescricaoCalculoRebateViewModel>(Descricao, new
                {
                    CodContrato = codContrato,
                    CodSubContrato = codSubContrato,
                    CodContratoFundo = codContratoFundo,
                    CodContratoRemuneracao = codContratoRemuneracao
                }) as List<DescricaoCalculoRebateViewModel>;

                foreach (var item in descricoes)
                {
                    List<CondicaoRemuneracaoViewModel> condicao = await connection.QueryAsync<CondicaoRemuneracaoViewModel>(Condicao, new { CodContratoRemuneracao = item.CodContratoRemuneracao }) as List<CondicaoRemuneracaoViewModel>;

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
									INNER JOIN tbl_pgto_adm_pfee ON tbl_calculo_pgto_adm_pfee.CodPgtoAdmPfee = tbl_pgto_adm_pfee.Id
                              WHERE
                                 tbl_pgto_adm_pfee.Competencia = @Competencia";

                return await connection.QueryFirstOrDefaultAsync<int>(query, new { filtro.Competencia });
            }
        }

        public async Task<IEnumerable<ControleRebateModel>> GetParametroControleRebateAsync(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            distinct tbl_investidor_distribuidor.CodGrupoRebate, 
                                tbl_pgto_adm_pfee.Competencia,
                                tbl_calculo_pgto_adm_pfee.UsuarioCriacao
                            FROM 
	                            tbl_calculo_pgto_adm_pfee
	                            INNER JOIN tbl_pgto_adm_pfee ON tbl_pgto_adm_pfee.Id = tbl_calculo_pgto_adm_pfee.CodPgtoAdmPfee
	                            INNER JOIN tbl_investidor_distribuidor ON tbl_investidor_distribuidor.Id = tbl_pgto_adm_pfee.CodInvestidorDistribuidor
                            WHERE
	                            tbl_pgto_adm_pfee.Competencia = @competencia";

                return await connection.QueryAsync<ControleRebateModel>(query, new { competencia });
            }
        }
    }
}
