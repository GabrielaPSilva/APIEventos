using Dapper;
using DUDS.Models.Contrato;
using DUDS.Service.Interface;
using DUDS.Service.SQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ContratoService : GenericService<ContratoModel>, IContratoService
    {
        public ContratoService() : base(new ContratoModel(),"tbl_contrato")
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<bool> ActivateAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.ACTIVATE_COMMAND.Replace("TABELA", TableName);
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
        }

        public async Task<bool> AddAsync(ContratoModel item)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = GenericSQLCommands.INSERT_COMMAND.Replace("TABELA", TableName).Replace("CAMPOS", String.Join(",", _fieldsInsert)).Replace("VALORES", String.Join(",", _propertiesInsert));
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

        public async Task<IEnumerable<ContratoViewModel>> GetAllAsync()
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoService.QUERY_BASE +
                    @"
                    WHERE
	                    tbl_contrato.Ativo = 1";
                List<ContratoViewModel> listaContratoModel = await connection.QueryAsync<ContratoViewModel>(query) as List<ContratoViewModel>;

                /*
                SubContratoService subContratoService = new SubContratoService();
                Parallel.ForEach(listaContratoModel, new ParallelOptions { MaxDegreeOfParallelism = maxParallProcess }, async x =>
                {
                    List<SubContratoModel> listaSubContrato = await subContratoService.GetContratoByIdAsync(x.Id) as List<SubContratoModel>;
                    x.ListaSubContrato = listaSubContrato;
                });
                */
                //foreach (ContratoModel item in listaContratoModel)
                //{
                //    List<SubContratoModel> listaSubContrato = await subContratoService.GetSubContratoCompletoByIdAsync(item.Id) as List<SubContratoModel>;
                //    item.ListaSubContrato = listaSubContrato;
                //}

                return listaContratoModel;
            }
        }

        public async Task<ContratoViewModel> GetByIdAsync(int id)
        {
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                const string query = IContratoService.QUERY_BASE + 
                    @"
                    WHERE
                        Id = @id";

                return await connection.QueryFirstOrDefaultAsync<ContratoViewModel>(query, new { id });
            }
        }

        public async Task<bool> UpdateAsync(ContratoModel item)
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

        public async Task<IEnumerable<EstruturaContratoViewModel>> GetContratosRebateAsync(string subContratoStatus)
        {
            string whereClause;
            if (subContratoStatus == "Ativo")
            {
                subContratoStatus = "Inativo";
                whereClause = @"
                                sub_contrato.Status <> @Status
                                AND sub_contrato.Status <> 'Em Encerramento'";
            }
            else
            {
                whereClause = "sub_contrato.Status = @Status";
            }
            using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
            {
                string query = @"
                            SELECT 
                                contrato_remuneracao.PercentualAdm,
                                contrato_remuneracao.PercentualPfee,
                                contrato.CodTipoContrato,
                                contrato.CodGestor,
                                contrato.CodDistribuidor,
                                sub_contrato.Versao,
                                sub_contrato.Status,
                                sub_contrato.ClausulaRetroatividade,
                                sub_contrato.DataRetroatividade,
                                sub_contrato.DataVigenciaInicio,
                                sub_contrato.DataVigenciaFim,
                                contrato_alocador.CodInvestidor AS CodInvestidorContrato,
                                contrato_fundo.CodFundo,
                                contrato_fundo.CodTipoCondicao,
                                investidor_distribuidor.CodInvestAdministrador AS CodInvestAdministrador,
                                investidor_distribuidor.CodAdministrador AS AdministradorCodigoInvestidor,
                                investidor_distribuidor.CodDistribuidorAdministrador AS DistribuidorCodigoInvestidor,
                                contrato.Id AS CodContrato,
                                sub_contrato.Id AS CodSubContrato,
                                contrato_fundo.Id AS CodContratoFundo,
                                contrato_remuneracao.Id AS CodContratoRemuneracao
                            FROM
                                tbl_contrato contrato
                                INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.CodContrato = contrato.Id
                                INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.CodSubContrato = sub_contrato.Id
                                INNER JOIN tbl_contrato_remuneracao contrato_remuneracao ON contrato_remuneracao.CodContratoFundo = contrato_fundo.Id
                                LEFT JOIN tbl_contrato_alocador contrato_alocador ON contrato_alocador.CodSubContrato = sub_contrato.Id
                                LEFT JOIN tbl_investidor_distribuidor investidor_distribuidor ON investidor_distribuidor.CodInvestidor = contrato_alocador.CodInvestidor
                            WHERE
                                WHERE_CLAUSE";
                query = query.Replace("WHERE_CLAUSE", whereClause);

                List<EstruturaContratoViewModel> listaContratoModel = await connection.QueryAsync<EstruturaContratoViewModel>(query, new { Status = subContratoStatus },commandTimeout:180) as List<EstruturaContratoViewModel>;
                return listaContratoModel;
            }
        }
    }
}
