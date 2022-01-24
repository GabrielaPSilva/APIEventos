﻿using Dapper;
using DUDS.Models.Passivo;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace DUDS.Service
{
    public class PosicaoClienteService : GenericService<PosicaoClienteModel>, IPosicaoClienteService
    {
        public PosicaoClienteService() : base(new PosicaoClienteModel(), "tbl_posicao_cliente") { }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(PosicaoClienteModel item)
        {
            if (item == null) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                        var retorno = await connection.ExecuteAsync(sql: query, param: item, transaction: transaction, commandTimeout: 180);
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

        public async Task<IEnumerable<PosicaoClienteModel>> AddBulkAsync(List<PosicaoClienteModel> item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlBulkCopy bulkCopy = new SqlBulkCopy(connection: (SqlConnection)connection,
                            copyOptions: SqlBulkCopyOptions.Default,
                            externalTransaction: (SqlTransaction)transaction);

                        var dataTable = ToDataTable(item);
                        bulkCopy = SqlBulkCopyConfigure(bulkCopy, dataTable.Rows.Count);
                        bulkCopy.WriteToServer(dataTable);
                        transaction.Commit();
                        return item;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return new List<PosicaoClienteModel>();
                    }
                }
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByDataRefAsync(DateTime dataRef)
        {
            List<PosicaoClienteModel> result = await GetByParametersAsync(dataInicio: dataRef, dataFim: null, codDistribuidor: null, codGestor: null, codInvestidorDistribuidor: null) as List<PosicaoClienteModel>;
            if (result == null) return false;
            if (result.Count == 0) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string query = "DELETE FROM tbl_posicao_cliente WHERE data_ref = @data_ref";
                        int rowsAffected = await connection.ExecuteAsync(sql: query, param: new { data_ref = dataRef }, transaction: transaction, commandTimeout: 180);
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

        public async Task<IEnumerable<PosicaoClienteViewModel>> GetByParametersAsync(DateTime? dataInicio, DateTime? dataFim, int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor)
        {
            if (dataInicio == null && dataFim == null) return new List<PosicaoClienteViewModel>();
            if (dataInicio != null && dataFim == null) dataFim = dataInicio;
            if (dataInicio == null && dataFim != null) dataInicio = dataFim;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IPosicaoClienteService.QUERY_BASE +
                    @"
                    WHERE
                        (@DataInicio IS NULL OR tbl_posicao_cliente.DataRef >= @DataInicio) 
                        AND (@DataFim IS NULL OR tbl_posicao_cliente.DataFef <= @DataFim)
                        AND (@CodDistribuidor IS NULL OR tbl_investidor_distribuidor.CodDistribuidorAdministrador = @CodDistribuidor)
                        AND (@CodGestor IS NULL OR tbl_investidor.CodGestor = @CodGestor)
                        AND (@CodInvestidorDistribuidor IS NULL OR tbl_investidor_distribuidor.Id = @CodInvestidorDistribuidor)
                    ORDER BY
	                    tbl_fundo.TipoCota,
	                    tbl_tipo_estrategia.Estrategia,
	                    tbl_fundo.DataCotaInicial,
	                    tbl_investidor.TipoInvestidor,
	                    adm_investidor.NomeAdministrador,
	                    tbl_gestor.NomeGestor,
	                    tbl_distribuidor.NomeDistribuidor";

                return await connection.QueryAsync<PosicaoClienteViewModel>(query, new
                {
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    CodDistribuidor = codDistribuidor,
                    CodGestor = codGestor,
                    CodInvestidorDistribuidor = codInvestidorDistribuidor
                });
            }
        }


        public async Task<int> GetCountByDataRefAsync(DateTime dataRef)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query =
                    @"
                    SELECT 
                        COUNT(1)
                    FROM
                        tbl_posicao_cliente
                    WHERE
                        tbl_posicao_cliente.DataRef = @DataRef";

                return await connection.QueryFirstOrDefaultAsync<int>(query, new { DataRef = dataRef });
            }
        }

        public async Task<bool> UpdateAsync(PosicaoClienteModel item)
        {
            if (item == null) return false;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = GenericSQLCommands.UPDATE_COMMAND.Replace("TABELA", TableName);
                        List<string> str = new List<string>();
                        for (int i = 0; i < _propertiesUpdate.Count; i++)
                        {
                            str.Add(_fieldsUpdate[i] + " = " + _propertiesUpdate[i]);
                        }
                        query = query.Replace("VALORES", String.Join(",", str));
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

        public async Task<double> GetMaxValorBrutoAsync(DateTime dataPosicao, int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor)
        {
            if (!codDistribuidor.HasValue && !codGestor.HasValue && !codInvestidorDistribuidor.HasValue) return 0;

            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = "SELECT ISNULL(MAX(ValorBruto),0) AS MaiorValorPosicao FROM (" + IPosicaoClienteService.QUERY_BASE +
                    @"
                    WHERE
                        (@CodDistribuidor IS NULL or tbl_distribuidor.Id = @CodDistribuidor)
                        AND (@CodGestor IS NULL or tbl_investidor.CodGestor = @CodGestor)
                        AND (@CodInvestidorDistribuidor IS NULL or tbl_investidor_distribuidor.Id = @CodInvestidorDistribuidor)
                        AND tbl_posicao_cliente.DataRef <= @DataRef) posicao";

                return await connection.QueryFirstOrDefaultAsync<double>(query, new
                {
                    DataRef = dataPosicao,
                    CodDistribuidor = codDistribuidor,
                    CodGestor = codGestor,
                    CodInvestidorDistribuidor = codInvestidorDistribuidor
                });
            }
        }
    }
}
