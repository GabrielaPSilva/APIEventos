using DUDS.Models.Contrato;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContratoAlocadorService : IGenericOperationsService<ContratoAlocadorModel>
    {
        const string QUERY_BASE = @"SELECT 
	                            tbl_contrato_alocador.*,
                                investidor.NomeInvestidor
                             FROM
	                            tbl_contrato_alocador 
                                INNER JOIN tbl_investidor ON tbl_investidor.Id = tbl_contrato_alocador.CodInvestidor
                                INNER JOIN tbl_sub_contrato ON tbl_sub_contrato.Id = tbl_contrato_alocador.CodSubContrato
                                INNER JOIN tbl_contrato ON tbl_contrato.Id = tbl_sub_contrato.CodContrato";

        Task<IEnumerable<ContratoAlocadorViewModel>> GetContratoAlocadorByCodSubContratoAsync(int id);

        Task<ContratoAlocadorViewModel> GetByIdAsync(int id);

        Task<IEnumerable<ContratoAlocadorViewModel>> GetAllAsync();
    }
}
