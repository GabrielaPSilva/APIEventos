using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class DistribuidorService : GenericService<DistribuidorModel>, IDistribuidorService
    {
        public DistribuidorService() : base(new DistribuidorModel(),
            "tbl_distribuidor",
            new List<string> { "'id'", "'data_criacao'", "'ativo'" },
            new List<string> { "Id", "DataCriacao", "Ativo", "Classificacao", "ListaDistribuidorAdministrador" },
            new List<string> { "'id'", "'data_criacao'", "'ativo'", "'usuario_criacao'" },
            new List<string> { "Id", "DataCriacao", "Ativo", "Classificacao", "UsuarioCriacao", "ListaDistribuidorAdministrador" })
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

        public async Task<bool> AddAsync(DistribuidorModel item)
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

        public async Task<IEnumerable<DistribuidorModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                distribuidor.*,
                                tipo_classificacao.classificacao
                              FROM
	                            tbl_distribuidor distribuidor
                                    INNER JOIN tbl_tipo_classificacao tipo_classificacao ON distribuidor.cod_tipo_classificacao = tipo_classificacao.id
                            WHERE 
	                            distribuidor.ativo = 1
                            ORDER BY    
                                distribuidor.nome_distribuidor";


                List<DistribuidorModel> distribuidores = await connection.QueryAsync<DistribuidorModel>(query) as List<DistribuidorModel>;

                DistribuidorAdministradorService distrAdmService = new DistribuidorAdministradorService();

                Parallel.ForEach(distribuidores, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, async distribuidor =>
                {
                    List<DistribuidorAdministradorModel> distrAdmList = await distrAdmService.GetDistribuidorByIdAsync(distribuidor.Id) as List<DistribuidorAdministradorModel>;
                    distribuidor.ListaDistribuidorAdministrador = distrAdmList;
                });
                //foreach (DistribuidorModel distribuidor in distribuidores)
                //{
                //    List<DistribuidorAdministradorModel> distrAdmList = await distrAdmService.GetDistribuidorByIdAsync(distribuidor.Id) as List<DistribuidorAdministradorModel>;
                //    distribuidor.ListaDistribuidorAdministrador = distrAdmList;
                //}

                return distribuidores;
            }
        }

        public async Task<DistribuidorModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT
                                distribuidor.*,
                                tipo_classificacao.classificacao
                              FROM
	                            tbl_distribuidor distribuidor
                                    INNER JOIN tbl_tipo_classificacao tipo_classificacao ON distribuidor.cod_tipo_classificacao = tipo_classificacao.id
                            WHERE 
	                            distribuidor.id = @id";


                DistribuidorModel distribuidor = await connection.QueryFirstOrDefaultAsync<DistribuidorModel>(query, new { id });

                DistribuidorAdministradorService distrAdmService = new DistribuidorAdministradorService();
                List<DistribuidorAdministradorModel> distrAdmList = await distrAdmService.GetDistribuidorByIdAsync(distribuidor.Id) as List<DistribuidorAdministradorModel>;
                distribuidor.ListaDistribuidorAdministrador = distrAdmList;
                //return null;
                return distribuidor;
            }
        }

        public async Task<DistribuidorModel> GetDistribuidorExistsBase(string cnpj)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            distribuidor.*,
                                tipo_classificacao.classificacao
                              FROM
	                            tbl_gestor distribuidor
                                    INNER JOIN tbl_tipo_classificacao tipo_classificacao ON gestor.cod_tipo_classificacao = tipo_classificacao.id
                            WHERE 
	                            gestor.cnpj = @cnpj";

                DistribuidorModel distribuidor = await connection.QueryFirstOrDefaultAsync<DistribuidorModel>(query, new { cnpj });
                if (distribuidor == null)
                {
                    return null;
                }

                DistribuidorAdministradorService distrAdmService = new DistribuidorAdministradorService();
                List<DistribuidorAdministradorModel> distrAdmList = await distrAdmService.GetDistribuidorByIdAsync(distribuidor.Id) as List<DistribuidorAdministradorModel>;
                distribuidor.ListaDistribuidorAdministrador = distrAdmList;

                return distribuidor;
            }
        }

        public async Task<bool> UpdateAsync(DistribuidorModel item)
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
