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
	            contrato_fundo.*,
                fundo.nome_reduzido as nome_fundo,
                tipo_condicao.tipo_condicao
            FROM
	            tbl_contrato_fundo contrato_fundo
                INNER JOIN tbl_fundo fundo ON fundo.id = contrato_fundo.cod_fundo
                INNER JOIN tbl_tipo_condicao tipo_condicao ON tipo_condicao.id = contrato_fundo.cod_tipo_condicao
                INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrado.id = contrato_fundo.cod_sub_contrato
                INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato";

        Task<IEnumerable<ContratoFundoViewModel>> GetContratoFundoBySubContratoAsync(int id);

        Task<ContratoFundoViewModel> GetByIdAsync(int id);

        Task<IEnumerable<ContratoFundoViewModel>> GetAllAsync();
    }
}
