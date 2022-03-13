using DUDS.Models.Rebate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DUDS.Service.Interface
{
    public interface IPgtoRebateService : IGenericOperationsService<PgtoRebateModel>
    {
        const string QUERY_INSERT_PFEE_GESTOR = @"
			INSERT INTO tbl_controle_pgto_rebate(DataAgendamento,CodFundo,CodTipoContrato,ValorBruto,CodDadosFavorecido,SourceFavorecido,Competencia,TipoPgto,UsuarioCriacao)
			SELECT
				@DataAgendamento AS DataAgendamento,
				tbl_pgto_adm_pfee.CodFundo,
				2 AS CodTipoContrato,
				SUM(tbl_pgto_adm_pfee.TaxaPerformanceResgate - ISNULL(tbl_calculo_pgto_adm_pfee.RebatePfeeResgate,0)) + 
					CASE WHEN RIGHT(tbl_pgto_adm_pfee.Competencia,2) = '06' OR RIGHT(tbl_pgto_adm_pfee.Competencia,2) = '12' 
						THEN SUM(tbl_pgto_adm_pfee.TaxaPerformanceApropriada - ISNULL(tbl_calculo_pgto_adm_pfee.RebatePfeeSemestre,0)) 
						ELSE 0 
					END AS ValorBruto,
				tbl_fundo.CodGestor AS CodDadosFavorecido,
				'tbl_gestor' AS SourceFavorecido,
				tbl_pgto_adm_pfee.Competencia,
				'P' AS TipoPgto,
				@UsuarioCriacao AS UsuarioCriacao
			FROM
				tbl_pgto_adm_pfee	 
				LEFT JOIN tbl_calculo_pgto_adm_pfee ON tbl_pgto_adm_pfee.Id = tbl_calculo_pgto_adm_pfee.CodPgtoAdmPfee 
				INNER JOIN tbl_fundo ON tbl_fundo.Id = tbl_pgto_adm_pfee.CodFundo 
				INNER JOIN tbl_gestor gestor_fundo ON gestor_fundo.Id = tbl_fundo.CodGestor 
			WHERE
				tbl_pgto_adm_pfee.Competencia = @Competencia
				AND tbl_pgto_adm_pfee.TaxaPerformanceApropriada <> 0
				AND tbl_pgto_adm_pfee.TaxaPerformanceResgate <> 0
			GROUP BY
				tbl_pgto_adm_pfee.CodFundo,
				tbl_pgto_adm_pfee.Competencia,
				tbl_fundo.CodGestor";

        const string QUERY_INSERT_PFEE_IVESTIDOR = @"
			INSERT INTO tbl_controle_pgto_rebate(DataAgendamento,CodFundo,CodTipoContrato,ValorBruto,CodDadosFavorecido,SourceFavorecido,Competencia,TipoPgto,UsuarioCriacao)
			SELECT
				@DataAgendamento AS DataAgendamento,
				tbl_pgto_adm_pfee.CodFundo,
				tbl_investidor_distribuidor.CodTipoContrato,
				SUM(tbl_calculo_pgto_adm_pfee.RebatePfeeResgate) + 
				CASE WHEN RIGHT(tbl_pgto_adm_pfee.Competencia,2) = '06' OR RIGHT(tbl_pgto_adm_pfee.Competencia,2) = '12' 
				THEN SUM(tbl_calculo_pgto_adm_pfee.RebatePfeeSemestre) 
				ELSE 0 END AS ValorBruto,
				CASE
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 1 THEN tbl_distribuidor.Id
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 2 THEN gestor_investidor.Id
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 3 THEN tbl_investidor.Id
				END AS CodDadosFavorecido,
				CASE
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 1 THEN 'tbl_distribuidor'
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 2 THEN 'tbl_gestor'
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 3 THEN 'tbl_investidor'
				END AS SourceFavorecido,
				tbl_pgto_adm_pfee.Competencia,
				'P' AS TipoPgto,
				@UsuarioCriacao AS UsuarioCriacao
			FROM
				tbl_calculo_pgto_adm_pfee 
				INNER JOIN tbl_pgto_adm_pfee ON tbl_pgto_adm_pfee.Id = tbl_calculo_pgto_adm_pfee.CodPgtoAdmPfee 
				INNER JOIN tbl_investidor_distribuidor ON tbl_investidor_distribuidor.Id = tbl_pgto_adm_pfee.CodInvestidorDistribuidor 
				INNER JOIN tbl_grupo_rebate ON tbl_grupo_rebate.Id = tbl_investidor_distribuidor.CodGrupoRebate 
				INNER JOIN tbl_tipo_contrato ON tbl_tipo_contrato.Id = tbl_investidor_distribuidor.CodTipoContrato 
				INNER JOIN tbl_distribuidor_administrador ON tbl_distribuidor_administrador.Id = tbl_investidor_distribuidor.CodDistribuidorAdministrador
				INNER JOIN tbl_distribuidor ON tbl_distribuidor.Id = tbl_distribuidor_administrador.CodDistribuidor
				INNER JOIN tbl_fundo ON tbl_fundo.Id = tbl_pgto_adm_pfee.CodFundo 
				INNER JOIN tbl_gestor gestor_fundo ON gestor_fundo.Id = tbl_fundo.CodGestor 
				INNER JOIN tbl_investidor ON tbl_investidor.Id = tbl_investidor_distribuidor.CodInvestidor 
				LEFT JOIN tbl_gestor gestor_investidor ON gestor_investidor.Id = tbl_investidor.CodGestor 
			WHERE
				tbl_pgto_adm_pfee.Competencia = @Competencia
				AND tbl_calculo_pgto_adm_pfee.RebatePfeeResgate <> 0
				AND tbl_calculo_pgto_adm_pfee.RebatePfeeSemestre <> 0
			GROUP BY
				tbl_pgto_adm_pfee.CodFundo,
				tbl_investidor_distribuidor.CodTipoContrato,
				CASE
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 1 THEN tbl_distribuidor.Id
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 2 THEN gestor_investidor.Id
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 3 THEN tbl_investidor.Id
				END,
				CASE
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 1 THEN 'tbl_distribuidor'
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 2 THEN 'tbl_gestor'
					WHEN tbl_investidor_distribuidor.CodTipoContrato = 3 THEN 'tbl_investidor'
				END,
				tbl_pgto_adm_pfee.Competencia";

        const string QUERY_INSERT_ADM_GESTOR = @"
            INSERT INTO tbl_controle_pgto_rebate(DataAgendamento,CodFundo,CodTipoContrato,ValorBruto,CodDadosFavorecido,SourceFavorecido,Competencia,TipoPgto,UsuarioCriacao)
			SELECT
				@DataAgendamento AS DataAgendamento,
				tbl_fundo.Id AS CodFundo,
				2 AS CodTipoContrato,
				tbl_pagamento_servico.SaldoGestor - ISNULL(SUM(tbl_controle_pgto_rebate.ValorBruto),0) AS ValorBruto,
				tbl_fundo.CodGestor AS CodDadosFavorecido,
				'tbl_gestor' AS SourceFavorecido,
				@Competencia AS Competencia,
				'A' AS TipoPgto,
				@UsuarioCriacao AS UsuarioCriacao
			FROM
				tbl_pagamento_servico
				INNER JOIN tbl_fundo ON tbl_fundo.Id = tbl_pagamento_servico.CodFundo
				LEFT JOIN tbl_controle_pgto_rebate ON tbl_pagamento_servico.CodFundo = tbl_controle_pgto_rebate.CodFundo AND tbl_pagamento_servico.Competencia = tbl_controle_pgto_rebate.Competencia
			WHERE
				tbl_pagamento_servico.Competencia = @Competencia
				AND tbl_fundo.TipoFundo = 'FEEDER'
			GROUP BY
				tbl_fundo.Id,
				tbl_fundo.CodGestor,
				tbl_pagamento_servico.SaldoGestor";

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
            )
            INSERT INTO tbl_controle_pgto_rebate(DataAgendamento,CodFundo,CodTipoContrato,ValorBruto,CodDadosFavorecido,SourceFavorecido,Competencia,TipoPgto,UsuarioCriacao)
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
				'A' AS TipoPgto,
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
	            tbl_controle_pgto_rebate.DataAgendamento,
	            tbl_fundo.Mnemonico AS CodFundo,
	            CASE WHEN tbl_controle_pgto_rebate.TipoPgto = 'A' THEN
					CASE 
						WHEN tbl_controle_pgto_rebate.CodTipoContrato = 1 THEN 1
						WHEN tbl_controle_pgto_rebate.CodTipoContrato = 2 THEN 2
						WHEN tbl_controle_pgto_rebate.CodTipoContrato = 3 THEN 1243
					END
				ELSE
					CASE 
						WHEN tbl_controle_pgto_rebate.CodTipoContrato = 1 THEN 6
						WHEN tbl_controle_pgto_rebate.CodTipoContrato = 2 THEN 7
						WHEN tbl_controle_pgto_rebate.CodTipoContrato = 3 THEN 1244
					END
				AS TipoDespesa,
	            tbl_controle_pgto_rebate.ValorBruto,
	            CASE
		            WHEN tbl_controle_pgto_rebate.SourceFavorecido = 'tbl_gestor' THEN (SELECT tbl_gestor.Cnpj FROM tbl_gestor WHERE tbl_gestor.Id = tbl_controle_pgto_rebate.CodDadosFavorecido)
		            WHEN tbl_controle_pgto_rebate.SourceFavorecido = 'tbl_distribuidor' THEN (SELECT tbl_distribuidor.Cnpj FROM tbl_distribuidor WHERE tbl_distribuidor.Id = tbl_controle_pgto_rebate.CodDadosFavorecido)
		            WHEN tbl_controle_pgto_rebate.SourceFavorecido = 'tbl_investidor' THEN (SELECT tbl_investidor.Cnpj FROM tbl_investidor WHERE tbl_investidor.Id = tbl_controle_pgto_rebate.CodDadosFavorecido)
	            END AS CnpjCpfFavorecido,
	            CASE
		            WHEN tbl_controle_pgto_rebate.SourceFavorecido = 'tbl_gestor' THEN (SELECT tbl_gestor.NomeGestor FROM tbl_gestor WHERE tbl_gestor.Id = tbl_controle_pgto_rebate.CodDadosFavorecido)
		            WHEN tbl_controle_pgto_rebate.SourceFavorecido = 'tbl_distribuidor' THEN (SELECT tbl_distribuidor.NomeDistribuidor FROM tbl_distribuidor WHERE tbl_distribuidor.Id = tbl_controle_pgto_rebate.CodDadosFavorecido)
		            WHEN tbl_controle_pgto_rebate.SourceFavorecido = 'tbl_investidor' THEN (SELECT tbl_investidor.NomeInvestidor FROM tbl_investidor WHERE tbl_investidor.Id = tbl_controle_pgto_rebate.CodDadosFavorecido)
	            END AS NomeFavorecido,
	            RIGHT(tbl_controle_pgto_rebate.Competencia,2) AS MesCompetencia,
	            LEFT(tbl_controle_pgto_rebate.Competencia,4) AS AnoCompetencia,
	            tbl_controle_pgto_rebate.Observacao,
	            CASE
		            WHEN tbl_controle_pgto_rebate.CodTipoContrato = 3 THEN (SELECT tbl_investidor.Cnpj FROM tbl_investidor WHERE tbl_investidor.Id = tbl_controle_pgto_rebate.CodDadosFavorecido)
		            ELSE NULL
	            END AS CnpjFundoInvestidor
            FROM 
	            tbl_controle_pgto_rebate
	            INNER JOIN tbl_fundo ON tbl_fundo.Id = tbl_controle_pgto_rebate.CodFundo";

		const string QUERY_BASE = @"
            SELECT 
	            tbl_controle_pgto_rebate.*,
	            tbl_fundo.NomeReduzido,
	            tbl_tipo_contrato.TipoContrato
            FROM
	            tbl_controle_pgto_rebate 
		            INNER JOIN tbl_fundo ON tbl_controle_pgto_rebate.CodFundo = tbl_fundo.Id
		            INNER JOIN tbl_tipo_contrato ON tbl_controle_pgto_rebate.CodTipoContrato = tbl_tipo_contrato.Id";


		Task<IEnumerable<PgtoRebateViewModel>> GetPgtoRebateByCompetencia(string competencia);
        Task<IEnumerable<PgtoRebateModel>> GetPgtoRebateById(Guid Id);
        Task<IEnumerable<PgtoRebateModel>> GetValidaErrosPagamento(string competencia);
        Task<bool> DeleteByCompetenciaAsync(string competencia);
    }
}
