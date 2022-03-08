using Dapper;
using DUDS.Models.Contrato;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ExcecaoContratoService : GenericService<ExcecaoContratoModel>, IExcecaoContratoService
    {
        public ExcecaoContratoService() : base(new ExcecaoContratoModel(), "tbl_excecao_contrato")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddAsync(ExcecaoContratoModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await DisableAsync(id);
        }

        public async Task<bool> DisableAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> UpdateAsync(ExcecaoContratoModel item)
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

        public async Task<IEnumerable<ExcecaoContratoViewModel>> GetExcecaoContratoAsync(int? codSubContrato, int? codFundo, int? codInvestidorDistribuidor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IExcecaoContratoService.QUERY_BASE +
                    @"
                    WHERE
	                    tbl_excecao_contrato.Ativo = 1
                        AND (@CodInvestidorDistribuidor IS NULL OR tbl_excecao_contrato.CodInvestidorDistribuidor = @CodInvestidorDistribuidor)
                        AND (@CodFundo IS NULL OR tbl_excecao_contrato.CodFundo = @CodFundo)
                        AND (@CodSubContrato IS NULL OR tbl_excecao_contrato.CodSubContrato = @CodSubContrato)";
                var listaExcecaoContratoModel = await connection.QueryAsync<ExcecaoContratoViewModel>(
                    query,
                    new
                    {
                        CodInvestidorDistribuidor = codInvestidorDistribuidor,
                        CodFundo = codFundo,
                        CodSubContrato = codSubContrato
                    });

                return listaExcecaoContratoModel;
            }
        }

        public async Task<ExcecaoContratoViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IExcecaoContratoService.QUERY_BASE +
                    @"
                    WHERE
                        Id = @id";

                return await connection.QueryFirstOrDefaultAsync<ExcecaoContratoViewModel>(query, new { id });
            }
        }
    }
}
