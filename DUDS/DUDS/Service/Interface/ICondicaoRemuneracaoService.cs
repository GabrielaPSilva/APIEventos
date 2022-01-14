using DUDS.Models.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ICondicaoRemuneracaoService : IGenericOperationsService<CondicaoRemuneracaoModel>
    {
        const string QUERY_BASE = @"SELECT 
	                                    condicao_remuneracao.*,
	                                    fundo.nome_reduzido as nome_fundo
                                    FROM
	                                    tbl_condicao_remuneracao condicao_remuneracao
	                                        INNER JOIN tbl_fundo fundo ON fundo.id = condicao_remuneracao.cod_fundo
                                            INNER JOIN tbl_contrato_remuneracao contrato_remuneracao ON contrato_remuneracao.id = condicao_remuneracao.cod_contrato_remuneracao
                                            INNER JOIN tbl_contrato_fundo contrato_fundo ON contrato_fundo.id = contrato_remuneracao.cod_contrato_fundo
                                            INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.id = contrato_fundo.cod_sub_contrato
                                            INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato";

        Task<IEnumerable<CondicaoRemuneracaoViewModel>> GetContratoRemuneracaoByIdAsync(int id);
        Task<CondicaoRemuneracaoViewModel> GetByIdAsync(int id);
        Task<IEnumerable<CondicaoRemuneracaoViewModel>> GetAllAsync();
    }
}
