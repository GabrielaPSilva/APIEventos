using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using EFCore.BulkExtensions;
using System.Collections.Concurrent;

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly DataContext _context;

        public PagamentosController(DataContext context)
        {
            _context = context;
        }

        #region Pagamento Servico
        // GET: api/Pagamentos/GetPagamentoServico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> GetPagamentoServico()
        {
            try
            {
                List<TblPagamentoServico> pgtosServico = await _context.TblPagamentoServico.OrderByDescending(c => c.Competencia).AsNoTracking().ToListAsync();

                if (pgtosServico.Count == 0)
                {
                    return NotFound();
                }

                return Ok(pgtosServico);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Pagamentos/GetPagamentoServicoByIds/competencia/cod_fundo
        [HttpGet("{competencia}/{cod_fundo}")]
        public async Task<ActionResult<TblPagamentoServico>> GetPagamentoServicoByIds(string competencia, int cod_fundo)
        {
            try
            {
                TblPagamentoServico tblPagamentoServico = await _context.TblPagamentoServico.FindAsync(competencia, cod_fundo);
                if (tblPagamentoServico != null)
                {
                    return NotFound();
                }
                return Ok(tblPagamentoServico);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Pagamentos/AddPagamentoServico/List<PagamentoServicoModel>
        [HttpPost]
        public async Task<ActionResult<PagamentoServicoModel>> AddPagamentoServico(List<PagamentoServicoModel> tblPagamentoServicoModel)
        {
            List<TblPagamentoServico> listaPagamentosServico = new List<TblPagamentoServico>();
            TblPagamentoServico itensPagamentoServico = new TblPagamentoServico();

            try
            {
                foreach (var line in tblPagamentoServicoModel)
                {
                    itensPagamentoServico = new TblPagamentoServico
                    {
                        Competencia = line.Competencia,
                        CodFundo = line.CodFundo,
                        TaxaAdm = line.TaxaAdm,
                        AdmFiduciaria = line.AdmFiduciaria,
                        Servico = line.Servico,
                        SaldoParcial = line.SaldoParcial,
                        SaldoGestor = line.SaldoGestor
                    };

                    listaPagamentosServico.Add(itensPagamentoServico);
                }

                await _context.BulkInsertAsync(listaPagamentosServico);
                await _context.SaveChangesAsync();

                return Ok(itensPagamentoServico);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Pagamentos/DeletePagamentoServico/competencia
        [HttpDelete("{competencia}")]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> DeletePagamentoServico(string competencia)
        {
            IList<TblPagamentoServico> tblPagamentoServico = await _context.TblPagamentoServico.Where(c => c.Competencia == competencia).ToListAsync();

            if (tblPagamentoServico == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblPagamentoServico.RemoveRange(tblPagamentoServico);
                await _context.SaveChangesAsync();
                return Ok(tblPagamentoServico);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        #endregion

        #region Pagamento Admin Pfee
        // GET: api/Pagamentos/GetPgtoAdmPfee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPgtoAdmPfee>>> GetPgtoAdmPfee()
        {
            try
            {
                List<TblPgtoAdmPfee> pgtosAdmPfee = await _context.TblPgtoAdmPfee.OrderByDescending(c => c.Competencia).AsNoTracking().ToListAsync();

                if (pgtosAdmPfee.Count == 0)
                {
                    return NotFound();
                }

                return Ok(pgtosAdmPfee);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Pagamentos/GetPgtoAdmPfeeByIds/competencia/cod_investidor_distribuidor/cod_administrador/cod_fundo
        [HttpGet("{competencia}/{cod_investidor_distribuidor}/{cod_administrador}/{cod_fundo}")]
        public async Task<ActionResult<TblPgtoAdmPfee>> GetPgtoAdmPfeeByIds(string competencia, int cod_investidor_distribuidor, int cod_administrador, int cod_fundo)
        {
            try
            {
                TblPgtoAdmPfee tblPgtoAdmPfee = await _context.TblPgtoAdmPfee.FindAsync(competencia, cod_investidor_distribuidor, cod_administrador, cod_fundo);
                if (tblPgtoAdmPfee == null)
                {
                    return NotFound();
                }
                return Ok(tblPgtoAdmPfee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Pagamentos/AddPagamentoAdminPfee/List<PagamentoAdminPfeeModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<PagamentoAdminPfeeModel>>> AddPagamentoAdminPfee(List<PagamentoAdminPfeeModel> tblPagamentoAdminPfeeModel)
        {
            List<TblPgtoAdmPfee> listaPagamentosAdminPfee = new List<TblPgtoAdmPfee>();
            // TblPgtoAdmPfee itensPagamentoAdminPfee = new TblPgtoAdmPfee();

            try
            {
                foreach (var line in tblPagamentoAdminPfeeModel)
                {
                    TblPgtoAdmPfee itensPagamentoAdminPfee = new TblPgtoAdmPfee
                    {
                        Competencia = line.Competencia,
                        CodInvestidorDistribuidor = line.CodInvestidorDistribuidor,
                        CodAdministrador = line.CodAdministrador,
                        CodFundo = line.CodFundo,
                        TaxaPerformanceApropriada = line.TaxaPerformanceApropriada,
                        TaxaPerformanceResgate = line.TaxaPerformanceResgate,
                        TaxaAdministracao = line.TaxaAdministracao,
                        TaxaGestao = line.TaxaGestao
                    };

                    listaPagamentosAdminPfee.Add(itensPagamentoAdminPfee);
                }

                await _context.BulkInsertAsync(listaPagamentosAdminPfee);
                await _context.SaveChangesAsync();

                return Ok(listaPagamentosAdminPfee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Pagamentos/DeletePagamentoAdminPfee/competencia
        [HttpDelete("{competencia}")]
        public async Task<ActionResult<IEnumerable<TblPgtoAdmPfee>>> DeletePagamentoAdminPfee(string competencia)
        {
            IList<TblPgtoAdmPfee> tblPagamentoAdminPfee = await _context.TblPgtoAdmPfee.Where(c => c.Competencia == competencia).ToListAsync();

            if (tblPagamentoAdminPfee == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblPgtoAdmPfee.RemoveRange(tblPagamentoAdminPfee);
                await _context.SaveChangesAsync();
                return Ok(tblPagamentoAdminPfee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion

        #region Pagamento Admin Pfee Investidor
        // GET: api/Pagamentos/GetPgtoAdmPfeeInvestidor
        [HttpGet("{competencia}")]
        public async Task<ActionResult<IEnumerable<PagamentoAdmPfeeInvestidorModel>>> GetPgtoAdmPfeeInvestidor(string competencia)
        {
            try
            {
                var pgtosAdmPfee = await (from pgtoAdmPfee in _context.TblPgtoAdmPfee
                                          join investidorDistribuidor in _context.TblInvestidorDistribuidor on pgtoAdmPfee.CodInvestidorDistribuidor equals investidorDistribuidor.Id
                                          join investidor in _context.TblInvestidor on investidorDistribuidor.CodInvestidor equals investidor.Id
                                          where pgtoAdmPfee.Competencia == competencia
                                          select new
                                          {
                                              pgtoAdmPfee.Competencia,
                                              pgtoAdmPfee.CodFundo,
                                              SourceAdministrador = pgtoAdmPfee.CodAdministrador,
                                              pgtoAdmPfee.TaxaAdministracao,
                                              pgtoAdmPfee.TaxaPerformanceApropriada,
                                              pgtoAdmPfee.TaxaPerformanceResgate,
                                              CodigoInvestidorAdministrador = investidorDistribuidor.CodInvestAdministrador,
                                              CodigoInvestidor = investidorDistribuidor.CodInvestidor,
                                              CodigoDistribuidorInvestidor = investidorDistribuidor.CodDistribuidor,
                                              CodigoAdministradorCodigoInvestidor = investidorDistribuidor.CodAdministrador,
                                              NomeInvestidor = investidor.NomeCliente,
                                              investidor.Cnpj,
                                              investidor.TipoCliente,
                                              investidor.CodTipoContrato,
                                              CodigoAdministradorInvestidor = investidor.CodAdministrador,
                                              CodigoGestorInvestidor = investidor.CodGestor
                                          }).AsNoTracking().ToListAsync();
                /*
                var pgtosAdmPfee = await _context.TblPgtoAdmPfee
                    .Join(
                        _context.TblInvestidorDistribuidor,
                        pgtoAdmPfee => pgtoAdmPfee.CodInvestidorDistribuidor,
                        investidorDistribuidor => investidorDistribuidor.Id,
                        (pgtoAdmPfee, investidorDistribuidor) => new
                        {
                            pgtoAdmPfee.Competencia,
                            pgtoAdmPfee.CodFundo,
                            SourceAdministrador = pgtoAdmPfee.CodAdministrador,
                            pgtoAdmPfee.TaxaAdministracao,
                            pgtoAdmPfee.TaxaPerformanceApropriada,
                            pgtoAdmPfee.TaxaPerformanceResgate,
                            CodigoInvestidorAdministrador = investidorDistribuidor.CodInvestAdministrador,
                            CodigoInvestidor = investidorDistribuidor.CodInvestidor,
                            CodigoDistribuidorInvestidor = investidorDistribuidor.CodDistribuidor,
                            CodigoAdministradorCodigoInvestidor = investidorDistribuidor.CodAdministrador

                        }
                        )
                    .Join(
                        _context.TblInvestidor,
                        pgtoAdmPfeeInvestidorDistribuidor => pgtoAdmPfeeInvestidorDistribuidor.CodigoInvestidor,
                        investidor => investidor.Id,
                        (pgtoAdmPfeeInvestidorDistribuidor, investidor) => new
                        {
                            pgtoAdmPfeeInvestidorDistribuidor.Competencia,
                            pgtoAdmPfeeInvestidorDistribuidor.CodFundo,
                            pgtoAdmPfeeInvestidorDistribuidor.SourceAdministrador,
                            pgtoAdmPfeeInvestidorDistribuidor.TaxaAdministracao,
                            pgtoAdmPfeeInvestidorDistribuidor.TaxaPerformanceApropriada,
                            pgtoAdmPfeeInvestidorDistribuidor.TaxaPerformanceResgate,
                            pgtoAdmPfeeInvestidorDistribuidor.CodigoInvestidorAdministrador,
                            pgtoAdmPfeeInvestidorDistribuidor.CodigoInvestidor,
                            pgtoAdmPfeeInvestidorDistribuidor.CodigoDistribuidorInvestidor,
                            pgtoAdmPfeeInvestidorDistribuidor.CodigoAdministradorCodigoInvestidor,
                            NomeInvestidor = investidor.NomeCliente,
                            investidor.Cnpj,
                            investidor.TipoCliente,
                            //investidor.DirecaoPagamento,
                            CodigoAdministradorInvestidor = investidor.CodAdministrador,
                            CodigoGestorInvestidor = investidor.CodGestor
                        }
                    ).Where(c => c.Competencia == competencia).AsNoTracking().ToListAsync();
                */
                // Verificando a possibilidade de utilizar funções em paralelo para aumentar a velocidade de processamento.
                ConcurrentBag<PagamentoAdmPfeeInvestidorModel> pagamentoAdmPfeeInvestidor = new ConcurrentBag<PagamentoAdmPfeeInvestidorModel>();
                Parallel.ForEach(
                    pgtosAdmPfee,
                    x =>
                    {
                        PagamentoAdmPfeeInvestidorModel c = new PagamentoAdmPfeeInvestidorModel
                        {
                            Cnpj = x.Cnpj,
                            CodFundo = x.CodFundo,
                            CodigoAdministradorCodigoInvestidor = x.CodigoAdministradorCodigoInvestidor,
                            CodigoAdministradorInvestidor = x.CodigoAdministradorInvestidor,
                            CodigoDistribuidorInvestidor = x.CodigoDistribuidorInvestidor,
                            CodigoGestorInvestidor = x.CodigoGestorInvestidor,
                            CodigoInvestidor = x.CodigoInvestidor,
                            CodigoInvestidorAdministrador = x.CodigoInvestidorAdministrador,
                            Competencia = x.Competencia,
                            CodTipoContrato = x.CodTipoContrato,
                            NomeInvestidor = x.NomeInvestidor,
                            SourceAdministrador = x.SourceAdministrador,
                            TaxaAdministracao = x.TaxaAdministracao,
                            TaxaPerformanceApropriada = x.TaxaPerformanceApropriada,
                            TaxaPerformanceResgate = x.TaxaPerformanceResgate,
                            TipoCliente = x.TipoCliente
                        };
                        pagamentoAdmPfeeInvestidor.Add(c);
                    }
                );
                /*
                List<PagamentoAdmPfeeInvestidorModel> pagamentoAdmPfeeInvestidor = pgtosAdmPfee
                    .ConvertAll(
                    x => new PagamentoAdmPfeeInvestidorModel
                    {
                        Cnpj = x.Cnpj,
                        CodFundo = x.CodFundo,
                        CodigoAdministradorCodigoInvestidor = x.CodigoAdministradorCodigoInvestidor,
                        CodigoAdministradorInvestidor = x.CodigoAdministradorInvestidor,
                        CodigoDistribuidorInvestidor = x.CodigoDistribuidorInvestidor,
                        CodigoGestorInvestidor = x.CodigoGestorInvestidor,
                        CodigoInvestidor = x.CodigoInvestidor,
                        CodigoInvestidorAdministrador = x.CodigoInvestidorAdministrador,
                        Competencia = x.Competencia,
                        //DirecaoPagamento = x.DirecaoPagamento,
                        NomeInvestidor = x.NomeInvestidor,
                        SourceAdministrador = x.SourceAdministrador,
                        TaxaAdministracao = x.TaxaAdministracao,
                        TaxaPerformanceApropriada = x.TaxaPerformanceApropriada,
                        TaxaPerformanceResgate = x.TaxaPerformanceResgate,
                        TipoCliente = x.TipoCliente
                    }
                    );
                */
                if (pagamentoAdmPfeeInvestidor.IsEmpty)
                {
                    return NotFound();
                }

                return Ok(pagamentoAdmPfeeInvestidor);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }
        #endregion

        #region Calculo Pagamento Adm Pfee
        // GET: api/Pagamentos/GetCalculoPagamentoAdminPfee
        [HttpGet("{competencia}")]
        public async Task<ActionResult<IEnumerable<TblCalculoPgtoAdmPfee>>> GetCalculoPagamentoAdminPfee(string competencia)
        {
            try
            {
                List<TblCalculoPgtoAdmPfee> calculoPagamentoAdminPfee = await _context.TblCalculoPgtoAdmPfee
                    .Where(c => c.Competencia == competencia)
                    .AsNoTracking()
                    .ToListAsync();

                if (calculoPagamentoAdminPfee.Count == 0)
                {
                    return NotFound();
                }

                return Ok(calculoPagamentoAdminPfee);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Pagamentos/AddCalculoPagamentoAdminPfee/List<CalculoPgtoAdmPfeeModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CalculoPgtoAdmPfeeModel>>> AddCalculoPagamentoAdminPfee(List<CalculoPgtoAdmPfeeModel> tblCalculoPgtoAdmPfeeModel)
        {
            List<TblCalculoPgtoAdmPfee> listaCalculoPagamentosAdminPfee = new List<TblCalculoPgtoAdmPfee>();

            try
            {
                foreach (var line in tblCalculoPgtoAdmPfeeModel)
                {
                    TblCalculoPgtoAdmPfee itensCalculoPagamentoAdminPfee = new TblCalculoPgtoAdmPfee
                    {
                        CodAdministrador = line.CodAdministrador,
                        CodCondicaoRemuneracao = line.CodCondicaoRemuneracao,
                        CodContrato = line.CodContrato,
                        CodContratoFundo = line.CodContratoFundo,
                        CodContratoRemuneracao = line.CodContratoRemuneracao,
                        CodFundo = line.CodFundo,
                        CodInvestidor = line.CodInvestidor,
                        CodSubContrato = line.CodSubContrato,
                        Competencia = line.Competencia,
                        RebateAdm = line.RebateAdm,
                        RebatePfeeResgate = line.RebatePfeeResgate,
                        RebatePfeeSementre = line.RebatePfeeSementre,
                        ValorAdm = line.ValorAdm,
                        ValorPfeeResgate = line.ValorPfeeResgate,
                        ValorPfeeSementre = line.ValorPfeeSementre,
                        UsuarioModificacao = line.UsuarioModificacao
                    };

                    listaCalculoPagamentosAdminPfee.Add(itensCalculoPagamentoAdminPfee);
                }

                await _context.BulkInsertAsync(listaCalculoPagamentosAdminPfee);
                await _context.SaveChangesAsync();

                return Ok(listaCalculoPagamentosAdminPfee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Pagamentos/DeleteCalculoPagamentoAdminPfee/competencia
        [HttpDelete("{competencia}")]
        public async Task<ActionResult<IEnumerable<TblCalculoPgtoAdmPfee>>> DeleteCalculoPagamentoAdminPfee(string competencia)
        {
            List<TblCalculoPgtoAdmPfee> tblCalculoPgtoAdmPfee = await _context.TblCalculoPgtoAdmPfee.Where(c => c.Competencia == competencia).ToListAsync();

            if (tblCalculoPgtoAdmPfee.Count == 0)
            {
                return NotFound();
            }

            try
            {
                _context.TblCalculoPgtoAdmPfee.RemoveRange(tblCalculoPgtoAdmPfee);
                await _context.SaveChangesAsync();
                return Ok(tblCalculoPgtoAdmPfee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        #endregion
    }
}
