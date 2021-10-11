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
    public class ContratoService : GenericService<ContratoModel>, IContratoService
    {
        public ContratoService(): base(new ContratoModel(),
            "tbl_contrato",
            new List<string> { "'id'", "'data_criacao'", "'ativo'" },
            new List<string> { "Id", "DataCriacao", "Ativo", "NomeDistribuidor", "NomeGestor", "TipoContrato" },
            new List<string> { "'id'", "'data_criacao'", "'ativo'", "'usuario_criacao'" },
            new List<string> { "Id", "DataCriacao", "Ativo", "UsuarioCriacao", "NomeDistribuidor", "NomeGestor", "TipoContrato" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddAsync(ContratoModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Disable(id);
        }

        public async Task<bool> Disable(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<ContratoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_contrato.*,
	                            tbl_distribuidor.nome_distribuidor,
	                            tbl_gestor.nome_gestor,
	                            tbl_tipo_contrato.tipo_contrato
                             FROM
	                            tbl_contrato
	                            LEFT JOIN tbl_distribuidor ON tbl_contrato.cod_distribuidor = tbl_distribuidor.id
	                            LEFT JOIN tbl_gestor ON tbl_contrato.cod_gestor = tbl_gestor.id
	                            INNER JOIN tbl_tipo_contrato ON tbl_contrato.cod_tipo_contrato = tbl_tipo_contrato.id
                             WHERE
	                            tbl_contrato.ativo = 1";

                return await connection.QueryAsync<ContratoModel>(query);
            }
        }

        public async Task<ContratoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                       SELECT 
	                                    tbl_contrato.*,
	                                    tbl_distribuidor.nome_distribuidor,
	                                    tbl_gestor.nome_gestor,
	                                    tbl_tipo_contrato.tipo_contrato
                                     FROM
	                                    tbl_contrato
	                                    LEFT JOIN tbl_distribuidor ON tbl_contrato.cod_distribuidor = tbl_distribuidor.id
	                                    LEFT JOIN tbl_gestor ON tbl_contrato.cod_gestor = tbl_gestor.id
	                                    INNER JOIN tbl_tipo_contrato ON tbl_contrato.cod_tipo_contrato = tbl_tipo_contrato.id
                                     WHERE
                                        id = @id";

                return await connection.QueryFirstOrDefaultAsync<ContratoModel>(query, new { id });
            }
        }

        public async Task<bool> UpdateAsync(ContratoModel item)
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
