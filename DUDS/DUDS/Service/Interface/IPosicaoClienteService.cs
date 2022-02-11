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
				tbl_fundo.NomeReduzido AS NomeFundo,
                tbl_investidor.NomeInvestidor,
	            adm_investidor.NomeAdministrador AS NomeAdministradorInvestidor,
				tbl_gestor.NomeGestor,
	            tbl_distribuidor.NomeDistribuidor,
	            tbl_administrador.NomeAdministrador AS NomeAdministradorDados
            FROM
				tbl_posicao_cliente
	            INNER JOIN tbl_fundo ON tbl_posicao_cliente.CodFundo = tbl_fundo.Id
	            INNER JOIN tbl_tipo_estrategia ON tbl_fundo.CodTipoEstrategia = tbl_tipo_estrategia.Id
                INNER JOIN tbl_investidor_distribuidor ON tbl_posicao_cliente.CodInvestidorDistribuidor = tbl_investidor_distribuidor.Id
	            INNER JOIN tbl_administrador ON tbl_posicao_cliente.CodAdministrador = tbl_administrador.Id
                INNER JOIN tbl_distribuidor_administrador ON tbl_investidor_distribuidor.CodDistribuidorAdministrador = tbl_distribuidor_administrador.Id
	            INNER JOIN tbl_distribuidor ON tbl_distribuidor.Id = tbl_distribuidor_administrador.CodDistribuidor
	            INNER JOIN tbl_investidor ON tbl_investidor_distribuidor.CodInvestidor = tbl_investidor.Id
                LEFT JOIN tbl_gestor ON tbl_investidor.CodGestor = tbl_gestor.Id
	            LEFT JOIN tbl_administrador adm_investidor ON tbl_investidor.CodAdministrador = adm_investidor.Id";

		Task<IEnumerable<PosicaoClienteModel>> AddBulkAsync(List<PosicaoClienteModel> item);

        Task<bool> DeleteByDataRefAsync(DateTime dataRef);

        Task<IEnumerable<PosicaoClienteViewModel>> GetByParametersAsync(DateTime? dataInicio, DateTime? dataFim, int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor);

		Task<double> GetMaxValorBrutoAsync(DateTime dataPosicao, int? codDistribuidor, int? codGestor, int? codInvestidorDistribuidor);

		Task<int> GetCountByDataRefAsync(DateTime dataRef);

	}
}
