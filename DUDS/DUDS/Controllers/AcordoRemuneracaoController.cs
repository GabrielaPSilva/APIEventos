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
    public class AcordoRemuneracaoController : ControllerBase
    {
        private readonly DataContext _context;

        public AcordoRemuneracaoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/AcordoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAcordoRemuneracao>>> GetTblAcordoRemuneracao()
        {
            return await _context.TblAcordoRemuneracao.ToListAsync();
        }

        // GET: api/AcordoRemuneracao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAcordoRemuneracao>> GetTblAcordoRemuneracao(int id)
        {
            var tblAcordoRemuneracao = await _context.TblAcordoRemuneracao.FindAsync(id);

            if (tblAcordoRemuneracao == null)
            {
                return NotFound();
            }

            return tblAcordoRemuneracao;
        }

        // PUT: api/AcordoRemuneracao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblAcordoRemuneracao(int id, TblAcordoRemuneracao tblAcordoRemuneracao)
        {
            if (id != tblAcordoRemuneracao.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblAcordoRemuneracao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblAcordoRemuneracaoExists(id))
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

        // POST: api/AcordoRemuneracao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblAcordoRemuneracao>> PostTblAcordoRemuneracao(TblAcordoRemuneracao tblAcordoRemuneracao)
        {
            _context.TblAcordoRemuneracao.Add(tblAcordoRemuneracao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblAcordoRemuneracao", new { id = tblAcordoRemuneracao.Id }, tblAcordoRemuneracao);
        }

        // DELETE: api/AcordoRemuneracao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblAcordoRemuneracao(int id)
        {
            var tblAcordoRemuneracao = await _context.TblAcordoRemuneracao.FindAsync(id);
            if (tblAcordoRemuneracao == null)
            {
                return NotFound();
            }

            _context.TblAcordoRemuneracao.Remove(tblAcordoRemuneracao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblAcordoRemuneracaoExists(int id)
        {
            return _context.TblAcordoRemuneracao.Any(e => e.Id == id);
        }
    }
}
