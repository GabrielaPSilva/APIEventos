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
    public class ContratoAlocadorService : GenericService<ContratoAlocadorModel>, IContratoAlocadorService
    {
        public ContratoAlocadorService() : base(new ContratoAlocadorModel(),
            "tbl_contrato_alocador",
            new List<string> { "'id'", "'data_criacao'" },
            new List<string> { "Id", "DataCriacao", "NomeInvestidor" },
            new List<string> { "'id'", "'data_criacao'", "'usuario_criacao'" },
            new List<string> { "Id", "DataCriacao", "UsuarioCriacao", "NomeInvestidor" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Task<bool> ActivateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddAsync(ContratoAlocadorModel item)
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

        public async Task<IEnumerable<ContratoAlocadorModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            contrato_alocador.*,
                                investidor.nome_investidor
                             FROM
	                            tbl_contrato_alocador contrato_alocador
                                inner join tbl_investidor investidor on contrato.id = contrato_alocador.cod_investidor
                             ORDER BY
                                investidor.nome_investidor";

                return await connection.QueryAsync<ContratoAlocadorModel>(query);
            }
        }

        public async Task<ContratoAlocadorModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"SELECT 
	                                    contrato_alocador.*,
                                        investidor.nome_investidor
                                     FROM
	                                    tbl_contrato_alocador contrato_alocador
                                        inner join tbl_investidor investidor on contrato.id = contrato_alocador.cod_investidor
                                       WHERE
                                        id = @id";
                return await connection.QueryFirstOrDefaultAsync<ContratoAlocadorModel>(query, new { id });
            }
        }

        public async Task<bool> UpdateAsync(ContratoAlocadorModel item)
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
