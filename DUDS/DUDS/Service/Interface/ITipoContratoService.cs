using DUDS.Models.Tipos;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ITipoContratoService : IGenericOperationsService<TipoContratoModel>
    {
        Task<TipoContratoModel> GetTipoContaExistsBase(string tipoContrato);
    }
}
