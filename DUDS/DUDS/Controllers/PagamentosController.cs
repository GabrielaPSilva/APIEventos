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
        // GET: api/Pagamentos/PagamentoServico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> PagamentoServico()
        {
            try
            {
                List<TblPagamentoServico> pgtosServico = await _context.TblPagamentoServico.OrderByDescending(c => c.Competencia).AsNoTracking().ToListAsync();

                if (pgtosServico.Count() == 0)
                {
                    return NotFound();
                }

                if (pgtosServico != null)
                {
                    return Ok(pgtosServico);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e);
            }
        }

        // GET: api/Pagamentos/GetPagamentoServico/competencia/cod_fundo
        [HttpGet("{competencia}/{cod_fundo}")]
        public async Task<ActionResult<TblPagamentoServico>> GetPagamentoServico(string competencia, int cod_fundo)
        {
            TblPagamentoServico tblPagamentoServico = await _context.TblPagamentoServico.FindAsync(competencia, cod_fundo);

            try
            {
                if (tblPagamentoServico != null)
                {
                    return Ok(tblPagamentoServico);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Pagamentos/CadastrarPagamentoServico/List<PagamentoServicoModel>
        [HttpPost]
        public async Task<ActionResult<PagamentoServicoModel>> CadastrarPagamentoServico(List<PagamentoServicoModel> tblPagamentoServicoModel)
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

        // DELETE: api/Pagamentos/DeletarPagamentoServico/competencia
        [HttpDelete("{competencia}")]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> DeletarPagamentoServico(string competencia)
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
        // GET: api/Pagamentos/PgtoAdmPfee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPgtoAdmPfee>>> PgtoAdmPfee()
        {
            try
            {
                List<TblPgtoAdmPfee> pgtosAdmPfee = await _context.TblPgtoAdmPfee.OrderByDescending(c => c.Competencia).AsNoTracking().ToListAsync();

                if (pgtosAdmPfee.Count() == 0)
                {
                    return NotFound();
                }

                if (pgtosAdmPfee != null)
                {
                    return Ok(pgtosAdmPfee);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e);
            }
        }

        // GET: api/Pagamentos/GetPgtoAdmPfee/competencia/cod_investidor_distribuidor/cod_administrador/cod_fundo
        [HttpGet("{competencia}/{cod_investidor_distribuidor}/{cod_administrador}/{cod_fundo}")]
        public async Task<ActionResult<TblPgtoAdmPfee>> GetPgtoAdmPfee(string competencia, int cod_investidor_distribuidor, int cod_administrador, int cod_fundo)
        {
            TblPgtoAdmPfee tblPgtoAdmPfee = await _context.TblPgtoAdmPfee.FindAsync(competencia, cod_investidor_distribuidor, cod_administrador, cod_fundo);
            
            try
            {
                if (tblPgtoAdmPfee != null)
                {
                    return Ok(tblPgtoAdmPfee);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Pagamentos/CadastrarPagamentoAdminPfee/List<PagamentoAdminPfeeModel>
        [HttpPost]
        public async Task<ActionResult<PagamentoAdminPfeeModel>> CadastrarPagamentoAdminPfee(List<PagamentoAdminPfeeModel> tblPagamentoAdminPfeeModel)
        {
            List<TblPgtoAdmPfee> listaPagamentosAdminPfee = new List<TblPgtoAdmPfee>();
            TblPgtoAdmPfee itensPagamentoAdminPfee = new TblPgtoAdmPfee();

            try
            {
                foreach (var line in tblPagamentoAdminPfeeModel)
                {
                    itensPagamentoAdminPfee = new TblPgtoAdmPfee
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

                return Ok(itensPagamentoAdminPfee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Pagamentos/DeletarPagamentoAdminPfee/competencia
        [HttpDelete("{competencia}")]
        public async Task<ActionResult<IEnumerable<TblPgtoAdmPfee>>> DeletarPagamentoAdminPfee(string competencia)
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
    }
}
