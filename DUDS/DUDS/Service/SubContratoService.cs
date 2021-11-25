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
    public class SubContratoService : GenericService<SubContratoModel>, ISubContratoService
    {
        public SubContratoService() : base(new SubContratoModel(),
            "tbl_sub_contrato",
            new List<string> { "'id'", "'data_criacao'" },
            new List<string> { "Id", "DataCriacao", "ListaContratoAlocador", "ListaContratoFundo" },
            new List<string> { "'id'", "'data_criacao'", "'usuario_criacao'" },
            new List<string> { "Id", "DataCriacao", "UsuarioCriacao", "ListaContratoAlocador", "ListaContratoFundo" })
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
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
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

        public async Task<IEnumerable<SubContratoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            sub_contrato.*,
                             FROM
	                            tbl_sub_contrato sub_contrato
                                INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrado.cod_contrato
                             WHERE
	                            contrato.ativo = 1 AND
                                sub_contrato.status = 'Ativo'
                             ORDER BY
                                contrato.id";

                return await connection.QueryAsync<SubContratoModel>(query);
            }
        }

        public async Task<SubContratoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
                                        sub_contrato.*,
                                       FROM
                                        tbl_sub_contrato sub_contrato
                                       WHERE
                                        id = @id";
                return await connection.QueryFirstOrDefaultAsync<SubContratoModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<SubContratoModel>> GetContratoByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            sub_contrato.*,
                             FROM
	                            tbl_sub_contrato sub_contrato
                                INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrado.cod_contrato
                             WHERE
	                            contrato.id = @id
                             ORDER BY
                                contrato.id";
                List<SubContratoModel> subContratoModels = await connection.QueryAsync<SubContratoModel>(query, new { id }) as List<SubContratoModel>;
                ContratoAlocadorService contratoAlocadorService = new ContratoAlocadorService();
                ContratoFundoService contratoFundoService = new ContratoFundoService();

                Parallel.ForEach(subContratoModels, new ParallelOptions { MaxDegreeOfParallelism = maxParalleProcess }, async item =>
                {
                    List<ContratoFundoModel> contratoFundoModels = await contratoFundoService.GetSubContratoByIdAsync(item.Id) as List<ContratoFundoModel>;
                    item.ListaContratoFundo = contratoFundoModels;

                    List<ContratoAlocadorModel> contratoAlocadorModels = await contratoAlocadorService.GetSubContratoByIdAsync(item.Id) as List<ContratoAlocadorModel>;
                    item.ListaContratoAlocador = contratoAlocadorModels;
                });
                //foreach (SubContratoModel item in subContratoModels)
                //{
                //    List<ContratoFundoModel> contratoFundoModels = await contratoFundoService.GetSubContratoByIdAsync(item.Id) as List<ContratoFundoModel>;
                //    item.ListaContratoFundo = contratoFundoModels;

                //    List<ContratoAlocadorModel> contratoAlocadorModels = await contratoAlocadorService.GetSubContratoByIdAsync(item.Id) as List<ContratoAlocadorModel>;
                //    item.ListaContratoAlocador = contratoAlocadorModels;
                //}

                return subContratoModels;
            }
        }

        public async Task<bool> UpdateAsync(SubContratoModel item)
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
    }
}
