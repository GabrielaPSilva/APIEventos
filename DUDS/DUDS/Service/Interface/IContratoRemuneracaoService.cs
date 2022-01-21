using DUDS.Models.Contrato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContratoRemuneracaoService : IGenericOperationsService<ContratoRemuneracaoModel>
    {
        const string QUERIY_BASE = @"SELECT 
	                                    tbl_contrato_remuneração.*
                                     FROM
	                                    tbl_contrato_remuneração
                                        INNER JOIN tbl_contrato_fundo ON tbl_contrato_fundo.Id = tbl_contrato_remuneração.CodContratoFundo
                                        INNER JOIN tbl_sub_contrato ON tbl_sub_contrato.id = tbl_contrato_fundo.CodSubContrato
                                        INNER JOIN tbl_contrato ON tbl_contrato.Id = tbl_sub_contrato.CodContrato";

        Task<IEnumerable<ContratoRemuneracaoViewModel>> GetContratoRemuneracaoByContratoFundoAsync(int id);

        Task<ContratoRemuneracaoViewModel> GetByIdAsync(int id);

        Task<IEnumerable<ContratoRemuneracaoViewModel>> GetAllAsync();
    }
}
