using DUDS.Models;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ILogErrosService
    {
        Task<LogErrosModel> GetLogErroById(int id);
        Task<bool> AddLogErro(LogErrosModel log);
        
    }
}
