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
    public class AlocadorController : ControllerBase
    {
        private readonly DataContext _context;

        public AlocadorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Alocador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAlocador>>> GetTblAlocador()
        {
            return await _context.TblAlocador.ToListAsync();
        }

        // GET: api/Alocador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAlocador>> GetTblAlocador(int id)
        {
            var tblAlocador = await _context.TblAlocador.FindAsync(id);

            if (tblAlocador == null)
            {
                return NotFound();
            }

            return tblAlocador;
        }

        // PUT: api/Alocador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblAlocador(int id, TblAlocador tblAlocador)
        {
            if (id != tblAlocador.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblAlocador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblAlocadorExists(id))
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

        // POST: api/Alocador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblAlocador>> PostTblAlocador(TblAlocador tblAlocador)
        {
            _context.TblAlocador.Add(tblAlocador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblAlocador", new { id = tblAlocador.Id }, tblAlocador);
        }

        // DELETE: api/Alocador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblAlocador(int id)
        {
            var tblAlocador = await _context.TblAlocador.FindAsync(id);
            if (tblAlocador == null)
            {
                return NotFound();
            }

            _context.TblAlocador.Remove(tblAlocador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblAlocadorExists(int id)
        {
            return _context.TblAlocador.Any(e => e.Id == id);
        }
    }
}
