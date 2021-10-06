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

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
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

                return CreatedAtAction(
                    nameof(GetPagamentoServico), listaPagamentosServico);
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

        #region Pagamento Taxa Admin Pfee
        // GET: api/Pagamentos/GetPgtoTaxaAdmPfee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPgtoAdmPfee>>> GetPgtoTaxaAdmPfee()
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

        // GET: api/Pagamentos/GetPgtoTaxaAdmPfeeByIds/competencia/cod_investidor_distribuidor/cod_administrador/cod_fundo
        [HttpGet("{competencia}/{cod_investidor_distribuidor}/{cod_administrador}/{cod_fundo}")]
        public async Task<ActionResult<TblPgtoAdmPfee>> GetPgtoTaxaAdmPfeeByIds(string competencia, int cod_investidor_distribuidor, int cod_administrador, int cod_fundo)
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

        //POST: api/Pagamentos/AddPgtoTaxaAdminPfee/List<PagamentoTaxaAdminPfeeModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<PagamentoTaxaAdminPfeeModel>>> AddPgtoTaxaAdminPfee(List<PagamentoTaxaAdminPfeeModel> tblPagamentoAdminPfeeModel)
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

                return CreatedAtAction(
                   nameof(GetPgtoTaxaAdmPfee), listaPagamentosAdminPfee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Pagamentos/DeletePagamentoTaxaAdminPfee/competencia
        [HttpDelete("{competencia}")]
        public async Task<ActionResult<IEnumerable<TblPgtoAdmPfee>>> DeletePagamentoTaxaAdminPfee(string competencia)
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

        #region Pagamento Taxa Admin Pfee Com Dados do Investidor
        // GET: api/Pagamentos/GetPgtoTaxaAdmPfeeInvestidor
        [HttpGet("{competencia}")]
        public async Task<ActionResult<IEnumerable<PagamentoAdmPfeeInvestidorModel>>> GetPgtoTaxaAdmPfeeInvestidor(string competencia)
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
                                              investidor.CodGrupoRebate,
                                              CodigoAdministradorInvestidor = investidor.CodAdministrador,
                                              CodigoGestorInvestidor = investidor.CodGestor
                                          }).AsNoTracking().ToListAsync();
                
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
                            TipoCliente = x.TipoCliente,
                            CodGrupoRebate = x.CodGrupoRebate
                        };
                        pagamentoAdmPfeeInvestidor.Add(c);
                    }
                );
                
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
        // GET: api/Pagamentos/GetCalculoPgtoTaxaAdminPfee
        [HttpGet("{competencia}")]
        public async Task<ActionResult<IEnumerable<CalculoPgtoTaxaAdmPfeeModel>>> GetCalculoPgtoTaxaAdminPfee(string competencia)
        {
            try
            {
                var calculoPagamentoAdminPfee = await (from calculoPgtoAdmPfee in _context.TblCalculoPgtoAdmPfee.Where(c => c.Competencia == competencia)
                                                       from investidor in _context.TblInvestidor.Where(i => i.Id == calculoPgtoAdmPfee.CodInvestidor)
                                                       //where investidor.Ativo.Equals(1) && calculoPgtoAdmPfee.Ativo.Equals(1)
                                                       select new
                                                       {
                                                           investidor.CodGrupoRebate,
                                                           calculoPgtoAdmPfee.CodFundo,
                                                           calculoPgtoAdmPfee.Competencia,
                                                           calculoPgtoAdmPfee.CodAdministrador,
                                                           calculoPgtoAdmPfee.CodCondicaoRemuneracao,
                                                           calculoPgtoAdmPfee.CodContrato,
                                                           calculoPgtoAdmPfee.CodContratoFundo,
                                                           calculoPgtoAdmPfee.CodContratoRemuneracao,
                                                           calculoPgtoAdmPfee.CodInvestidor,
                                                           calculoPgtoAdmPfee.CodSubContrato,
                                                           calculoPgtoAdmPfee.RebateAdm,
                                                           calculoPgtoAdmPfee.RebatePfeeResgate,
                                                           calculoPgtoAdmPfee.RebatePfeeSementre,
                                                           calculoPgtoAdmPfee.ValorAdm,
                                                           calculoPgtoAdmPfee.ValorPfeeResgate,
                                                           calculoPgtoAdmPfee.ValorPfeeSementre
                                                       }).AsNoTracking().ToListAsync();

                ConcurrentBag<CalculoPgtoTaxaAdmPfeeModel> resultadoCalculoPgtoAdmPfee = new ConcurrentBag<CalculoPgtoTaxaAdmPfeeModel>();
                Parallel.ForEach(
                    calculoPagamentoAdminPfee,
                    x =>
                    {
                        CalculoPgtoTaxaAdmPfeeModel c = new CalculoPgtoTaxaAdmPfeeModel
                        {
                            CodAdministrador = x.CodAdministrador,
                            CodCondicaoRemuneracao = x.CodCondicaoRemuneracao,
                            ValorPfeeSementre = x.ValorPfeeSementre,
                            ValorPfeeResgate = x.ValorPfeeResgate,
                            ValorAdm = x.ValorAdm,
                            RebatePfeeSementre = x.RebatePfeeSementre,
                            RebatePfeeResgate = x.RebatePfeeResgate,
                            CodContrato = x.CodContrato,
                            CodContratoFundo = x.CodContratoFundo,
                            CodContratoRemuneracao = x.CodContratoRemuneracao,
                            CodSubContrato = x.CodSubContrato,
                            CodFundo = x.CodFundo,
                            CodInvestidor = x.CodInvestidor,
                            Competencia = x.Competencia,
                            CodGrupoRebate = x.CodGrupoRebate,
                            RebateAdm = x.RebateAdm
                        };
                        resultadoCalculoPgtoAdmPfee.Add(c);
                    }
                );
                //List<TblCalculoPgtoAdmPfee> calculoPagamentoAdminPfee = await _context.TblCalculoPgtoAdmPfee
                //    .Where(c => c.Competencia == competencia)
                //    .AsNoTracking()
                //    .ToListAsync();

                if (resultadoCalculoPgtoAdmPfee.IsEmpty)
                {
                    return NotFound();
                }

                return Ok(resultadoCalculoPgtoAdmPfee);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Pagamentos/AddCalculoPgtoTaxaAdminPfee/List<CalculoPgtoTaxaAdmPfeeModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CalculoPgtoTaxaAdmPfeeModel>>> AddCalculoPgtoTaxaAdminPfee(List<CalculoPgtoTaxaAdmPfeeModel> tblCalculoPgtoAdmPfeeModel)
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

                return CreatedAtAction(
                 nameof(GetCalculoPgtoTaxaAdminPfee), listaCalculoPagamentosAdminPfee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Pagamentos/DeleteCalculoPgtoTaxaAdminPfee/competencia
        [HttpDelete("{competencia}")]
        public async Task<ActionResult<IEnumerable<TblCalculoPgtoAdmPfee>>> DeleteCalculoPgtoTaxaAdminPfee(string competencia)
        {
            try
            {
                List<TblCalculoPgtoAdmPfee> tblCalculoPgtoAdmPfee = await _context.TblCalculoPgtoAdmPfee
                .Where(c => c.Competencia == competencia)
                .ToListAsync();

                if (tblCalculoPgtoAdmPfee.Count == 0)
                {
                    return NotFound();
                }


                _context.TblCalculoPgtoAdmPfee.RemoveRange(tblCalculoPgtoAdmPfee);
                await _context.SaveChangesAsync();
                return Ok(tblCalculoPgtoAdmPfee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetDescricaoCalculoPgtoTaxaAdmPfee
        [HttpGet("{cod_contrato}/{cod_sub_contrato}/{cod_contrato_fundo}/{cod_contrato_remuneracao}")]
        public async Task<ActionResult<IEnumerable<DescricaoCalculoPgtoTaxaAdmPfeeModel>>> GetDescricaoCalculoPgtoTaxaAdmPfee(int cod_contrato, int cod_sub_contrato, int cod_contrato_fundo, int cod_contrato_remuneracao, [FromQuery] string cod_condicao_remuneracao)
        {
            try
            {
                var detatalheContratoCalculoPgtoAdmPfee = await (from contrato in _context.TblContrato.Where(c => c.Id == cod_contrato)
                                                                 from tipoContrato in _context.TblTipoContrato.Where(tC => tC.Id == contrato.CodTipoContrato)
                                                                 from subContrato in _context.TblSubContrato.Where(sC => sC.Id == cod_sub_contrato)
                                                                 from contratoFundo in _context.TblContratoFundo.Where(cF => cF.Id == cod_contrato_fundo)
                                                                 from codigoCondicao in _context.TblTipoCondicao.Where(cC => cC.Id == contratoFundo.CodTipoCondicao)
                                                                 from fundo in _context.TblFundo.Where(f => f.Id == contratoFundo.CodFundo)
                                                                 from contratoRemuneracao in _context.TblContratoRemuneracao.Where(cR => cR.Id == cod_contrato_remuneracao)
                                                                 from distribuidor in _context.TblDistribuidor.Where(d => d.Id == contrato.CodDistribuidor).DefaultIfEmpty()
                                                                 from gestor in _context.TblGestor.Where(g => g.Id == contrato.Parceiro).DefaultIfEmpty()
                                                                 //where contrato.Ativo.Equals(1)
                                                                 select new
                                                                 {
                                                                     tipoContrato.TipoContrato,
                                                                     ParceiroDistribuidor = distribuidor == null ? gestor.NomeGestor : distribuidor.NomeDistribuidor,
                                                                     VersaoContrato = subContrato.Versao,
                                                                     StatusContrato = subContrato.Status,
                                                                     subContrato.IdDocusign,
                                                                     subContrato.DataVigenciaInicio,
                                                                     subContrato.DataVigenciaFim,
                                                                     subContrato.DataRetroatividade,
                                                                     NomeFundo = fundo.NomeReduzido,
                                                                     codigoCondicao.TipoCondicao,
                                                                     contratoRemuneracao.PercentualAdm,
                                                                     contratoRemuneracao.PercentualPfee
                                                                 }).AsNoTracking().FirstOrDefaultAsync();

                DescricaoCalculoPgtoTaxaAdmPfeeModel descricaoCalculoPgtoAdmPfee = new DescricaoCalculoPgtoTaxaAdmPfeeModel
                {
                    DataRetroatividade = detatalheContratoCalculoPgtoAdmPfee.DataRetroatividade,
                    DataVigenciaFim = detatalheContratoCalculoPgtoAdmPfee.DataVigenciaFim,
                    DataVigenciaInicio = detatalheContratoCalculoPgtoAdmPfee.DataVigenciaInicio,
                    IdDocusign = detatalheContratoCalculoPgtoAdmPfee.IdDocusign,
                    NomeFundo = detatalheContratoCalculoPgtoAdmPfee.NomeFundo,
                    ParceiroDistribuidor = detatalheContratoCalculoPgtoAdmPfee.ParceiroDistribuidor,
                    PercentualAdm = detatalheContratoCalculoPgtoAdmPfee.PercentualAdm,
                    PercentualPfee = detatalheContratoCalculoPgtoAdmPfee.PercentualPfee,
                    StatusContrato = detatalheContratoCalculoPgtoAdmPfee.StatusContrato,
                    TipoCondicao = detatalheContratoCalculoPgtoAdmPfee.TipoCondicao,
                    TipoContrato = detatalheContratoCalculoPgtoAdmPfee.TipoContrato,
                    VersaoContrato = detatalheContratoCalculoPgtoAdmPfee.VersaoContrato
                };
                detatalheContratoCalculoPgtoAdmPfee = null;
                return Ok(descricaoCalculoPgtoAdmPfee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion
    }
}
