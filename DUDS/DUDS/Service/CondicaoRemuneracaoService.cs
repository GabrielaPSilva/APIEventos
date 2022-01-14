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
        public CondicaoRemuneracaoService() : base(new CondicaoRemuneracaoModel(),"tbl_condicao_remuneracao")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(CondicaoRemuneracaoModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> UpdateAsync(CondicaoRemuneracaoModel item)
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

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DELETE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public Task<bool> DisableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CondicaoRemuneracaoViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = ICondicaoRemuneracaoService.QUERY_BASE + 
                    @"
                    WHERE
	                    contrato.ativo = 1";

                return await connection.QueryAsync<CondicaoRemuneracaoViewModel>(query);
            }
        }

        public async Task<CondicaoRemuneracaoViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = ICondicaoRemuneracaoService.QUERY_BASE + 
                    @"
                    WHERE
                        condicao_remuneracao.id = @id";

                return await connection.QueryFirstOrDefaultAsync<CondicaoRemuneracaoViewModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<CondicaoRemuneracaoViewModel>> GetCondicaoRemuneracaoByContratoRemuneracaoAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ICondicaoRemuneracaoService.QUERY_BASE + 
                    @"
                    WHERE
	                    contrato_remuneracao.id = @id
                    ORDER BY
                        condicao_remuneracao.id,
                        fundo.nome_reduzido";

                return await connection.QueryAsync<CondicaoRemuneracaoViewModel>(query, new { id });
            }
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
