using DUDS.Models.Gestor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IGestorService : IGenericOperationsService<GestorModel>
    {
        const string QUERY_BASE = @"SELECT
                                        tbl_gestor.*,
                                        tbl_tipo_classificacao.Classificacao
                                    FROM
	                                    tbl_gestor
                                            INNER JOIN tbl_tipo_classificacao 
                                            ON tbl_gestor.CodTipoClassificacao = tbl_tipo_classificacao.Id";

        Task<IEnumerable<GestorViewModel>> GetAllAsync();
        Task<GestorViewModel> GetByIdAsync(int id);
        Task<GestorViewModel> GetGestorExistsBase(string cnpj);
    }
}
