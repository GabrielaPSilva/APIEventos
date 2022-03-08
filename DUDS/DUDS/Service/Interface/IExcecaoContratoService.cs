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
	            tbl_fundo.NomeReduzido as NomeFundo,
				tbl_investidor_distribuidor.CodInvestidorDistribuidor,
				tbl_investidor_distribuidor.CodInvestAdministrador,
				tbl_contrato.CodContrato,
				tbl_contrato_alocador.CodContratoAlocador,
				tbl_contrato_fundo.CodContratoFundo

            FROM
				tbl_excecao_contrato
				INNER JOIN tbl_sub_contrato on tbl_sub_contrato.Id = tbl_excecao_contrato.CodSubContrato
	            INNER JOIN tbl_contrato on tbl_contrato.Id = tbl_sub_contrato.CodContrato
				INNER JOIN tbl_contrato_alocador on tbl_contrato_alocador.CodSubContrato = tbl_sub_contrato.Id AND tbl_contrato_alocador.CodInvestidor = tbl_excecao_contrato.CodInvestidor
				INNER JOIN tbl_contrato_fundo on tbl_contrato_fundo.CodSubContrato = tbl_sub_contrato.Id and tbl_contrato_fundo.CodFundo = tbl_excecao_contrato.CodFundo
				INNER JOIN tbl_investidor on tbl_investidor.Id = tbl_excecao_contrato.CodInvestidor 
				INNER JOIN tbl_investidor_distribuidor on tbl_investidor_distribuidor.CodInvestidor = tbl_investidor.Id
				INNER JOIN tbl_fundo on tbl_fundo.Id = tbl_excecao_contrato.CodFundo";

		Task<IEnumerable<ExcecaoContratoViewModel>> GetExcecaoContratoAsync(int? codContrato, int? codFundo, int? codInvestidor);

		Task<ExcecaoContratoViewModel> GetByIdAsync(int id);
	}
}
