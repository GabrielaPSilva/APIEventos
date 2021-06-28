using DUDS.BLL.Interfaces;
using DUDS.DAL.Interfaces;
using DUDS.MOD;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DUDS.BLL
{
    public class LogErrorBLL : ILogErrorBLL
    {
        private ILogErrorDAL _logErrorDAL;

        public LogErrorBLL(ILogErrorDAL logErrorDAL)
        {
            _logErrorDAL = logErrorDAL;
        }

        public async Task<bool> CadastrarLogErroAsync(LogErrorMOD logErro)
        {
            return await _logErrorDAL.CadastrarLogErroAsync(logErro);
        }
    }
}
