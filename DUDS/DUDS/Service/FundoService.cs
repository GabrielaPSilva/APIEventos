using Dapper;
using DUDS.Models.Fundo;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class FundoService : GenericService<FundoModel>, IFundoService
    {
        public FundoService() : base(new FundoModel(),"tbl_fundo")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        #region Fundo
        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddAsync(FundoModel fundo)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
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
                string query = GenericSQLCommands.DISABLE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<IEnumerable<FundoViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IFundoService.QUERY_BASE +
                    @"
                    WHERE
	                    tbl_fundo.ativo = 1
                    ORDER BY
	                    tbl_fundo.nome_reduzido";

                List<FundoViewModel> fundos = await connection.QueryAsync<FundoViewModel>(query) as List<FundoViewModel>;

                /*
                ContaService contaService = new ContaService();

                Parallel.ForEach(fundos, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, async fundo =>
                {
                    List<ContaViewModel> contaList = await contaService.GetFundoByIdAsync(fundo.Id) as List<ContaViewModel>;
                    fundo.ListaConta = contaList;
                });
                */

                //foreach (FundoModel fundo in fundos)
                //{
                //    List<ContaModel> contaList = await contaService.GetFundoByIdAsync(fundo.Id) as List<ContaModel>;
                //    fundo.ListaConta = contaList;
                //}

                return fundos;
            }
        }

        public async Task<FundoViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IFundoService.QUERY_BASE + 
                    @"
                    WHERE 
	                    tbl_fundo.id = @id";


                FundoViewModel fundo = await connection.QueryFirstOrDefaultAsync<FundoViewModel>(query, new { id });

                /*
                ContaService contaService = new ContaService();
                List<ContaViewModel> contaList = await contaService.GetFundoByIdAsync(fundo.Id) as List<ContaViewModel>;
                fundo.ListaConta = contaList;
                //return null;
                */
                return fundo;
            }
        }

        public async Task<FundoViewModel> GetFundoExistsBase(string cnpj)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = $@"{IFundoService.QUERY_BASE}
                    WHERE 
	                    tbl_fundo.cnpj = @cnpj";

                FundoViewModel fundo = await connection.QueryFirstOrDefaultAsync<FundoViewModel>(query, new { cnpj });

                if (fundo == null)
                {
                    return null;
                }
                
                /*
                ContaService contaService = new ContaService();
                List<ContaViewModel> contaList = await contaService.GetFundoByIdAsync(fundo.Id) as List<ContaViewModel>;
                fundo.ListaConta = contaList;
                */
                
                return fundo;
            }
        }

        public async Task<bool> UpdateAsync(FundoModel fundo)
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
                return await connection.ExecuteAsync(query, fundo) > 0;
            }
        }
        #endregion
    }
}
