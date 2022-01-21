using DUDS.Models;
using DUDS.Models.Investidor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IInvestidorService : IGenericOperationsService<InvestidorModel>
    {
        const string QUERY_BASE = 
            @"SELECT
	            tbl_investidor.*,
	            tbl_administrador.NomeAdministrador,
	            tbl_gestor.NomeGestor
            FROM
	            tbl_investidor
                LEFT JOIN tbl_administrador ON tbl_investidor.CodAdministrador = tbl_administrador.Id
		        LEFT JOIN tbl_gestor ON tbl_investidor.CodGestor = tbl_gestor.Id";

        Task<InvestidorViewModel> GetInvestidorExistsBase(string cnpj);
        
        //Task<IEnumerable<InvestidorViewModel>> GetInvestidorByDataCriacao(DateTime dataCriacao);
        
        Task<IEnumerable<InvestidorModel>> AddInvestidores(List<InvestidorModel> investidores);

        Task<InvestidorViewModel> GetByIdAsync(int id);

        Task<IEnumerable<InvestidorModel>> GetAllAsync();
    }
}
