using Dapper;
using DUDS.Models;
using DUDS.Models.Contrato;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ContratoFundoService: GenericService<ContratoFundoModel>, IContratoFundoService
    {
        public ContratoFundoService() : base(new ContratoFundoModel(),"tbl_contrato_fundo")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(ContratoFundoModel item)
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

        public async Task<IEnumerable<ContratoFundoViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoFundoService.QUERY_BASE + 
                    @"
                    WHERE
                        contrato.ativo = 1
                    ORDER BY
                        fundo.nome_reduzido";

                return await connection.QueryAsync<ContratoFundoViewModel>(query);
            }
        }

        public async Task<ContratoFundoViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoFundoService.QUERY_BASE + 
                    @"
                    WHERE
                        contrato_fundo.id = @id
                    ORDER BY
                        fundo.nome_reduzido";
                return await connection.QueryFirstOrDefaultAsync<ContratoFundoViewModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<ContratoFundoViewModel>> GetContratoFundoBySubContratoAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoFundoService.QUERY_BASE + 
                    @"
                    WHERE
                        sub_contrato.id = @id
                    ORDER BY
                        fundo.nome_reduzido";
                List<ContratoFundoViewModel> contratoFundoModels = await connection.QueryAsync<ContratoFundoViewModel>(query, new { id }) as List<ContratoFundoViewModel>;
                
                /*
                ContratoRemuneracaoService contratoRemuneracaoService = new ContratoRemuneracaoService();
                Parallel.ForEach(contratoFundoModels, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, async item =>
                {
                    List<ContratoRemuneracaoModel> contratoRemuneracaoModels = await contratoRemuneracaoService.GetContratoFundoByIdAsync(item.Id) as List<ContratoRemuneracaoModel>;
                    item.ListaContratoRemuneracao = contratoRemuneracaoModels;
                });
                */

                //foreach (var item in contratoFundoModels)
                //{
                //    List<ContratoRemuneracaoModel> contratoRemuneracaoModels = await contratoRemuneracaoService.GetContratoRemuneracaoByContratoFundoAsync(item.Id) as List<ContratoRemuneracaoModel>;
                //    item.ListaContratoRemuneracao = contratoRemuneracaoModels;
                //}

                return contratoFundoModels;
            }
        }

        public async Task<bool> UpdateAsync(ContratoFundoModel item)
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
