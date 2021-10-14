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
    public class PagamentoServicoService : GenericService<PagamentoServicoModel>, IPagamentoServicoService
    {
        public PagamentoServicoService() : base(new PagamentoServicoModel(),
            "tbl_pagamento_servico",
            new List<string> { "'id'" },
            new List<string> { "Id", "NomeFundo" },
            new List<string> { "'id'" },
            new List<string> { "Id", "NomeFundo" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(PagamentoServicoModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> AddPagamentoServico(List<PagamentoServicoModel> pagamentoServicos)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                Parallel.ForEach(pagamentoServicos, async x =>
                {
                    _ = await AddAsync(x);
                });

                return GetPagamentoServicoByCompetencia(pagamentoServicos.FirstOrDefault().Competencia).Result.ToArray().Length == pagamentoServicos.Count;
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
                const string query = "DELETE FROM tbl_pagamento_servico WHERE competencia = @competencia";
                return await connection.ExecuteAsync(query, new { competencia }) > 0;
            }
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PagamentoServicoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pagamento_servico.*,
                                        fundo.nome_reduzido as nome_fundo
                                     FROM
	                                    tbl_pagamento_servico pagamento_servico
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pagamento_servico.cod_fundo
                                     ORDER BY
                                        pagamento_servico.competencia,
                                        fundo.nome_reduzido";

                return await connection.QueryAsync<PagamentoServicoModel>(query);
            }
        }

        public async Task<PagamentoServicoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pagamento_servico.*,
                                        fundo.nome_reduzido as nome_fundo
                                     FROM
	                                    tbl_pagamento_servico pagamento_servico
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pagamento_servico.cod_fundo
                                     WHERE
                                        pagamento_servico.id = @id";

                return await connection.QueryFirstOrDefaultAsync<PagamentoServicoModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<PagamentoServicoModel>> GetByIdsAsync(string competencia, int codFundo)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pagamento_servico.*,
                                        fundo.nome_reduzido as nome_fundo
                                     FROM
	                                    tbl_pagamento_servico pagamento_servico
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pagamento_servico.cod_fundo
                                     WHERE
                                        pagamento_servico.cod_fundo = @cod_fundo
                                        AND pagamento_servico.competencia = @competencia";

                return await connection.QueryAsync<PagamentoServicoModel>(query, new { cod_fundo = codFundo, competencia });
            }
        }

        public async Task<IEnumerable<PagamentoServicoModel>> GetPagamentoServicoByCompetencia(string competencia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    pagamento_servico.*,
                                        fundo.nome_reduzido as nome_fundo
                                     FROM
	                                    tbl_pagamento_servico pagamento_servico
                                        INNER JOIN tbl_fundo fundo ON fundo.id = pagamento_servico.cod_fundo
                                     WHERE
                                        pagamento_servico.competencia = @competencia";

                return await connection.QueryAsync<PagamentoServicoModel>(query, new { competencia });
            }
        }

        public Task<bool> UpdateAsync(PagamentoServicoModel item)
        {
            throw new NotImplementedException();
        }
    }
}
