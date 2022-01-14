using DUDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface ITipoContratoService : IGenericOperationsService<TipoContratoModel>
    {
        const string QUERY_BASE = @"SELECT
                                 *
                              FROM
	                             tbl_tipo_contrato";

        Task<TipoContratoModel> GetTipoContaExistsBase(string tipoContrato);
        Task<IEnumerable<TipoContratoModel>> GetAllAsync();
    }
}
