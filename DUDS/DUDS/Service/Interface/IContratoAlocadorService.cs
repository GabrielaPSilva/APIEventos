using DUDS.Models.Contrato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContratoAlocadorService : IGenericOperationsService<ContratoAlocadorModel>
    {
        const string QUERY_BASE = @"SELECT 
	                            contrato_alocador.*,
                                investidor.nome_investidor
                             FROM
	                            tbl_contrato_alocador contrato_alocador
                                INNER JOIN tbl_investidor investidor ON contrato.id = contrato_alocador.cod_investidor
                                INNER JOIN tbl_sub_contrato sub_contrato ON sub_contrato.id = contrato_alocador.cod_sub_contrato
                                INNER JOIN tbl_contrato contrato ON contrato.id = sub_contrato.cod_contrato";

        Task<IEnumerable<ContratoAlocadorViewModel>> GetContratoAlocadorByCodSubContratoAsync(int id);

        Task<ContratoAlocadorViewModel> GetByIdAsync(int id);

        Task<IEnumerable<ContratoAlocadorViewModel>> GetAllAsync();
    }
}
