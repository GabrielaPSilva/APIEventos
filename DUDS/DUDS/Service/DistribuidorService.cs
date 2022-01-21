using Dapper;
using DUDS.Models.Distribuidor;
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
                                       "tbl_distribuidor")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> AddAsync(DistribuidorModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, item) > 0;
            }
        }

        public async Task<bool> UpdateAsync(DistribuidorModel item)
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

        public Task<bool> DeleteAsync(int id)
        {
            return DisableAsync(id);
        }

        public async Task<bool> DisableAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<DistribuidorViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IDistribuidorAdministradorService.QUERY_BASE +
                            @"
                              WHERE 
	                             distribuidor.ativo = 1
                              ORDER BY    
                                 distribuidor.nome_distribuidor";


                List<DistribuidorViewModel> distribuidores = await connection.QueryAsync<DistribuidorViewModel>(query) as List<DistribuidorViewModel>;

                DistribuidorAdministradorService distrAdmService = new DistribuidorAdministradorService();

                Parallel.ForEach(distribuidores, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, async distribuidor =>
                {
                    List<DistribuidorAdministradorViewModel> distrAdmList = await distrAdmService.GetDistribuidorByIdAsync(distribuidor.Id) as List<DistribuidorAdministradorViewModel>;
                    distribuidor.ListaDistribuidorAdministrador = distrAdmList;
                });

                return distribuidores;
            }
        }

        public async Task<DistribuidorViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IDistribuidorAdministradorService.QUERY_BASE +
                            @"
                               WHERE 
	                             distribuidor.id = @id";

                DistribuidorViewModel distribuidor = await connection.QueryFirstOrDefaultAsync<DistribuidorViewModel>(query, new { id });

                DistribuidorAdministradorService distrAdmService = new DistribuidorAdministradorService();
                List<DistribuidorAdministradorViewModel> distrAdmList = await distrAdmService.GetDistribuidorByIdAsync(distribuidor.Id) as List<DistribuidorAdministradorViewModel>;
                distribuidor.ListaDistribuidorAdministrador = distrAdmList;
                
                return distribuidor;
            }
        }

        public async Task<DistribuidorViewModel> GetDistribuidorExistsBase(string cnpj)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = IDistribuidorAdministradorService.QUERY_BASE +
                        @"
                           WHERE 
	                          gestor.cnpj = @cnpj";

                DistribuidorViewModel distribuidor = await connection.QueryFirstOrDefaultAsync<DistribuidorViewModel>(query, new { cnpj });
                
                if (distribuidor == null)
                {
                    return null;
                }

                DistribuidorAdministradorService distrAdmService = new DistribuidorAdministradorService();
                List<DistribuidorAdministradorViewModel> distrAdmList = await distrAdmService.GetDistribuidorByIdAsync(distribuidor.Id) as List<DistribuidorAdministradorViewModel>;
                distribuidor.ListaDistribuidorAdministrador = distrAdmList;

                return distribuidor;
            }
        }
    }
}
