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
	            tbl_administrador.nome_administrador AS NomeAdministrador,
	            tbl_gestor.nome_gestor AS NomeGestor
            FROM
	            tbl_investidor
                LEFT JOIN tbl_administrador ON tbl_investidor.cod_administrador = tbl_administrador.id
		        LEFT JOIN tbl_gestor ON tbl_investidor.cod_gestor = tbl_gestor.id";

        Task<InvestidorViewModel> GetInvestidorExistsBase(string cnpj);
        
        //Task<IEnumerable<InvestidorViewModel>> GetInvestidorByDataCriacao(DateTime dataCriacao);
        
        Task<IEnumerable<InvestidorModel>> AddInvestidores(List<InvestidorModel> investidores);
    }
}
