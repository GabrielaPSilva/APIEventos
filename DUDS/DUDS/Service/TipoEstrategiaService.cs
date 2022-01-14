using Dapper;
using DUDS.Models.Tipos;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class TipoEstrategiaService : GenericService<TipoEstrategiaModel>, ITipoEstrategiaService
    {
        public TipoEstrategiaService() : base(new TipoEstrategiaModel(),
                                    "tbl_tipo_estrategia")
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

        public async Task<bool> AddAsync(TipoEstrategiaModel tipoEstrategia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, tipoEstrategia) > 0;
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

        public async Task<IEnumerable<TipoEstrategiaModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {

                var query = ITipoEstrategiaService.QUERY_BASE + 
                    @"
                    WHERE 
                        ativo = 1
                    ORDER BY    
                        estrategia";

                return await connection.QueryAsync<TipoEstrategiaModel>(query);
            }
        }

        public async Task<TipoEstrategiaModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ITipoEstrategiaService.QUERY_BASE + 
                    @"
                    WHERE 
                        id = @id";

                return await connection.QueryFirstOrDefaultAsync<TipoEstrategiaModel>(query, new { id });
            }
        }

        public async Task<TipoEstrategiaModel> GetTipoEstrategiaExistsBase(string estrategia)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = ITipoEstrategiaService.QUERY_BASE + 
                    @"
                    WHERE 
                        estrategia = @estrategia";

                return await connection.QueryFirstOrDefaultAsync<TipoEstrategiaModel>(query, new { estrategia });
            }
        }

        public async Task<bool> UpdateAsync(TipoEstrategiaModel tipoEstrategia)
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
                return await connection.ExecuteAsync(query, tipoEstrategia) > 0;
            }
        }
    }
}
