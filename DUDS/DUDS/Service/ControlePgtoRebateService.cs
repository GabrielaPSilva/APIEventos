﻿using Dapper;
using DUDS.Models.Rebate;
using DUDS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ControlePgtoRebateService : GenericService<PgtoRebateModel>, IControlePgtoRebateService
    {
        public ControlePgtoRebateService() : base(new PgtoRebateModel(),
                                        "tbl_controle_pgto_rebate")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IEnumerable<PgtoRebateViewModel>> GetPgtoRebateByCompetencia(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IControlePgtoRebateService.QUERY_ARQUIVO_PGTO +
                             @"
                                 WHERE
	                                tbl_controle_pgto_rebate.Competencia = @Competencia";

                return await connection.QueryAsync<PgtoRebateViewModel>(query, new { Competencia = competencia });
            }
        }

        public async Task<IEnumerable<PgtoRebateModel>> GetValidaErrosPagamento(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                        SELECT
                                        	*
                                        FROM 
                                        	tbl_controle_pgto_rebate
                                        		INNER JOIN tbl_erros_pgto ON ISNULL(tbl_controle_pgto_rebate.CodDistribuidor, 0) = ISNULL(tbl_erros_pgto.CodDistribuidor, 0) AND
                                        									 ISNULL(tbl_controle_pgto_rebate.CodGestor, 0) = ISNULL(tbl_erros_pgto.CodGestor, 0) AND
                                        									 ISNULL(tbl_controle_pgto_rebate.CodInvestidor, 0) = ISNULL(tbl_erros_pgto.CodInvestidor, 0) AND
                                        									 tbl_controle_pgto_rebate.CodFundo = tbl_erros_pgto.CodFundo AND
                                        									 tbl_controle_pgto_rebate.ValorBruto = tbl_erros_pgto.Valor AND 
                                        									 tbl_controle_pgto_rebate.Competencia = tbl_erros_pgto.Competencia AND 
                                        									 tbl_controle_pgto_rebate.CodTipoContrato = tbl_erros_pgto.CodDespesaAdministrador
                                       WHERE 
                                           tbl_controle_pgto_rebate.Competencia = @Competencia";

                return await connection.QueryAsync<PgtoRebateModel>(query, new { Competencia = competencia });
            }
        }

        public async Task<IEnumerable<PgtoRebateModel>> GetPgtoRebateById(Guid id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                return await connection.QueryAsync<PgtoRebateModel>(IControlePgtoRebateService.QUERY_BASE, new { Id = id });
            }
        }

        public async Task<bool> AddAsync(PgtoRebateModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int retornoUnderBucket = await connection.ExecuteAsync(sql: IControlePgtoRebateService.QUERY_INSERT_ADM_INVESTIDOR.Replace("[Condicao]", "<="),
                            param: new
                            {
                                Competencia = item.Competencia,
                                UsuarioCriacao = item.UsuarioCriacao,
                                DataAgendamento = item.DataAgendamento
                            }
                            );
                        if (retornoUnderBucket != 0)
                        {
                            return false;
                        }
                        int retornoOverBucket = await connection.ExecuteAsync(sql: IControlePgtoRebateService.QUERY_INSERT_ADM_INVESTIDOR.Replace("[Condicao]", ">"),
                            param: new
                            {
                                Competencia = item.Competencia,
                                UsuarioCriacao = item.UsuarioCriacao,
                                DataAgendamento = item.DataAgendamento
                            }
                            );
                        transaction.Commit();
                        return (retornoUnderBucket + retornoOverBucket) > 0;
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

        public Task<bool> UpdateAsync(PgtoRebateModel item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByCompetenciaAsync(string competencia)
        {
            List<PgtoRebateViewModel> result = await GetPgtoRebateByCompetencia(competencia) as List<PgtoRebateViewModel>;
            if (result == null) return false;
            if (result.Count == 0) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string query = "DELETE FROM tbl_controle_pgto_rebate WHERE Competencia = @Competencia";
                        int rowsAffected = await connection.ExecuteAsync(sql: query, param: new { Competencia = competencia }, transaction: transaction, commandTimeout: 180);
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

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
