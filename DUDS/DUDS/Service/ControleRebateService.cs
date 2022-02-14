using Dapper;
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
    public class ControleRebateService : GenericService<ControleRebateModel>, IControleRebateService
    {
        public ControleRebateService() : base(new ControleRebateModel(),
                                              "tbl_controle_rebate")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(ControleRebateModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                        
                        var retorno = await connection.ExecuteAsync(sql: query, param: item, transaction: transaction);
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

        public async Task<IEnumerable<ControleRebateModel>> AddBulkAsync(List<ControleRebateModel> item)
        {
            ConcurrentBag<ControleRebateModel> vs = new ConcurrentBag<ControleRebateModel>();
            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess };
            await Parallel.ForEachAsync(item, parallelOptions, async (x, cancellationToken) =>
            {
                var result = await AddAsync(x);
                if (!result) { vs.Add(x); }
            }
            );
            return vs;
        }

        public async Task<bool> UpdateAsync(ControleRebateModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.UPDATE_COMMAND.Replace("TABELA", TableName);
                List<string> str = new List<string>();
                for (int i = 0; i < _propertiesUpdate.Count; i++)
                {
                    str.Add(_fieldsUpdate[i] + " = " + _propertiesUpdate[i]);
                }
                query = query.Replace("VALORES", String.Join(",", str));
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
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<ControleRebateViewModel>> GetFiltroControleRebateAsync(int grupoRebate, string investidor, string competencia, string codMellon)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                tbl_controle_rebate.*,
                                tbl_calculo_pgto_adm_pfee.*,
								tbl_pgto_adm_pfee.Competencia,
								tbl_pgto_adm_pfee.TaxaAdministracao AS ValorAdm,
								tbl_pgto_adm_pfee.TaxaPerformanceApropriada AS ValorPfeeSemestre,
								tbl_pgto_adm_pfee.TaxaPerformanceResgate AS ValorPfeeResgate,
								tbl_contrato_remuneracao.PercentualAdm AS PercAdm,
								tbl_contrato_remuneracao.PercentualPfee AS PercPfee,
                                tbl_grupo_rebate.NomeGrupoRebate AS NomeGrupoRebate,
                                tbl_investidor.NomeInvestidor AS NomeInvestidor,
                                tbl_fundo.NomeReduzido AS NomeFundo,
	                            tbl_tipo_contrato.TipoContrato AS NomeTipoContrato,
                                tbl_investidor_distribuidor.CodInvestAdministrador AS CodMellon
                            FROM
                                tbl_controle_rebate
		                            INNER JOIN tbl_investidor_distribuidor ON tbl_controle_rebate.CodGrupoRebate = tbl_investidor_distribuidor.CodGrupoRebate
									INNER JOIN tbl_pgto_adm_pfee ON tbl_pgto_adm_pfee.CodInvestidorDistribuidor = tbl_investidor_distribuidor.Id
		                            INNER JOIN tbl_calculo_pgto_adm_pfee ON tbl_calculo_pgto_adm_pfee.CodPgtoAdmPfee = tbl_pgto_adm_pfee.Id
									INNER JOIN tbl_contrato_remuneracao ON tbl_calculo_pgto_adm_pfee.CodContratoRemuneracao = tbl_contrato_remuneracao.Id
		                            INNER JOIN tbl_fundo ON tbl_pgto_adm_pfee.CodFundo = tbl_fundo.Id
		                            INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.CodInvestidor = tbl_investidor.Id
		                            INNER JOIN tbl_tipo_contrato ON tbl_investidor_distribuidor.CodTipoContrato = tbl_tipo_contrato.Id
		                            INNER JOIN tbl_grupo_rebate ON tbl_controle_rebate.CodGrupoRebate = tbl_grupo_rebate.Id
                            WHERE
	                            tbl_controle_rebate.CodGrupoRebate = @id
	                            AND (@investidor IS NULL OR tbl_investidor.NomeInvestidor COLLATE Latin1_general_CI_AI LIKE '%' + @investidor + '%')
                                AND (@codMellon IS NULL OR tbl_investidor_distribuidor.CodInvestAdministrador = @codMellon)
	                            AND tbl_pgto_adm_pfee.Competencia = @competencia";

                var a = await connection.QueryAsync<ControleRebateViewModel, CalculoRebateViewModel, ControleRebateViewModel>(query,
                   (controle, calculo) =>
                   {
                       controle.Calculo = calculo;

                       return controle;
                   }, new
                   {
                       id = grupoRebate,
                       competencia,
                       investidor,
                       codMellon
                   }, splitOn: "Id");

                return a;
            }
        }

        public async Task<IEnumerable<ControleRebateViewModel>> GetByCompetenciaAsync(FiltroModel filtro)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IControleRebateService.QUERY_BASE +
                          @"
                             WHERE
                                (@Competencia IS NULL OR tbl_controle_rebate.Competencia = @Competencia) AND
								(@NomeGrupoRebate IS NULL OR tbl_grupo_rebate.NomeGrupoRebate COLLATE Latin1_general_CI_AI LIKE '%' + @NomeGrupoRebate + '%')
                             ORDER BY
                                tbl_controle_rebate.Enviado,
								tbl_controle_rebate.Validado,
								tbl_grupo_rebate.NomeGrupoRebate";

                return await connection.QueryAsync<ControleRebateViewModel>(query, new
                {
                    filtro.Competencia,
                    filtro.NomeGrupoRebate
                });
            }
        }

        public async Task<ControleRebateViewModel> GetGrupoRebateExistsBase(int codGrupoRebate, string Competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IControleRebateService.QUERY_BASE +
                            @"
                               WHERE 
	                               tbl_controle_rebate.CodGrupoRebate = @CodGrupoRebate AND
                                   tbl_controle_rebate.Competencia = @Competencia";

                return await connection.QueryFirstOrDefaultAsync<ControleRebateViewModel>(query, new { CodGrupoRebate = codGrupoRebate, Competencia });
            }
        }

        public async Task<ControleRebateViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IControleRebateService.QUERY_BASE + 
                                    @"
                                      WHERE
                                         tbl_controle_rebate.Id = @id";

                return await connection.QueryFirstOrDefaultAsync<ControleRebateViewModel>(query, new { id });
            }
        }

        public async Task<int> GetCountControleRebateAsync(FiltroModel filtro)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
                                 COUNT(1)
                              FROM
                                 tbl_controle_rebate
	                               INNER JOIN tbl_grupo_rebate ON tbl_controle_rebate.CodGrupoRebate = tbl_grupo_rebate.Id
                             WHERE
                                 (@Competencia IS NULL OR tbl_controle_rebate.Competencia = @Competencia)";
                
                return await connection.QueryFirstOrDefaultAsync<int>(query, new { filtro.Competencia });
            }
        }
    }
}
