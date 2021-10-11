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
    public class InvestidorService : GenericService<InvestidorModel>, IInvestidorService
    {
        public InvestidorService() : base(new InvestidorModel(),
                                            "tbl_investidor",
                                            new List<string> { "'id'", "'data_criacao'", "'ativo'" },
                                            new List<string> { "Id", "DataCriacao", "Ativo", "Classificacao", "ListaDistribuidorAdministrador" },
                                            new List<string> { "'id'", "'data_criacao'", "'ativo'", "'usuario_criacao'" },
                                            new List<string> { "Id", "DataCriacao", "Ativo", "Classificacao", "UsuarioCriacao", "ListaDistribuidorAdministrador" })
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }


        #region Investidor
        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddAsync(InvestidorModel investidor)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, investidor) > 0;
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

        public async Task<IEnumerable<InvestidorModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor.*,
	                            tbl_administrador.nome_administrador,
	                            tbl_gestor.nome_gestor,
	                            tbl_tipo_contrato.tipo_contrato,
	                            tbl_grupo_rebate.nome_grupo_rebate
                             FROM
	                            tbl_investidor
		                            LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
		                            LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
		                            INNER JOIN tbl_tipo_contrato ON tbl_investidor.cod_tipo_contrato = tbl_tipo_contrato.id
		                            INNER JOIN tbl_grupo_rebate ON tbl_investidor.cod_grupo_rebate = tbl_grupo_rebate.id
                             WHERE
	                            tbl_investidor.ativo = 1
                             ORDER BY
	                            tbl_investidor.nome_cliente";


                List<InvestidorModel> investidores = await connection.QueryAsync<InvestidorModel>(query) as List<InvestidorModel>;

                InvestidorDistribuidorService investDistriService = new InvestidorDistribuidorService();

                foreach (InvestidorModel investidor in investidores)
                {
                    List<InvestidorDistribuidorModel> investDistriList = await investDistriService.GetInvestidorByIdAsync(investidor.Id) as List<InvestidorDistribuidorModel>;
                    investidor.ListaInvestDistribuidor = investDistriList;
                }

                return investidores;
            }
        }

        public async Task<InvestidorModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor.*,
	                            tbl_administrador.nome_administrador,
	                            tbl_gestor.nome_gestor,
	                            tbl_tipo_contrato.tipo_contrato,
	                            tbl_grupo_rebate.nome_grupo_rebate
                             FROM
	                            tbl_investidor
		                            LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
		                            LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
		                            INNER JOIN tbl_tipo_contrato ON tbl_investidor.cod_tipo_contrato = tbl_tipo_contrato.id
		                            INNER JOIN tbl_grupo_rebate ON tbl_investidor.cod_grupo_rebate = tbl_grupo_rebate.id
                              WHERE 
	                            tbl_investidor.id = @id";


                InvestidorModel investidor = await connection.QueryFirstOrDefaultAsync<InvestidorModel>(query, new { id });

                InvestidorDistribuidorService investDistribuidorService = new InvestidorDistribuidorService();
                List<InvestidorDistribuidorModel> investDistribuidorList = await investDistribuidorService.GetInvestidorByIdAsync(investidor.Id) as List<InvestidorDistribuidorModel>;
                investidor.ListaInvestDistribuidor = investDistribuidorList;
                //return null;
                return investidor;
            }
        }

        public async Task<InvestidorModel> GetInvestidorExistsBase(string cnpj)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                            tbl_investidor.*,
	                            tbl_administrador.nome_administrador,
	                            tbl_gestor.nome_gestor,
	                            tbl_tipo_contrato.tipo_contrato,
	                            tbl_grupo_rebate.nome_grupo_rebate
                             FROM
	                            tbl_investidor
		                            LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
		                            LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
		                            INNER JOIN tbl_tipo_contrato ON tbl_investidor.cod_tipo_contrato = tbl_tipo_contrato.id
		                            INNER JOIN tbl_grupo_rebate ON tbl_investidor.cod_grupo_rebate = tbl_grupo_rebate.id
                            WHERE 
	                            tbl_investidor.cnpj = @cnpj";

                InvestidorModel investidor = await connection.QueryFirstOrDefaultAsync<InvestidorModel>(query, new { cnpj });
                
                if (investidor == null)
                {
                    return null;
                }

                InvestidorDistribuidorService investDistribuidorService = new InvestidorDistribuidorService();
                List<InvestidorDistribuidorModel> investDistribuidorList = await investDistribuidorService.GetInvestidorByIdAsync(investidor.Id) as List<InvestidorDistribuidorModel>;
                investidor.ListaInvestDistribuidor = investDistribuidorList;

                return investidor;
            }
        }

        public async Task<bool> UpdateAsync(InvestidorModel investidor)
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
                return await connection.ExecuteAsync(query, investidor) > 0;
            }
        }
        #endregion

        #region Investidor Distribuidor
        //public async Task<IEnumerable<InvestidorDistribuidorModel>> GetInvestidorDistribuidor()
        //{
        //    using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
        //    {
        //        var query = @"
        //                     SELECT 
        //                     tbl_investidor.*,
        //                     tbl_administrador.nome_administrador,
        //                     tbl_gestor.nome_gestor,
        //                     tbl_tipo_contrato.tipo_contrato,
        //                     tbl_grupo_rebate.nome_grupo_rebate
        //                     FROM
        //                     tbl_investidor
        //                      LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
        //                      LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
        //                      INNER JOIN tbl_tipo_contrato ON tbl_investidor.cod_tipo_contrato = tbl_tipo_contrato.id
        //                      INNER JOIN tbl_grupo_rebate ON tbl_investidor.cod_grupo_rebate = tbl_grupo_rebate.id
        //                     WHERE
        //                     tbl_investidor.ativo = 1
        //                     ORDER BY
        //                     tbl_investidor.nome_cliente";

        //        return await connection.QueryAsync<InvestidorModel>(query);
        //    }
        //}

        //public async Task<InvestidorModel> GetInvestidorById(int id)
        //{
        //    using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
        //    {
        //        const string query = @"
        //                             SELECT 
        //                           tbl_investidor.*,
        //                           tbl_administrador.nome_administrador,
        //                           tbl_gestor.nome_gestor,
        //                           tbl_tipo_contrato.tipo_contrato,
        //                           tbl_grupo_rebate.nome_grupo_rebate
        //                            FROM
        //                           tbl_investidor
        //                            LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
        //                            LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
        //                            INNER JOIN tbl_tipo_contrato ON tbl_investidor.cod_tipo_contrato = tbl_tipo_contrato.id
        //                            INNER JOIN tbl_grupo_rebate ON tbl_investidor.cod_grupo_rebate = tbl_grupo_rebate.id
        //                            WHERE
        //                           tbl_investidor.id = @id";

        //        return await connection.QueryFirstOrDefaultAsync<InvestidorModel>(query, new { id });
        //    }
        //}

        //public async Task<InvestidorModel> GetInvestidorExistsBase(string cnpj)
        //{
        //    using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
        //    {
        //        const string query = @"
        //                            SELECT 
        //                           tbl_investidor.*,
        //                           tbl_administrador.nome_administrador,
        //                           tbl_gestor.nome_gestor,
        //                           tbl_tipo_contrato.tipo_contrato,
        //                           tbl_grupo_rebate.nome_grupo_rebate
        //                            FROM
        //                           tbl_investidor
        //                            LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
        //                            LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
        //                            INNER JOIN tbl_tipo_contrato ON tbl_investidor.cod_tipo_contrato = tbl_tipo_contrato.id
        //                            INNER JOIN tbl_grupo_rebate ON tbl_investidor.cod_grupo_rebate = tbl_grupo_rebate.id
        //                            WHERE
        //                           tbl_investidor.cnpj = @cnpj";

        //        return await connection.QueryFirstOrDefaultAsync<InvestidorModel>(query, new { cnpj });
        //    }
        //}

        //public async Task<bool> AddInvestidor(InvestidorModel investidor)
        //{
        //    using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
        //    {
        //        const string query = @"
        //                        INSERT INTO
        //                            tbl_investidor 
        //                           (nome_cliente, cnpj, tipo_cliente, cod_administrador, cod_gestor, cod_tipo_contrato, cod_grupo_rebate)
        //                        VALUES
        //                           (@NomeCliente, @Cnpj, @TipoCliente, @CodAdministrador, @CodGestor, @CodTipoContrato, @CodGrupoRebate)";

        //        return await connection.ExecuteAsync(query, investidor) > 0;
        //    }
        //}

        //public async Task<bool> UpdateInvestidor(InvestidorModel investidor)
        //{
        //    using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
        //    {
        //        const string query = @"
        //                        UPDATE
        //                            tbl_investidor
        //                        SET 
        //                            nome_cliente = @NomeCliente, 
        //                            cnpj = @Cnpj, 
        //                            tipo_cliente = @TipoCliente,
        //                            cod_administrador = @CodAdministrador,
        //                            cod_gestor = @CodGestor, 
        //                            cod_tipo_contrato = @CodTipoContrato, 
        //                            cod_grupo_rebate = @CodGrupoRebate
        //                        WHERE    
        //                            id = @id";

        //        return await connection.ExecuteAsync(query, investidor) > 0;
        //    }
        //}

        //public async Task<bool> DisableInvestidor(int id)
        //{
        //    using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
        //    {
        //        const string query = @"
        //                        UPDATE
        //                            tbl_investidor
        //                        SET 
        //                            ativo = 0
        //                        WHERE    
        //                            id = @id";

        //        return await connection.ExecuteAsync(query, new { id }) > 0;
        //    }
        //}

        //public async Task<bool> ActivateInvestidor(int id)
        //{
        //    using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
        //    {
        //        const string query = @"
        //                        UPDATE
        //                            tbl_investidor
        //                        SET 
        //                            ativo = 1
        //                        WHERE    
        //                            id = @id";

        //        return await connection.ExecuteAsync(query, new { id }) > 0;
        //    }
        //}
        #endregion
    }
}
