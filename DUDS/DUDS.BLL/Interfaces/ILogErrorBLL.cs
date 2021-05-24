using DUDS.MOD;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DUDS.BLL.Interfaces
{
    public interface ILogErrorBLL
    {
        Task<bool> CadastrarLogErroAsync(LogErrorMOD logErro);
    }
}
