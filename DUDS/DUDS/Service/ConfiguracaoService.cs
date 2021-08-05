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
        private readonly DataContext _context;
        private IConfiguration _config;
        string Sistema = "DUDS";

        public ConfiguracaoService(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<bool> GetValidacaoExisteIdOutrasTabelas(int id, string tableName)
        {
            string[] codInvestidor = _config["RelacaoTabelas:tbl_investidor"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codFundo = _config["RelacaoTabelas:tbl_fundo"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codDistribuidor = _config["RelacaoTabelas:tbl_distribuidor"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codAdministrador = _config["RelacaoTabelas:tbl_administrador"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codGestor = _config["RelacaoTabelas:tbl_gestor"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codContratoDistribuicao = _config["RelacaoTabelas:tbl_contrato_distribuicao"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codTipoConta = _config["RelacaoTabelas:tbl_tipo_conta"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codContrato = _config["RelacaoTabelas:tbl_contrato"].Split(',').Select(t => t.Trim()).ToArray();
            string[] codCustodiante = _config["RelacaoTabelas:tbl_custodiante"].Split(',').Select(t => t.Trim()).ToArray();

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
                case "tbl_contrato_distribuicao":
                    tableUsed = codContratoDistribuicao;
                    codUsed = "cod_contrato_distribuicao = ";
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
                default:
                    break;
            }

            try
            {
                foreach (var item in tableUsed)
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT * FROM " + item + " WHERE " + codUsed + id);

                    using (var con = ConnectionFactory.Dahlia())
                    {
                        retorno = await con.QueryFirstOrDefaultAsync<int>(query.ToString(), new { item, id }) > 0;

                        if (retorno)
                        {
                            return retorno;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
            }

            return retorno;
        }
    }
}
