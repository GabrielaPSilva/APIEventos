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
    public class ContasController : ControllerBase
    {
        private readonly DataContext _context;

        public ContasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Contas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContas>>> GetTblContas()
        {
            return await _context.TblContas.ToListAsync();
        }

        // GET: api/Contas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContas>> GetTblContas(int id)
        {
            var tblContas = await _context.TblContas.FindAsync(id);

            if (tblContas == null)
            {
                return NotFound();
            }

            return tblContas;
        }

        // PUT: api/Contas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblContas(int id, TblContas tblContas)
        {
            if (id != tblContas.CodFundo)
            {
                return BadRequest();
            }

            _context.Entry(tblContas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblContasExists(id))
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

        // POST: api/Contas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblContas>> PostTblContas(TblContas tblContas)
        {
            _context.TblContas.Add(tblContas);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblContasExists(tblContas.CodFundo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblContas", new { id = tblContas.CodFundo }, tblContas);
        }

        // DELETE: api/Contas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblContas(int id)
        {
            var tblContas = await _context.TblContas.FindAsync(id);
            if (tblContas == null)
            {
                return NotFound();
            }

            _context.TblContas.Remove(tblContas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblContasExists(int id)
        {
            return _context.TblContas.Any(e => e.CodFundo == id);
        }
    }
}
