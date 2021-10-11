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
    public class TipoContratoService : GenericService<TipoContratoModel>, ITipoContratoService
    {
        public TipoContratoService() : base(new TipoContratoModel(),
                                            "tbl_tipo_contrato",
                                            new List<string> { "'id'", "'data_criacao'", "'ativo'" },
                                            new List<string> { "Id", "DataCriacao", "Ativo" },
                                            new List<string> { "'id'", "'data_criacao'", "'ativo'", "'usuario_criacao'" },
                                            new List<string> { "Id", "DataCriacao", "Ativo", "UsuarioCriacao" })
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

        public async Task<bool> AddAsync(TipoContratoModel tipoContrato)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, tipoContrato) > 0;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            return DisableAsync(id);
        }

        public async Task<bool> DisableAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<TipoContratoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {

                var query = @"SELECT
                                 *
                              FROM
	                             tbl_tipo_contrato
                              WHERE 
	                             ativo = 1
                              ORDER BY    
                                 tipo_contrato";

                return await connection.QueryAsync<TipoContratoModel>(query);
            }
        }

        public async Task<TipoContratoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                 *
                              FROM
	                             tbl_tipo_contrato
                              WHERE 
	                             id = @id";

                return await connection.QueryFirstOrDefaultAsync<TipoContratoModel>(query, new { id });
            }
        }

        public async Task<TipoContratoModel> GetTipoContaExistsBase(string tipoContrato)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                 *
                              FROM
	                             tbl_tipo_contrato
                              WHERE 
	                             tipo_contrato = @tipo_contrato";

                return await connection.QueryFirstOrDefaultAsync<TipoContratoModel>(query, new { tipo_contrato = tipoContrato });
            }
        }

        public async Task<bool> UpdateAsync(TipoContratoModel tipoContrato)
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
                return await connection.ExecuteAsync(query, tipoContrato) > 0;
            }
        }
    }
}
