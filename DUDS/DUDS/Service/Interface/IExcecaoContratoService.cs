using DUDS.Models.Contrato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IExcecaoContratoService : IGenericOperationsService<ExcecaoContratoModel>
    {
		const string QUERY_BASE =
			@"
			SELECT
				tbl_excecao_contrato.*,
				tbl_fundo.NomeReduzido AS NomeFundo,
				tbl_investidor.NomeInvestidor,
				tbl_investidor_distribuidor.CodInvestidor,
				tbl_investidor_distribuidor.CodInvestAdministrador,
				tbl_sub_contrato.CodContrato
			FROM
				tbl_excecao_contrato
				INNER JOIN tbl_fundo ON tbl_fundo.Id = tbl_excecao_contrato.CodFundo
				INNER JOIN tbl_investidor_distribuidor ON tbl_investidor_distribuidor.Id = tbl_excecao_contrato.CodInvestidorDistribuidor
				INNER JOIN tbl_investidor ON tbl_investidor.Id = tbl_investidor_distribuidor.CodInvestidor
				INNER JOIN tbl_sub_contrato ON tbl_sub_contrato.Id = tbl_excecao_contrato.CodSubContrato";

		Task<IEnumerable<ExcecaoContratoViewModel>> GetExcecaoContratoAsync(int? codSubContrato, int? codFundo, int? codInvestidorDistribuidor);

		Task<ExcecaoContratoViewModel> GetByIdAsync(int id);
	}
}
