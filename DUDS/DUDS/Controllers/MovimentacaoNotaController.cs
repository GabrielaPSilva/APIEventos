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
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoNotaController : ControllerBase
    {
        private readonly DataContext _context;

        public MovimentacaoNotaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TblMovimentacaoNota
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMovimentacaoNota>>> GetTblMovimentacaoNota()
        {
            return await _context.TblMovimentacaoNota.ToListAsync();
        }

        // GET: api/TblMovimentacaoNota/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMovimentacaoNota>> GetTblMovimentacaoNota(int id)
        {
            var tblMovimentacaoNota = await _context.TblMovimentacaoNota.FindAsync(id);

            if (tblMovimentacaoNota == null)
            {
                return NotFound();
            }

            return tblMovimentacaoNota;
        }

        // PUT: api/TblMovimentacaoNota/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblMovimentacaoNota(int id, TblMovimentacaoNota tblMovimentacaoNota)
        {
            if (id != tblMovimentacaoNota.CodMovimentacao)
            {
                return BadRequest();
            }

            _context.Entry(tblMovimentacaoNota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblMovimentacaoNotaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TblMovimentacaoNota
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblMovimentacaoNota>> PostTblMovimentacaoNota(TblMovimentacaoNota tblMovimentacaoNota)
        {
            _context.TblMovimentacaoNota.Add(tblMovimentacaoNota);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblMovimentacaoNotaExists(tblMovimentacaoNota.CodMovimentacao))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblMovimentacaoNota", new { id = tblMovimentacaoNota.CodMovimentacao }, tblMovimentacaoNota);
        }

        // DELETE: api/TblMovimentacaoNota/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblMovimentacaoNota(int id)
        {
            var tblMovimentacaoNota = await _context.TblMovimentacaoNota.FindAsync(id);
            if (tblMovimentacaoNota == null)
            {
                return NotFound();
            }

            _context.TblMovimentacaoNota.Remove(tblMovimentacaoNota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblMovimentacaoNotaExists(int id)
        {
            return _context.TblMovimentacaoNota.Any(e => e.CodMovimentacao == id);
        }
    }
}
