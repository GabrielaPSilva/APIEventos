using DUDS.Models.Contrato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContratoRemuneracaoService : IGenericOperationsService<ContratoRemuneracaoModel>
    {
        const string QUERIY_BASE = @"SELECT 
	                                    contrato_remuneracao.*
                                     FROM
	                                    tbl_contrato_remuneração contrato_remuneracao
                                        INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.id = contrato_remuneracao.cod_contrato_fundo
                                        INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.id = contrato_fundo.cod_sub_contrato
                                        INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato";

        Task<IEnumerable<ContratoRemuneracaoViewModel>> GetContratoRemuneracaoByContratoFundoAsync(int id);

        Task<ContratoRemuneracaoViewModel> GetByIdAsync(int id);

        Task<IEnumerable<ContratoRemuneracaoViewModel>> GetAllAsync();
    }
}
