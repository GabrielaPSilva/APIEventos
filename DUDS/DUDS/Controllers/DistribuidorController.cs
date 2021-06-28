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
    public class DistribuidorController : ControllerBase
    {
        private readonly DataContext _context;

        public DistribuidorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Distribuidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDistribuidor>>> GetTblDistribuidor()
        {
            return await _context.TblDistribuidor.ToListAsync();
        }

        // GET: api/Distribuidor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDistribuidor>> GetTblDistribuidor(int id)
        {
            var tblDistribuidor = await _context.TblDistribuidor.FindAsync(id);

            if (tblDistribuidor == null)
            {
                return NotFound();
            }

            return tblDistribuidor;
        }

        // PUT: api/Distribuidor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblDistribuidor(int id, TblDistribuidor tblDistribuidor)
        {
            if (id != tblDistribuidor.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblDistribuidor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblDistribuidorExists(id))
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

        // POST: api/Distribuidor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblDistribuidor>> PostTblDistribuidor(TblDistribuidor tblDistribuidor)
        {
            _context.TblDistribuidor.Add(tblDistribuidor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblDistribuidor", new { id = tblDistribuidor.Id }, tblDistribuidor);
        }

        // DELETE: api/Distribuidor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblDistribuidor(int id)
        {
            var tblDistribuidor = await _context.TblDistribuidor.FindAsync(id);
            if (tblDistribuidor == null)
            {
                return NotFound();
            }

            _context.TblDistribuidor.Remove(tblDistribuidor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblDistribuidorExists(int id)
        {
            return _context.TblDistribuidor.Any(e => e.Id == id);
        }
    }
}
