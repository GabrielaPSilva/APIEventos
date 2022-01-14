using DUDS.Models.Tipos;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ITipoEstrategiaService : IGenericOperationsService<TipoEstrategiaModel>
    {
        Task<TipoEstrategiaModel> GetTipoEstrategiaExistsBase(string estrategia);
    }
}
