using DUDS.Models.Contrato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ICondicaoRemuneracaoService : IGenericOperationsService<CondicaoRemuneracaoModel>
    {
        const string QUERY_BASE = @"SELECT 
	                                    tbl_condicao_remuneracao.*,
	                                    tbl_fundo.NomeReduzido as NomeFundo
                                    FROM
	                                    tbl_condicao_remuneracao
	                                        INNER JOIN tbl_fundo ON tbl_fundo.Id = tbl_condicao_remuneracao.CodFundo
                                            INNER JOIN tbl_contrato_remuneracao ON tbl_contrato_remuneracao.Id = tbl_condicao_remuneracao.CodContratoRemuneracao
                                            INNER JOIN tbl_contrato_fundo ON tbl_contrato_fundo.Id = tbl_contrato_remuneracao.CodContratoFundo
                                            INNER JOIN tbl_sub_contrato ON tbl_sub_contrato.Id = tbl_contrato_fundo.CodSubContrato
                                            INNER JOIN tbl_contrato ON tbl_contrato.Id = tbl_sub_contrato.CodContrato";

        Task<IEnumerable<CondicaoRemuneracaoViewModel>> GetContratoRemuneracaoByIdAsync(int id);
        Task<CondicaoRemuneracaoViewModel> GetByIdAsync(int id);
        Task<IEnumerable<CondicaoRemuneracaoViewModel>> GetAllAsync();
    }
}
