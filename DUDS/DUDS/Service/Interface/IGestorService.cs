using DUDS.Models.Gestor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IGestorService : IGenericOperationsService<GestorModel>
    {
        const string QUERY_BASE = @"SELECT
                                        gestor.*,
                                        tipo_classificacao.classificacao as Classificacao
                                    FROM
	                                    tbl_gestor gestor
                                            INNER JOIN tbl_tipo_classificacao tipo_classificacao 
                                            ON gestor.cod_tipo_classificacao = tipo_classificacao.id";

        Task<IEnumerable<GestorViewModel>> GetAllAsync();
        Task<GestorViewModel> GetByIdAsync(int id);
        Task<GestorViewModel> GetGestorExistsBase(string cnpj);
    }
}
