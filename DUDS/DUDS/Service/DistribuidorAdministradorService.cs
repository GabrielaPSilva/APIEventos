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
    public class DistribuidorAdministradorService : GenericService<DistribuidorAdministradorModel>, IDistribuidorAdministradorService
    {
        public DistribuidorAdministradorService() : base(new DistribuidorAdministradorModel(),
            "tbl_distribuidor_administrador",
            new List<string> { "'id'", "'data_criacao'" },
            new List<string> { "Id", "DataCriacao", "NomeDistribuidor", "NomeAdministrador" },
            new List<string> { "'id'", "'data_criacao'", "'usuario_criacao'" },
            new List<string> { "Id", "DataCriacao", "UsuarioCriacao", "NomeDistribuidor", "NomeAdministrador" })
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

        public async Task<bool> AddAsync(DistribuidorAdministradorModel item)
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

        public async Task<IEnumerable<DistribuidorAdministradorModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                distr_adm.*,
                                distribuidor.nome_distribuidor,
                                administrador.nome_administrador
                              FROM
	                            tbl_distribuidor_administrador distr_adm
                                inner join tbl_distribuidor distribuidor on distr_adm.cod_distribuidor = distribuidor.id
                                inner join tbl_administrador administrador on distr_adm.cod_administrador = administrador.id
                            WHERE 
	                            distr_adm.ativo = 1
                            ORDER BY    
                                distribuidor.nome_distribuidor";

                return await connection.QueryAsync<DistribuidorAdministradorModel>(query);
            }
        }

        public async Task<IEnumerable<DistribuidorAdministradorModel>> GetByDistribuidorIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                distr_adm.*,
                                distribuidor.nome_distribuidor,
                                administrador.nome_administrador
                              FROM
	                            tbl_distribuidor_administrador distr_adm
                                inner join tbl_distribuidor distribuidor on distr_adm.cod_distribuidor = distribuidor.id
                                inner join tbl_administrador administrador on distr_adm.cod_administrador = administrador.id
                            WHERE 
	                            distr_adm.cod_distribuidor = @id
                            ORDER BY    
                                distribuidor.nome_distribuidor";

                //List<DistribuidorAdministradorModel> distrAdmList = await connection.QueryAsync<DistribuidorAdministradorModel>(query, new { id }) as List<DistribuidorAdministradorModel>;
                //return distrAdmList;
                return await connection.QueryAsync<DistribuidorAdministradorModel>(query, new { id });
            }
        }

        public async Task<DistribuidorAdministradorModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                distr_adm.*,
                                distribuidor.nome_distribuidor,
                                administrador.nome_administrador
                              FROM
	                            tbl_distribuidor_administrador distr_adm
                                inner join tbl_distribuidor distribuidor on distr_adm.cod_distribuidor = distribuidor.id
                                inner join tbl_administrador administrador on distr_adm.cod_administrador = administrador.id
                            WHERE 
	                            distr_adm.id = @id
                            ORDER BY    
                                distribuidor.nome_distribuidor";

                return await connection.QueryFirstOrDefaultAsync<DistribuidorAdministradorModel>(query, new { id });
            }
        }

        public async Task<bool> UpdateAsync(DistribuidorAdministradorModel item)
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
