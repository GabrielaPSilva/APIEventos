using Dapper;
using DUDS.Models;
using DUDS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ContratoService : IContratoService
    {
        public ContratoService()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IEnumerable<ContratoModel>> GetContrato()
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

        public async Task<ContratoModel> GetContratoById(int id)
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

        public async Task<bool> AddContrato(ContratoModel contrato)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = @"
                                INSERT INTO
                                    tbl_contrato 
                                   (cod_distribuidor, cod_gestor, cod_tipo_contrato)
                                VALUES
                                   (@CodDistribuidor, @CodGestor, @CodTipoContrato)";

                return await connection.ExecuteAsync(query, contrato) > 0;
            }
        }
    }
}
