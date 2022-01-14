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
										tbl_fundo.nome_reduzido,
										tbl_investidor.nome_investidor,
										tbl_tipo_conta.tipo_conta
									FROM
										tbl_contas
											LEFT JOIN tbl_fundo ON tbl_contas.cod_fundo = tbl_fundo.id
											LEFT JOIN tbl_investidor ON tbl_contas.cod_investidor = tbl_investidor.id
											INNER JOIN tbl_tipo_conta ON tbl_contas.cod_tipo_conta = tbl_tipo_conta.id";

		Task<IEnumerable<ContaViewModel>> GetAllAsync();
		Task<ContaViewModel> GetByIdAsync(int id);
		Task<ContaViewModel> GetContaExistsBase(int codFundo, int codInvestidor, int codTipoConta);
		Task<IEnumerable<ContaViewModel>> GetFundoByIdAsync(int id);
    }
}
