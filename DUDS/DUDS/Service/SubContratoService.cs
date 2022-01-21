using Dapper;
using DUDS.Models.Contrato;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class SubContratoService : GenericService<SubContratoModel>, ISubContratoService
    {
        public SubContratoService() : base(new SubContratoModel(),
            "tbl_sub_contrato")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(SubContratoModel item)
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

        public async Task<IEnumerable<SubContratoViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = ISubContratoService.QUERY_BASE +
                    @"
                    WHERE
                        contrato.ativo = 1 AND
                        sub_contrato.status = 'Ativo'
                    ORDER BY
                        contrato.id";

                return await connection.QueryAsync<SubContratoViewModel>(query);
            }
        }

        public async Task<SubContratoViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = ISubContratoService.QUERY_BASE +
                    @"
                    WHERE
                        id = @id";

                return await connection.QueryFirstOrDefaultAsync<SubContratoViewModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<SubContratoViewModel>> GetSubContratoCompletoByIdAsync(int codContrato)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = ISubContratoService.QUERY_BASE +
                    @"
                    WHERE
                        contrato.id = @cod_contrato
                    ORDER BY
                        contrato.id";

                // TODO - VOLTAR AQUI SEM FALTA
                List<SubContratoViewModel> subContratoModels = await connection.QueryAsync<SubContratoViewModel>(query, new { cod_contrato = codContrato }) as List<SubContratoViewModel>;

                /*
                ContratoAlocadorService contratoAlocadorService = new ContratoAlocadorService();
                ContratoFundoService contratoFundoService = new ContratoFundoService();

                Parallel.ForEach(subContratoModels, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, async item =>
                {
                    List<ContratoFundoModel> contratoFundoModels = await contratoFundoService.GetContratoAlocadorByCodSubContratoAsync(item.Id) as List<ContratoFundoModel>;
                    item.ListaContratoFundo = contratoFundoModels;

                    List<ContratoAlocadorModel> contratoAlocadorModels = await contratoAlocadorService.GetContratoAlocadorByCodSubContratoAsync(item.Id) as List<ContratoAlocadorModel>;
                    item.ListaContratoAlocador = contratoAlocadorModels;
                });
                */
                return subContratoModels;
            }
        }

        public async Task<bool> UpdateAsync(SubContratoModel item)
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
