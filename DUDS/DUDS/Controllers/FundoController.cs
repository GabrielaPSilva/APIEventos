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
    public class FundoController : ControllerBase
    {
        private readonly DataContext _context;

        public FundoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Fundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblFundo>>> GetTblFundo()
        {
            return await _context.TblFundo.ToListAsync();
        }

        // GET: api/Fundo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblFundo>> GetTblFundo(int id)
        {
            var tblFundo = await _context.TblFundo.FindAsync(id);

            if (tblFundo == null)
            {
                return NotFound();
            }

            return tblFundo;
        }

        // PUT: api/Fundo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblFundo(int id, TblFundo tblFundo)
        {
            if (id != tblFundo.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblFundo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblFundoExists(id))
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

        // POST: api/Fundo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblFundo>> PostTblFundo(TblFundo tblFundo)
        {
            _context.TblFundo.Add(tblFundo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblFundo", new { id = tblFundo.Id }, tblFundo);
        }

        // DELETE: api/Fundo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblFundo(int id)
        {
            var tblFundo = await _context.TblFundo.FindAsync(id);
            if (tblFundo == null)
            {
                return NotFound();
            }

            _context.TblFundo.Remove(tblFundo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblFundoExists(int id)
        {
            return _context.TblFundo.Any(e => e.Id == id);
        }
    }
}
