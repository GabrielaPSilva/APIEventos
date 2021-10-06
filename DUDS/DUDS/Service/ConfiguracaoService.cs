using Dapper;
using DUDS.Data;
using DUDS.Models;
using DUDS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUDS.Service
{
    public class ConfiguracaoService : IConfiguracaoService
    {
        private IConfiguration _config;
        string Sistema = "DUDS";

        public ConfiguracaoService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> GetValidacaoExisteIdOutrasTabelas(int id, string tableName)
        {
            string[] codInvestidor = _config["RelacaoTabelas:tbl_investidor"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codFundo = _config["RelacaoTabelas:tbl_fundo"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codDistribuidor = _config["RelacaoTabelas:tbl_distribuidor"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codAdministrador = _config["RelacaoTabelas:tbl_administrador"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codGestor = _config["RelacaoTabelas:tbl_gestor"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codTipoConta = _config["RelacaoTabelas:tbl_tipo_conta"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codContrato = _config["RelacaoTabelas:tbl_contrato"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codCustodiante = _config["RelacaoTabelas:tbl_custodiante"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codContratoRemuneracao = _config["RelacaoTabelas:tbl_contrato_remuneracao"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codSubContrato = _config["RelacaoTabelas:tbl_sub_contrato"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codContratoFundo = _config["RelacaoTabelas:tbl_contrato_fundo"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codInvestidorDistribuidor = _config["RelacaoTabelas:tbl_investidor_distribuidor"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codTipoCondicao = _config["RelacaoTabelas:tbl_tipo_condicao"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codTipoContrato = _config["RelacaoTabelas:tbl_tipo_contrato"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codTipoClassificacaoGestor = _config["RelacaoTabelas:tbl_tipo_classificacao_gestor"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codTipoEstrategia = _config["RelacaoTabelas:tbl_tipo_estrategia"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codCondicacaoRemuneracao = _config["RelacaoTabelas:tbl_condicao_remuneracao"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codGrupoRebate = _config["RelacaoTabelas:tbl_grupo_rebate"].Split(',').Select(t => t.Trim()).ToArray();

            string[] tableUsed = null;
            string codUsed = string.Empty;
            bool retorno = false;

            switch (tableName)
            {
                case "tbl_investidor":
                    tableUsed = codInvestidor;
                    codUsed = "cod_investidor = ";
                    break;
                case "tbl_fundo":
                    tableUsed = codFundo;
                    codUsed = "cod_fundo = ";
                    break;
                case "tbl_distribuidor":
                    tableUsed = codDistribuidor;
                    codUsed = "cod_distribuidor = ";
                    break;
                case "tbl_administrador":
                    tableUsed = codAdministrador;
                    codUsed = "cod_administrador = ";
                    break;
                case "tbl_gestor":
                    tableUsed = codGestor;
                    codUsed = "cod_gestor = ";
                    break;
                case "tbl_tipo_conta":
                    tableUsed = codTipoConta;
                    codUsed = "cod_tipo_conta = ";
                    break;
                case "tbl_contrato":
                    tableUsed = codContrato;
                    codUsed = "cod_contrato = ";
                    break;
                case "tbl_custodiante":
                    tableUsed = codCustodiante;
                    codUsed = "cod_custodiante = ";
                    break;
                case "tbl_contrato_remuneracao":
                    tableUsed = codContratoRemuneracao;
                    codUsed = "cod_contrato_remuneracao = ";
                    break;
                case "tbl_sub_contrato":
                    tableUsed = codSubContrato;
                    codUsed = "cod_sub_contrato = ";
                    break;
                case "tbl_contrato_fundo":
                    tableUsed = codContratoFundo;
                    codUsed = "cod_contrato_fundo = ";
                    break;
                case "tbl_investidor_distribuidor":
                    tableUsed = codInvestidorDistribuidor;
                    codUsed = "cod_investidor_distribuidor = ";
                    break;
                case "tbl_tipo_condicao":
                    tableUsed = codTipoCondicao;
                    codUsed = "cod_tipo_condicao = ";
                    break;
                case "tbl_tipo_contrato":
                    tableUsed = codTipoContrato;
                    codUsed = "cod_tipo_contrato = ";
                    break;
                case "tbl_tipo_classificacao_gestor":
                    tableUsed = codTipoClassificacaoGestor;
                    codUsed = "cod_tipo_classificacao_gestor = ";
                    break;
                case "tbl_tipo_estrategia":
                    tableUsed = codTipoEstrategia;
                    codUsed = "cod_tipo_estrategia = ";
                    break;
                case "tbl_condicao_remuneracao":
                    tableUsed = codCondicacaoRemuneracao;
                    codUsed = "cod_condicao_remuneracao = ";
                    break;
                case "tbl_grupo_rebate":
                    tableUsed = codGrupoRebate;
                    codUsed = "cod_grupo_rebate = ";
                    break;
                default:
                    break;
            }

            if(tableUsed != null)
            {
                try
                {
                    foreach (string item in tableUsed)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT * FROM " + item + " WHERE " + codUsed + id);

                        using (var connection = await SqlHelpers.ConnectionFactory.ConexaoAsync())
                        {
                            retorno = await connection.QueryFirstOrDefaultAsync<int>(query.ToString(), new { item, id }) > 0;

                            if (retorno)
                            {
                                return retorno;
                            }
                        }
                    }

                }
                catch (Exception e)
                {

                }
            }
            return retorno;
        }
    }
}
