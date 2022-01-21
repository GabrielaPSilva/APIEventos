using Dapper;
using DUDS.Models.Contrato;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ContratoAlocadorService : GenericService<ContratoAlocadorModel>, IContratoAlocadorService
    {
        public ContratoAlocadorService() : base(new ContratoAlocadorModel(),"tbl_contrato_alocador")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(ContratoAlocadorModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
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

        public async Task<IEnumerable<ContratoAlocadorViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoAlocadorService.QUERY_BASE +
                    @"
                    WHERE
                        contrato.Ativo = 1
                    ORDER BY
                        investidor.NomeInvestidor";

                return await connection.QueryAsync<ContratoAlocadorViewModel>(query);
            }
        }

        public async Task<ContratoAlocadorViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoAlocadorService.QUERY_BASE +
                    @"
                    WHERE
                        contrato_alocador.Id = @id";
                return await connection.QueryFirstOrDefaultAsync<ContratoAlocadorViewModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<ContratoAlocadorViewModel>> GetContratoAlocadorByCodSubContratoAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IContratoAlocadorService.QUERY_BASE + 
                    @"
                    WHERE
                        sub_contrato.Id = @id
                    ORDER BY
                        investidor.NomeInvestidor";

                return await connection.QueryAsync<ContratoAlocadorViewModel>(query,new { id });
            }
        }

        public async Task<bool> UpdateAsync(ContratoAlocadorModel item)
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
    }
}
