using DUDS.Models.Passivo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IOrdemPassivoService : IGenericOperationsService<OrdemPassivoModel>
    {
        const string QUERY_BASE =
            @"
            SELECT
	            tbl_ordem_passivo.*,
	            tbl_fundo.NomeReduzido AS NomeFundo,
	            tbl_investidor.NomeInvestidor,
	            tbl_administrador.NomeAdministrador
            FROM
	            tbl_ordem_passivo
		        INNER JOIN tbl_fundo ON tbl_ordem_passivo.CodFundo = tbl_fundo.Id
                INNER JOIN tbl_investidor_distribuidor ON tbl_ordem_passivo.CodInvestidorDistribuidor = tbl_investidor_distribuidor.Id
		        INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.CodInvestidor = tbl_investidor.Id
		        INNER JOIN tbl_administrador ON tbl_ordem_passivo.CodAdministrador = tbl_administrador.Id";

        Task<bool> DeleteByDataRefAsync(DateTime dataRef);

        Task<IEnumerable<OrdemPassivoViewModel>> GetByDataEntradaAsync(DateTime? dataEntrada);

        Task<IEnumerable<OrdemPassivoModel>> AddBulkAsync(List<OrdemPassivoModel> item);

        Task<IEnumerable<OrdemPassivoViewModel>> GetAllAsync();
    }
}
