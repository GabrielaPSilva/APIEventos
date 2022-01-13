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
    public class FundoService : GenericService<FundoModel>, IFundoService
    {
        public FundoService() : base(new FundoModel(),
                                         "tbl_fundo")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        #region Fundo
        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", _tableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddAsync(FundoModel fundo)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", _tableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
                return await connection.ExecuteAsync(query, fundo) > 0;
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

        public async Task<IEnumerable<FundoModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                             tbl_fundo.*,
	                             tbl_administrador.nome_administrador,
	                             tbl_custodiante.nome_custodiante,
	                             tbl_gestor.nome_gestor,
	                             tbl_tipo_estrategia.estrategia
                             FROM
	                             tbl_fundo
		                            INNER JOIN tbl_administrador ON tbl_fundo.cod_administrador = tbl_administrador.id
		                            INNER JOIN tbl_custodiante ON tbl_fundo.cod_custodiante = tbl_custodiante.id
		                            INNER JOIN tbl_gestor ON tbl_fundo.cod_gestor = tbl_gestor.id
		                            INNER JOIN tbl_tipo_estrategia ON tbl_fundo.cod_tipo_estrategia = tbl_tipo_estrategia.id
                             WHERE
	                             tbl_fundo.ativo = 1
                             ORDER BY
	                             tbl_fundo.nome_reduzido";

                List<FundoModel> fundos = await connection.QueryAsync<FundoModel>(query) as List<FundoModel>;

                ContaService contaService = new ContaService();

                Parallel.ForEach(fundos, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, async fundo =>
                {
                    List<ContaModel> contaList = await contaService.GetFundoByIdAsync(fundo.Id) as List<ContaModel>;
                    fundo.ListaConta = contaList;
                });

                //foreach (FundoModel fundo in fundos)
                //{
                //    List<ContaModel> contaList = await contaService.GetFundoByIdAsync(fundo.Id) as List<ContaModel>;
                //    fundo.ListaConta = contaList;
                //}

                return fundos;
            }
        }

        public async Task<FundoModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                             tbl_fundo.*,
	                             tbl_administrador.nome_administrador,
	                             tbl_custodiante.nome_custodiante,
	                             tbl_gestor.nome_gestor,
	                             tbl_tipo_estrategia.estrategia
                             FROM
	                             tbl_fundo
		                            INNER JOIN tbl_administrador ON tbl_fundo.cod_administrador = tbl_administrador.id
		                            INNER JOIN tbl_custodiante ON tbl_fundo.cod_custodiante = tbl_custodiante.id
		                            INNER JOIN tbl_gestor ON tbl_fundo.cod_gestor = tbl_gestor.id
		                            INNER JOIN tbl_tipo_estrategia ON tbl_fundo.cod_tipo_estrategia = tbl_tipo_estrategia.id
                              WHERE 
	                             tbl_fundo.id = @id";


                FundoModel fundo = await connection.QueryFirstOrDefaultAsync<FundoModel>(query, new { id });

                ContaService contaService = new ContaService();
                List<ContaModel> contaList = await contaService.GetFundoByIdAsync(fundo.Id) as List<ContaModel>;
                fundo.ListaConta = contaList;
                //return null;
                return fundo;
            }
        }

        public async Task<FundoModel> GetFundoExistsBase(string cnpj)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                var query = @"SELECT 
	                             tbl_fundo.*,
	                             tbl_administrador.nome_administrador,
	                             tbl_custodiante.nome_custodiante,
	                             tbl_gestor.nome_gestor,
	                             tbl_tipo_estrategia.estrategia
                             FROM
	                             tbl_fundo
		                            INNER JOIN tbl_administrador ON tbl_fundo.cod_administrador = tbl_administrador.id
		                            INNER JOIN tbl_custodiante ON tbl_fundo.cod_custodiante = tbl_custodiante.id
		                            INNER JOIN tbl_gestor ON tbl_fundo.cod_gestor = tbl_gestor.id
		                            INNER JOIN tbl_tipo_estrategia ON tbl_fundo.cod_tipo_estrategia = tbl_tipo_estrategia.id
                            WHERE 
	                            tbl_fundo.cnpj = @cnpj";

                FundoModel fundo = await connection.QueryFirstOrDefaultAsync<FundoModel>(query, new { cnpj });

                if (fundo == null)
                {
                    return null;
                }

                ContaService contaService = new ContaService();
                List<ContaModel> contaList = await contaService.GetFundoByIdAsync(fundo.Id) as List<ContaModel>;
                fundo.ListaConta = contaList;

                return fundo;
            }
        }

        public async Task<bool> UpdateAsync(FundoModel fundo)
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
                return await connection.ExecuteAsync(query, fundo) > 0;
            }
        }
        #endregion
    }
}
