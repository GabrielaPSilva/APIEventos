using Dapper;
using DUDS.Models.Rebate;
using DUDS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class PgtoRebateService : GenericService<PgtoRebateModel>, IPgtoRebateService
    {
        public PgtoRebateService(): base(new PgtoRebateModel(),
                                        "tbl_pgto_rebate")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(PgtoRebateModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var retorno = await connection.ExecuteAsync(sql: IPgtoRebateService.QUERY_INSERT, param: item);
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

        public Task<bool> DeleteAsync(int id)
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
                        const string query = "DELETE FROM tbl_pgto_rebate WHERE Competencia = @Competencia";
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

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PgtoRebateViewModel>> GetPgtoRebateByCompetencia(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                return await connection.QueryAsync<PgtoRebateViewModel>(IPgtoRebateService.QUERY_BASE, new { Competencia = competencia });
            }
        }

        public Task<bool> UpdateAsync(PgtoRebateModel item)
        {
            throw new NotImplementedException();
        }
    }
}
