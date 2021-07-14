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

        // GET: api/Pagamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> GetTblPagamentoServico()
        {
            return await _context.TblPagamentoServico.ToListAsync();
        }

        // GET: api/Pagamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblPagamentoServico>> GetTblPagamentoServico(string id)
        {
            var tblPagamentoServico = await _context.TblPagamentoServico.FindAsync(id);

            if (tblPagamentoServico == null)
            {
                return NotFound();
            }

            return tblPagamentoServico;
        }

        // GET: api/PgtoAdmPfee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPgtoAdmPfee>>> GetTblPgtoAdmPfee()
        {
            return await _context.TblPgtoAdmPfee.ToListAsync();
        }

        // GET: api/PgtoAdmPfee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblPgtoAdmPfee>> GetTblPgtoAdmPfee(string id)
        {
            var tblPgtoAdmPfee = await _context.TblPgtoAdmPfee.FindAsync(id);

            if (tblPgtoAdmPfee == null)
            {
                return NotFound();
            }

            return tblPgtoAdmPfee;
        }

        // PUT: api/Pagamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblPagamentoServico(string id, TblPagamentoServico tblPagamentoServico)
        //{
        //    if (id != tblPagamentoServico.Competencia)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblPagamentoServico).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblPagamentoServicoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Pagamentos
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblPagamentoServico>> PostTblPagamentoServico(TblPagamentoServico tblPagamentoServico)
        //{
        //    _context.TblPagamentoServico.Add(tblPagamentoServico);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblPagamentoServicoExists(tblPagamentoServico.Competencia))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblPagamentoServico", new { id = tblPagamentoServico.Competencia }, tblPagamentoServico);
        //}

        //// DELETE: api/Pagamentos/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblPagamentoServico(string id)
        //{
        //    var tblPagamentoServico = await _context.TblPagamentoServico.FindAsync(id);
        //    if (tblPagamentoServico == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblPagamentoServico.Remove(tblPagamentoServico);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblPagamentoServicoExists(string id)
        //{
        //    return _context.TblPagamentoServico.Any(e => e.Competencia == id);
        //}
    }
}
