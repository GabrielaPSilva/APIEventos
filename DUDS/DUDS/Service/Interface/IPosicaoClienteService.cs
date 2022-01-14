using DUDS.Models;
using DUDS.Models.Passivo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPosicaoClienteService : IGenericOperationsService<PosicaoClienteModel>
    {
        const string QUERY_BASE = 
			@"
			SELECT
				tbl_posicao_cliente.*,
				tbl_fundo.nome_reduzido AS nome_fundo,
                tbl_investidor.nome_investidor,
	            adm_investidor.nome_administrador AS nome_administrador_investidor,
				tbl_gestor.nome_gestor,
	            tbl_distribuidor.nome_distribuidor,
	            tbl_administrador.nome_administrador AS nome_administrador_dados
            FROM
				tbl_posicao_cliente
	            INNER JOIN tbl_fundo ON tbl_posicao_cliente.cod_fundo = tbl_fundo.id
	            INNER JOIN tbl_tipo_estrategia ON tbl_fundo.cod_tipo_estrategia = tbl_tipo_estrategia.id
                INNER JOIN tbl_investidor_distribuidor ON tbl_posicao_cliente.cod_investidor_distribuidor = tbl_investidor_distribuidor.id
	            INNER JOIN tbl_administrador ON tbl_posicao_cliente.cod_administrador = tbl_administrador.id
                INNER JOIN tbl_distribuidor_administrador ON tbl_investidor_distribuidor.cod_distribuidor_administrador = tbl_distribuidor_administrador.id
	            INNER JOIN tbl_distribuidor ON tbl_distribuidor.id = tbl_distribuidor_administrador.cod_distribuidor
	            INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.cod_investidor = tbl_investidor.id
                LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id
	            LEFT JOIN tbl_administrador adm_investidor ON tbl_investidor.cod_administrador = adm_investidor.id";

        Task<IEnumerable<PosicaoClienteModel>> AddBulkAsync(List<PosicaoClienteModel> item);

        Task<bool> DeleteByDataRefAsync(DateTime dataRef);

        Task<IEnumerable<PosicaoClienteViewModel>> GetByParametersAsync(DateTime? dataInicio, DateTime? dataFim, int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor);

        Task<double?> GetMaxValorBrutoAsync(int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor);
    }
}
