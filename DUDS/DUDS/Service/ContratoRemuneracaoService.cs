using Dapper;
using DUDS.Models.Contrato;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ContratoRemuneracaoService : GenericService<ContratoRemuneracaoModel>, IContratoRemuneracaoService
    {
        public ContratoRemuneracaoService() : base(new ContratoRemuneracaoModel(),
            "tbl_contrato_remuneracao")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(ContratoRemuneracaoModel item)
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

        public async Task<IEnumerable<ContratoRemuneracaoViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoRemuneracaoService.QUERIY_BASE + 
                    @"
                    WHERE
                        contrato.ativo = 1
                    ORDER BY
                        contrato_remuneracao.id";

                return await connection.QueryAsync<ContratoRemuneracaoViewModel>(query);
            }
        }

        public async Task<ContratoRemuneracaoViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoRemuneracaoService.QUERIY_BASE + 
                    @"
                    WHERE
                        contrato_remuneracao.id = @id
                    ORDER BY
                        contrato_remuneracao.id";
                return await connection.QueryFirstOrDefaultAsync<ContratoRemuneracaoViewModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<ContratoRemuneracaoViewModel>> GetContratoRemuneracaoByContratoFundoAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoRemuneracaoService.QUERIY_BASE + 
                    @"
                    WHERE
                        contrato_fundo.id = @id
                    ORDER BY
                        contrato_remuneracao.id";

                List<ContratoRemuneracaoViewModel> contratoRemuneracaoModels = await connection.QueryAsync<ContratoRemuneracaoViewModel>(query, new { id }) as List<ContratoRemuneracaoViewModel>;
                /*
                CondicaoRemuneracaoService condicaoRemuneracaoService = new CondicaoRemuneracaoService();
                Parallel.ForEach(contratoRemuneracaoModels, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, async item =>
                {
                    List<CondicaoRemuneracaoModel> condicaoRemuneracaoModels = await condicaoRemuneracaoService.GetCondicaoRemuneracaoByContratoRemuneracaoAsync(item.Id) as List<CondicaoRemuneracaoModel>;
                    item.ListaCondicaoRemuneracao = condicaoRemuneracaoModels;
                });
                */
                //foreach (ContratoRemuneracaoModel item in contratoRemuneracaoModels)
                //{
                //    List<CondicaoRemuneracaoModel> condicaoRemuneracaoModels = await condicaoRemuneracaoService.GetCondicaoRemuneracaoByContratoRemuneracaoAsync(item.Id) as List<CondicaoRemuneracaoModel>;
                //    item.ListaCondicaoRemuneracao = condicaoRemuneracaoModels;
                //}

                return contratoRemuneracaoModels;
            }
        }

        public async Task<bool> UpdateAsync(ContratoRemuneracaoModel item)
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
