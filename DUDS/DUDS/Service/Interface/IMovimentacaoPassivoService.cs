using DUDS.Models.Passivo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IMovimentacaoPassivoService : IGenericOperationsService<MovimentacaoPassivoModel>
    {
        const string QUERY_BASE = 
            @"
            SELECT
	            tbl_movimentacao_passivo.*,
	            tbl_fundo.nome_reduzido AS nome_fundo,
	            tbl_investidor.nome_investidor,
	            tbl_administrador.nome_administrador
            FROM
	            tbl_movimentacao_passivo
		        INNER JOIN tbl_fundo ON tbl_movimentacao_passivo.cod_fundo = tbl_fundo.id
                INNER JOIN tbl_investidor_distribuidor ON tbl_movimentacao_passivo.cod_investidor_distribuidor = tbl_investidor_distribuidor.id
		        INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
		        INNER JOIN tbl_administrador ON tbl_ordem_passivo.cod_administrador = tbl_administrador.id";

        Task<IEnumerable<MovimentacaoPassivoModel>> AddBulkAsync(List<MovimentacaoPassivoModel> item);

        Task<bool> DeleteByDataRefAsync(DateTime dataMovimentacao);

        Task<IEnumerable<MovimentacaoPassivoViewModel>> GetByDataEntradaAsync(DateTime? dataMovimentacao);

        Task<IEnumerable<MovimentacaoPassivoViewModel>> GetAllAsync();
    }
}
