using DUDS.Models;
using DUDS.Models.Conta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IContaService : IGenericOperationsService<ContaModel>
    {
        const string QUERY_BASE = @"SELECT
										tbl_contas.*,
										tbl_fundo.NomeReduzido,
										tbl_investidor.NomeInvestidor,
										tbl_tipo_conta.TipoConta
									FROM
										tbl_contas
											LEFT JOIN tbl_fundo ON tbl_contas.CodFundo = tbl_fundo.Id
											LEFT JOIN tbl_investidor ON tbl_contas.CodInvestidor = tbl_investidor.Id
											INNER JOIN tbl_tipo_conta ON tbl_contas.CodTipoConta = tbl_tipo_conta.Id";

		Task<IEnumerable<ContaViewModel>> GetAllAsync();
		Task<ContaViewModel> GetByIdAsync(int id);
		Task<ContaViewModel> GetContaExistsBase(int codFundo, int codInvestidor, int codTipoConta);
		Task<IEnumerable<ContaViewModel>> GetFundoByIdAsync(int id);
    }
}
