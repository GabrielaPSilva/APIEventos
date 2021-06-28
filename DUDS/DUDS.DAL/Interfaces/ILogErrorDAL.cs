using DUDS.MOD;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DUDS.DAL.Interfaces
{
    public interface ILogErrorDAL
    {
        Task<bool> CadastrarLogErroAsync(LogErrorMOD logErro);
    }
}
