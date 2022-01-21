using DUDS.Models;
using DUDS.Models.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContratoFundoService : IGenericOperationsService<ContratoFundoModel>
    {
        const string QUERY_BASE =
            @"SELECT 
	            tbl_contrato_fundo.*,
                fundo.NomeReduzido as NomeFundo,
                tipo_condicao.TipoCondicao
            FROM
	            tbl_contrato_fundo
                INNER JOIN tbl_fundo ON fundo.Id = tbl_contrato_fundo.CodFundo
                INNER JOIN tbl_tipo_condicao ON tbl_tipo_condicao.Id = tbl_contrato_fundo.CodTipoCondicao
                INNER JOIN tbl_sub_contrato ON tbl_sub_contrato.Id = tbl_contrato_fundo.CodSubContrato
                INNER JOIN tbl_contrato ON tbl_contrato.Id = tbl_sub_contrato.CodContrato";

        Task<IEnumerable<ContratoFundoViewModel>> GetContratoFundoBySubContratoAsync(int id);

        Task<ContratoFundoViewModel> GetByIdAsync(int id);

        Task<IEnumerable<ContratoFundoViewModel>> GetAllAsync();
    }
}
