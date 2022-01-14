using DUDS.Models.Passivo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IOrdemPassivoService : IGenericOperationsService<OrdemPassivoModel>
    {
        const string query =
            @"
            SELECT
	            tbl_ordem_passivo.*,
	            tbl_fundo.nome_reduzido AS nome_fundo,
	            tbl_investidor.nome_investidor,
	            tbl_administrador.nome_administrador
            FROM
	            tbl_ordem_passivo
		        INNER JOIN tbl_fundo ON tbl_ordem_passivo.cod_fundo = tbl_fundo.id
                INNER JOIN tbl_investidor_distribuidor ON tbl_ordem_passivo.cod_investidor_distribuidor = tbl_investidor_distribuidor.id
		        INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
		        INNER JOIN tbl_administrador ON tbl_ordem_passivo.cod_administrador = tbl_administrador.id";

        Task<bool> DeleteByDataRefAsync(DateTime dataRef);

        Task<IEnumerable<OrdemPassivoViewModel>> GetByDataEntradaAsync(DateTime? dataEntrada);

        Task<IEnumerable<OrdemPassivoModel>> AddBulkAsync(List<OrdemPassivoModel> item);

        Task<IEnumerable<OrdemPassivoViewModel>> GetAllAsync();
    }
}
