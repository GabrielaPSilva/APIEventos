using Dapper;
using DUDS.Models.Contrato;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class CondicaoRemuneracaoService : GenericService<CondicaoRemuneracaoModel>, ICondicaoRemuneracaoService
    {
        public CondicaoRemuneracaoService() : base(new CondicaoRemuneracaoModel(),
                                              "tbl_condicao_remuneracao")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(CondicaoRemuneracaoModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> UpdateAsync(CondicaoRemuneracaoModel item)
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

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DELETE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CondicaoRemuneracaoViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICondicaoRemuneracaoService.QUERY_BASE +
                            @"
                              WHERE
	                             tbl_contrato.Ativo = 1
                                 AND tbl_sub_contrato.Status <> 'Inativo'";

                return await connection.QueryAsync<CondicaoRemuneracaoViewModel>(query);
            }
        }

        public async Task<CondicaoRemuneracaoViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICondicaoRemuneracaoService.QUERY_BASE +
                                    @"
                                       WHERE
                                           tbl_condicao_remuneracao.Id = @id";

                return await connection.QueryFirstOrDefaultAsync<CondicaoRemuneracaoViewModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<CondicaoRemuneracaoViewModel>> GetContratoRemuneracaoByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICondicaoRemuneracaoService.QUERY_BASE +
                            @"
                              WHERE
	                              tbl_contrato_remuneracao.Id = @id
                              ORDER BY
                                  tbl_condicao_remuneracao.Id,
                                  tbl_fundo.NomeReduzido";

                return await connection.QueryAsync<CondicaoRemuneracaoViewModel>(query, new { id });
            }
        }
    }
}
