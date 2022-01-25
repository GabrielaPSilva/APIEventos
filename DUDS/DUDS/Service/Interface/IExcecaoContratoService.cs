using DUDS.Models.Contrato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IExcecaoContratoService : IGenericOperationsService<ExcecaoContratoModel>
    {
		const string QUERY_BASE =
			@"SELECT 
	            tbl_excecao_contrato.*,
	            tbl_investidor.NomeInvestidor,
	            tbl_fundo.NomeReduzido as NomeFundo
            FROM
				tbl_excecao_contrato
	            INNER JOIN tbl_contrato on tbl_contrato.Id = tbl_excecao_contrato.CodContrato
				INNER JOIN tbl_investidor_distribuidor on tbl_investidor_distribuidor.Id = tbl_excecao_contrato.CodInvestidorDistribuidor
				INNER JOIN tbl_investidor on tbl_investidor.Id = tbl_investidor_distribuidor.CodInvestidor
				INNER JOIN tbl_fundo on tbl_fundo.Id = tbl_excecao_contrato.CodFundo";

		Task<IEnumerable<ExcecaoContratoViewModel>> GetExcecaoContratoAsync(int codInvestidorDistribuidor, int codFundo, int codContrato);

		Task<ExcecaoContratoViewModel> GetByIdAsync(int id);
	}
}
