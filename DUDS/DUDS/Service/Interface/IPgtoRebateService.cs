using DUDS.Models.Rebate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPgtoRebateService : IGenericOperationsService<PgtoRebateModel>
    {
        const string QUERY_INSERT_PFEE_GESTOR = @"";

        const string QUERY_INSERT_PFEE_IVESTIDOR = @"";

        const string QUERY_INSERT_ADM_GESTOR = @"";

        const string QUERY_INSERT_ADM_INVESTIDOR = @"
            WITH calculo_pgto(CodPgtoAdmPfee, Competencia, CodInvestidorDistribuidor, CodGrupoRebate, CodTipoContrato, CodInvestidor, NomeInvestidor, 
					            CnpjInvestidor, CodGestorInvestidor, GestorInvestidor, CnpjGestorInvestidor, CodFundo, NomeFundo, CnpjFundo, CodGestorFundo, 
					            GestorFundo, CnpjGestorFundo, CodDistribuidor, NomeDistribuidor, CnpjDistribuidor, RebateAdm, RowNum, SumRebateAdm) AS 
            (
               SELECT
                  tbl_calculo_pgto_adm_pfee.CodPgtoAdmPfee,
                  tbl_pgto_adm_pfee.Competencia,
	              tbl_investidor_distribuidor.Id AS CodInvestidorDistribuidor,
                  tbl_investidor_distribuidor.CodGrupoRebate,
                  tbl_investidor_distribuidor.CodTipoContrato,
                  tbl_investidor.Id as CodInvestidor,
	              tbl_investidor.NomeInvestidor,
                  tbl_investidor.Cnpj AS CnpjInvestidor,
	              gestor_investidor.Id As CodGestorInvestidor,
                  gestor_investidor.NomeGestor AS GestorInvestidor,
                  gestor_investidor.Cnpj AS CnpjGestorInvestidor,
                  tbl_fundo.Id AS CodFundo,
                  tbl_fundo.Mnemonico AS NomeFundo,
                  tbl_fundo.Cnpj AS CnpjFundo,
	              gestor_fundo.Id AS CodGestorFundo,
                  gestor_fundo.NomeGestor AS GestorFundo,
                  gestor_fundo.Cnpj AS CnpjGestorFundo,
	              tbl_distribuidor.Id AS CodDistribuidor,
	              tbl_distribuidor.NomeDistribuidor,
	              tbl_distribuidor.Cnpj AS CnpjDistribuidor,
                  tbl_calculo_pgto_adm_pfee.RebateAdm,
                  ROW_NUMBER() OVER ( 
               ORDER BY
	              tbl_investidor_distribuidor.CodGrupoRebate,
	              tbl_investidor_distribuidor.CodTipoContrato,
	              tbl_fundo.Id ASC,
                  tbl_calculo_pgto_adm_pfee.RebateAdm DESC),
                  SUM(tbl_calculo_pgto_adm_pfee.RebateAdm) OVER ( 
               ORDER BY
                  tbl_investidor_distribuidor.CodGrupoRebate,
	              tbl_investidor_distribuidor.CodTipoContrato,
	              tbl_fundo.Id ASC,
                  tbl_calculo_pgto_adm_pfee.RebateAdm DESC RANGE UNBOUNDED PRECEDING ) 
               FROM
                  tbl_calculo_pgto_adm_pfee 
                  INNER JOIN
                     tbl_pgto_adm_pfee 
                     ON tbl_pgto_adm_pfee.Id = tbl_calculo_pgto_adm_pfee.CodPgtoAdmPfee 
                  INNER JOIN
                     tbl_investidor_distribuidor 
                     ON tbl_investidor_distribuidor.Id = tbl_pgto_adm_pfee.CodInvestidorDistribuidor 
                  INNER JOIN
                     tbl_grupo_rebate 
                     ON tbl_grupo_rebate.Id = tbl_investidor_distribuidor.CodGrupoRebate 
                  INNER JOIN
                     tbl_tipo_contrato 
                     ON tbl_tipo_contrato.Id = tbl_investidor_distribuidor.CodTipoContrato 
                  INNER JOIN
                     tbl_distribuidor_administrador 
                     ON tbl_distribuidor_administrador.Id = tbl_investidor_distribuidor.CodDistribuidorAdministrador
	              INNER JOIN
		             tbl_distribuidor
		             ON tbl_distribuidor.Id = tbl_distribuidor_administrador.CodDistribuidor
                  INNER JOIN
                     tbl_fundo 
                     ON tbl_fundo.Id = tbl_pgto_adm_pfee.CodFundo 
                  INNER JOIN
                     tbl_gestor gestor_fundo 
                     ON gestor_fundo.Id = tbl_fundo.CodGestor 
                  INNER JOIN
                     tbl_investidor 
                     ON tbl_investidor.Id = tbl_investidor_distribuidor.CodInvestidor 
                  LEFT JOIN
                     tbl_gestor gestor_investidor 
                     ON gestor_investidor.Id = tbl_investidor.CodGestor 
               WHERE
                  tbl_pgto_adm_pfee.Competencia = @Competencia
            ),
            pre_pgto AS (
            SELECT
               calculo_pgto.*,
               tbl_pagamento_servico.SaldoGestor 
            FROM
               calculo_pgto 
               INNER JOIN
                  tbl_pagamento_servico 
                  ON tbl_pagamento_servico.CodFundo = calculo_pgto.CodFundo 
                  AND tbl_pagamento_servico.Competencia = calculo_pgto.Competencia 
            WHERE
               calculo_pgto.SumRebateAdm <= tbl_pagamento_servico.SaldoGestor
               AND (@CodGrupoRebate IS NULL OR calculo_pgto.CodGrupoRebate = @CodGrupoRebate)
            )
            INSERT INTO tbl_pgto_rebate(DataAgendamento,CodFundo,CodTipoContrato,ValorBruto,CodDadosFavorecido,SourceFavorecido,Competencia,UsuarioCriacao)
            SELECT
	            @DataAgendamento AS DataAgendamento,
	            pre_pgto.CodFundo,
	            pre_pgto.CodTipoContrato,
	            SUM(pre_pgto.RebateAdm) AS ValorBruto,
	            CASE
		            WHEN pre_pgto.CodTipoContrato = 1 THEN pre_pgto.CodDistribuidor
		            WHEN pre_pgto.CodTipoContrato = 2 THEN pre_pgto.CodGestorInvestidor
		            WHEN pre_pgto.CodTipoContrato = 3 THEN pre_pgto.CodInvestidor
	            END AS CodDadosFavorecido,
	            CASE
		            WHEN pre_pgto.CodTipoContrato = 1 THEN 'tbl_distribuidor'
		            WHEN pre_pgto.CodTipoContrato = 2 THEN 'tbl_gestor'
		            WHEN pre_pgto.CodTipoContrato = 3 THEN 'tbl_investidor'
	            END AS SourceFavorecido,
	            pre_pgto.Competencia,
	            @UsuarioCriacao AS UsuarioCriacao
            FROM
	            pre_pgto
            GROUP BY
	            pre_pgto.CodFundo,
	            pre_pgto.CodTipoContrato,
	            CASE
		            WHEN pre_pgto.CodTipoContrato = 1 THEN pre_pgto.CodDistribuidor
		            WHEN pre_pgto.CodTipoContrato = 2 THEN pre_pgto.CodGestorInvestidor
		            WHEN pre_pgto.CodTipoContrato = 3 THEN pre_pgto.CodInvestidor
	            END,
	            CASE
		            WHEN pre_pgto.CodTipoContrato = 1 THEN 'tbl_distribuidor'
		            WHEN pre_pgto.CodTipoContrato = 2 THEN 'tbl_gestor'
		            WHEN pre_pgto.CodTipoContrato = 3 THEN 'tbl_investidor'
	            END,
	            pre_pgto.Competencia";

        const string QUERY_ARQUIVO_PGTO = @"
            SELECT
	            tbl_pgto_rebate.DataAgendamento,
	            tbl_fundo.Mnemonico AS CodFundo,
	            CASE
		            WHEN tbl_pgto_rebate.CodTipoContrato = 1 THEN 1
		            WHEN tbl_pgto_rebate.CodTipoContrato = 2 THEN 2
		            WHEN tbl_pgto_rebate.CodTipoContrato = 3 THEN 1243
	            END AS TipoDespesa,
	            tbl_pgto_rebate.ValorBruto,
	            CASE
		            WHEN tbl_pgto_rebate.SourceFavorecido = 'tbl_gestor' THEN (SELECT tbl_gestor.Cnpj FROM tbl_gestor WHERE tbl_gestor.Id = tbl_pgto_rebate.CodDadosFavorecido)
		            WHEN tbl_pgto_rebate.SourceFavorecido = 'tbl_distribuidor' THEN (SELECT tbl_distribuidor.Cnpj FROM tbl_distribuidor WHERE tbl_distribuidor.Id = tbl_pgto_rebate.CodDadosFavorecido)
		            WHEN tbl_pgto_rebate.SourceFavorecido = 'tbl_investidor' THEN (SELECT tbl_investidor.Cnpj FROM tbl_investidor WHERE tbl_investidor.Id = tbl_pgto_rebate.CodDadosFavorecido)
	            END AS CnpjCpfFavorecido,
	            CASE
		            WHEN tbl_pgto_rebate.SourceFavorecido = 'tbl_gestor' THEN (SELECT tbl_gestor.NomeGestor FROM tbl_gestor WHERE tbl_gestor.Id = tbl_pgto_rebate.CodDadosFavorecido)
		            WHEN tbl_pgto_rebate.SourceFavorecido = 'tbl_distribuidor' THEN (SELECT tbl_distribuidor.NomeDistribuidor FROM tbl_distribuidor WHERE tbl_distribuidor.Id = tbl_pgto_rebate.CodDadosFavorecido)
		            WHEN tbl_pgto_rebate.SourceFavorecido = 'tbl_investidor' THEN (SELECT tbl_investidor.NomeInvestidor FROM tbl_investidor WHERE tbl_investidor.Id = tbl_pgto_rebate.CodDadosFavorecido)
	            END AS NomeFavorecido,
	            RIGHT(tbl_pgto_rebate.Competencia,2) AS MesCompetencia,
	            LEFT(tbl_pgto_rebate.Competencia,4) AS AnoCompetencia,
	            tbl_pgto_rebate.Observacao,
	            CASE
		            WHEN tbl_pgto_rebate.CodTipoContrato = 3 THEN (SELECT tbl_investidor.Cnpj FROM tbl_investidor WHERE tbl_investidor.Id = tbl_pgto_rebate.CodDadosFavorecido)
		            ELSE NULL
	            END AS CnpjFundoInvestidor
            FROM 
	            tbl_pgto_rebate
	            INNER JOIN tbl_fundo ON tbl_fundo.Id = tbl_pgto_rebate.CodFundo";

		const string QUERY_BASE = @"
            SELECT 
	            tbl_pgto_rebate.*,
	            tbl_fundo.NomeReduzido,
	            tbl_tipo_contrato.TipoContrato
            FROM
	            tbl_pgto_rebate 
		            INNER JOIN tbl_fundo ON tbl_pgto_rebate.CodFundo = tbl_fundo.Id
		            INNER JOIN tbl_tipo_contrato ON tbl_pgto_rebate.CodTipoContrato = tbl_tipo_contrato.Id";


		Task<IEnumerable<PgtoRebateViewModel>> GetPgtoRebateByCompetencia(string competencia);
        Task<IEnumerable<PgtoRebateModel>> GetPgtoRebateById(Guid Id);
        Task<IEnumerable<PgtoRebateViewModel>> GetValidaErrosPagamento(string competencia);
        Task<bool> DeleteByCompetenciaAsync(string competencia);
    }
}
