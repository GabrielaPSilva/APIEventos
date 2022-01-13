using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class GestorService : GenericService<GestorModel>, IGestorService
    {
        public GestorService() : base(new GestorModel(),
            "tbl_gestor")
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

        public async Task<bool> AddAsync(GestorModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
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

        public async Task<IEnumerable<GestorModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {

                var query = @"SELECT
                                gestor.*,
                                tipo_classificacao.classificacao as Classificacao
                              FROM
	                            tbl_gestor gestor
                                    INNER JOIN tbl_tipo_classificacao tipo_classificacao ON gestor.cod_tipo_classificacao = tipo_classificacao.id
                            WHERE 
	                            gestor.ativo = 1
                            ORDER BY    
                                gestor.nome_gestor";

                return await connection.QueryAsync<GestorModel>(query);
            }
        }

        public async Task<GestorModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            gestor.*,
                                tipo_classificacao.classificacao
                              FROM
	                            tbl_gestor gestor
                                    INNER JOIN tbl_tipo_classificacao tipo_classificacao ON gestor.cod_tipo_classificacao = tipo_classificacao.id
                            WHERE 
	                            gestor.id = @id";

                return await connection.QueryFirstOrDefaultAsync<GestorModel>(query, new { id });
            }
        }

        public async Task<GestorModel> GetGestorExistsBase(string cnpj)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            gestor.*,
                                tipo_classificacao.classificacao
                              FROM
	                            tbl_gestor gestor
                                    INNER JOIN tbl_tipo_classificacao tipo_classificacao ON gestor.cod_tipo_classificacao = tipo_classificacao.id
                            WHERE 
	                            gestor.cnpj = @cnpj";

                return await connection.QueryFirstOrDefaultAsync<GestorModel>(query, new { cnpj });
            }
        }

        public async Task<bool> UpdateAsync(GestorModel item)
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
