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
            var codInvestidor = _config["RelacaoTabelas:tbl_investidor"].Split(',').Select(t => t.Trim()).ToArray();
            var codFundo = _config["RelacaoTabelas:tbl_fundo"].Split(',').Select(t => t.Trim()).ToArray();
            var codDistribuidor = _config["RelacaoTabelas:tbl_distribuidor"].Split(',').Select(t => t.Trim()).ToArray();
            var codAdministrador = _config["RelacaoTabelas:tbl_administrador"].Split(',').Select(t => t.Trim()).ToArray();
            var codGestor = _config["RelacaoTabelas:tbl_gestor"].Split(',').Select(t => t.Trim()).ToArray();
            var codContratoDistribuicao = _config["RelacaoTabelas:tbl_contrato_distribuicao"].Split(',').Select(t => t.Trim()).ToArray();
            var codTipoConta = _config["RelacaoTabelas:tbl_tipo_conta"].Split(',').Select(t => t.Trim()).ToArray();
            var codContrato = _config["RelacaoTabelas:tbl_contrato"].Split(',').Select(t => t.Trim()).ToArray();
            var codCustodiante = _config["RelacaoTabelas:tbl_custodiante"].Split(',').Select(t => t.Trim()).ToArray();

            var connection = _context.Database.GetDbConnection();
            var transaction = _context.Database.CurrentTransaction?.GetDbTransaction();
            var commandTimeout = _context.Database.GetCommandTimeout();

            bool retorno = false;

            try
            {
                var command = new CommandDefinition();

                if (tableName == "tbl_investidor")
                {
                    foreach (var investidor in codInvestidor)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT * FROM " + investidor + " WHERE cod_fundo = " + id);

                        using (var con = ConnectionFactory.Dahlia())
                        {
                            retorno = await con.QueryFirstOrDefaultAsync<int>(query.ToString(), new { investidor, id }) > 0;

                            if (retorno)
                            {
                                return retorno;
                                break;
                            }
                        }
                    }
                }
                else if (tableName == "tbl_fundo")
                {
                    foreach (var fundo in codFundo)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT * FROM " + fundo + " WHERE cod_fundo = " + id);

                        using (var con = ConnectionFactory.Dahlia())
                        {
                            retorno = await con.QueryFirstOrDefaultAsync<int>(query.ToString(), new { fundo, id }) > 0;

                            if (retorno)
                            {
                                return retorno;
                                break;
                            }
                        }
                    }
                }
                else if (tableName == "tbl_distribuidor")
                {
                    foreach (var distribuidor in codDistribuidor)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT * FROM " + distribuidor + " WHERE cod_fundo = " + id);

                        using (var con = ConnectionFactory.Dahlia())
                        {
                            retorno = await con.QueryFirstOrDefaultAsync<int>(query.ToString(), new { distribuidor, id }) > 0;

                            if (retorno)
                            {
                                return retorno;
                                break;
                            }
                        }
                    }
                }
                else if (tableName == "tbl_administrador")
                {
                    foreach (var admin in codAdministrador)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT * FROM " + admin + " WHERE cod_fundo = " + id);

                        using (var con = ConnectionFactory.Dahlia())
                        {
                            retorno = await con.QueryFirstOrDefaultAsync<int>(query.ToString(), new { admin, id }) > 0;

                            if (retorno)
                            {
                                return retorno;
                                break;
                            }
                        }
                    }
                }
                else if (tableName == "tbl_gestor")
                {
                    foreach (var gestor in codGestor)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT * FROM " + gestor + " WHERE cod_fundo = " + id);

                        using (var con = ConnectionFactory.Dahlia())
                        {
                            retorno = await con.QueryFirstOrDefaultAsync<int>(query.ToString(), new { gestor, id }) > 0;

                            if (retorno)
                            {
                                return retorno;
                                break;
                            }
                        }
                    }
                }
                else if (tableName == "tbl_contrato_distribuicao")
                {
                    foreach (var contratoDistribuicao in codContratoDistribuicao)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT * FROM " + contratoDistribuicao + " WHERE cod_fundo = " + id);

                        using (var con = ConnectionFactory.Dahlia())
                        {
                            retorno = await con.QueryFirstOrDefaultAsync<int>(query.ToString(), new { contratoDistribuicao, id }) > 0;

                            if (retorno)
                            {
                                return retorno;
                                break;
                            }
                        }
                    }
                }
                else if (tableName == "tbl_tipo_conta")
                {
                    foreach (var tipoConta in codTipoConta)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT * FROM " + tipoConta + " WHERE cod_fundo = " + id);

                        using (var con = ConnectionFactory.Dahlia())
                        {
                            retorno = await con.QueryFirstOrDefaultAsync<int>(query.ToString(), new { tipoConta, id }) > 0;

                            if (retorno)
                            {
                                return retorno;
                                break;
                            }
                        }
                    }
                }
                else if (tableName == "tbl_contrato")
                {
                    foreach (var contrato in codContrato)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT * FROM " + contrato + " WHERE cod_fundo = " + id);

                        using (var con = ConnectionFactory.Dahlia())
                        {
                            retorno = await con.QueryFirstOrDefaultAsync<int>(query.ToString(), new { contrato, id }) > 0;

                            if (retorno)
                            {
                                return retorno;
                                break;
                            }
                        }
                    }
                }
                else if (tableName == "tbl_custodiante")
                {
                    foreach (var custodiante in codCustodiante)
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("SELECT * FROM " + custodiante + " WHERE cod_fundo = " + id);

                        using (var con = ConnectionFactory.Dahlia())
                        {
                            retorno = await con.QueryFirstOrDefaultAsync<int>(query.ToString(), new { custodiante, id }) > 0;

                            if (retorno)
                            {
                                return retorno;
                                break;
                            }
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
