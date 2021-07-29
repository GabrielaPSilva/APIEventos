using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;

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
        // GET: api/Pagamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> PagamentoServico()
        {
            try
            {
                var pgmtoServico = await _context.TblPagamentoServico.AsNoTracking().ToListAsync();

                if (pgmtoServico != null)
                {
                    return Ok(pgmtoServico);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (InvalidOperationException e)
            {
                return BadRequest();
            }
        }

        // GET: api/Pagamentos/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblPagamentoServico>> GetPagamentoServico(string id)
        {
            var tblPagamentoServico = await _context.TblPagamentoServico.FindAsync(id);

            if (tblPagamentoServico == null)
            {
                return NotFound();
            }

            return Ok(tblPagamentoServico);
        }

        [HttpPost]
        public async Task<ActionResult<PagamentoServicoModel>> CadastrarPagamentoServico(PagamentoServicoModel tblPagamentoServicoModel)
        {
            var itensPagamentoServico = new TblPagamentoServico
            {
                Competencia = tblPagamentoServicoModel.Competencia,
                CodFundo = tblPagamentoServicoModel.CodFundo,
                TaxaAdm = tblPagamentoServicoModel.TaxaAdm,
                AdmFiduciaria = tblPagamentoServicoModel.AdmFiduciaria,
                Servico = tblPagamentoServicoModel.Servico,
                SaldoParcial = tblPagamentoServicoModel.SaldoParcial,
                SaldoGestor = tblPagamentoServicoModel.SaldoGestor
            };

            _context.TblPagamentoServico.Add(itensPagamentoServico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPagamentoServico),
                Ok(itensPagamentoServico));
        }

        // DELETE: api/Pagamentos/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPagamentoServico(int id)
        {
            var tblPagamentoServico = await _context.TblPagamentoServico.FindAsync(id);

            if (tblPagamentoServico == null)
            {
                return NotFound();
            }

            _context.TblPagamentoServico.Remove(tblPagamentoServico);
            await _context.SaveChangesAsync();

            return Ok();
        }

        #endregion

        #region Pagamento Admin Pfee
        // GET: api/PgtoAdmPfee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPgtoAdmPfee>>> PgtoAdmPfee()
        {
            return await _context.TblPgtoAdmPfee.ToListAsync();
        }

        // GET: api/PgtoAdmPfee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblPgtoAdmPfee>> GetPgtoAdmPfee(string id)
        {
            var tblPgtoAdmPfee = await _context.TblPgtoAdmPfee.FindAsync(id);

            if (tblPgtoAdmPfee == null)
            {
                return NotFound();
            }

            return Ok(tblPgtoAdmPfee);
        }

        [HttpPost]
        public async Task<ActionResult<PagamentoAdminPfeeModel>> CadastrarPagamentoAdminPfee(PagamentoAdminPfeeModel tblPagamentoAdminPfeeModel)
        {
            var itensPagamentoAdminPfee = new TblPgtoAdmPfee
            {
                Competencia = tblPagamentoAdminPfeeModel.Competencia,
                CodCliente = tblPagamentoAdminPfeeModel.CodCliente,
                CodFundo = tblPagamentoAdminPfeeModel.CodFundo,
                TaxaPerformanceApropriada = tblPagamentoAdminPfeeModel.TaxaPerformanceApropriada,
                TaxaPerformanceResgate = tblPagamentoAdminPfeeModel.TaxaPerformanceResgate,
                TaxaAdministracao = tblPagamentoAdminPfeeModel.TaxaAdministracao,
                TaxaGestao = tblPagamentoAdminPfeeModel.TaxaGestao
            };

            _context.TblPgtoAdmPfee.Add(itensPagamentoAdminPfee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPgtoAdmPfee),
                Ok(itensPagamentoAdminPfee));
        }

        // DELETE: api/Fundo/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPagamentoAdminPfee(int id)
        {
            var tblPagamentoAdminPfee = await _context.TblPgtoAdmPfee.FindAsync(id);

            if (tblPagamentoAdminPfee == null)
            {
                return NotFound();
            }

            _context.TblPgtoAdmPfee.Remove(tblPagamentoAdminPfee);
            await _context.SaveChangesAsync();

            return Ok();
        }

        #endregion
    }
}
