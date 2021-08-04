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
    public class InvestidorDistribuidorController : ControllerBase
    {
        private readonly DataContext _context;

        public InvestidorDistribuidorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/InvestidorDistribuidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblInvestidorDistribuidor>>> GetTblInvestidorDistribuidor()
        {
            return await _context.TblInvestidorDistribuidor.ToListAsync();
        }

        // GET: api/InvestidorDistribuidor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblInvestidorDistribuidor>> GetTblInvestidorDistribuidor(int id)
        {
            var tblInvestidorDistribuidor = await _context.TblInvestidorDistribuidor.FindAsync(id);

            if (tblInvestidorDistribuidor == null)
            {
                return NotFound();
            }

            return tblInvestidorDistribuidor;
        }

        // PUT: api/InvestidorDistribuidor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblInvestidorDistribuidor(int id, TblInvestidorDistribuidor tblInvestidorDistribuidor)
        {
            if (id != tblInvestidorDistribuidor.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblInvestidorDistribuidor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblInvestidorDistribuidorExists(id))
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

        // POST: api/InvestidorDistribuidor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblInvestidorDistribuidor>> PostTblInvestidorDistribuidor(TblInvestidorDistribuidor tblInvestidorDistribuidor)
        {
            _context.TblInvestidorDistribuidor.Add(tblInvestidorDistribuidor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblInvestidorDistribuidor", new { id = tblInvestidorDistribuidor.Id }, tblInvestidorDistribuidor);
        }

        // DELETE: api/InvestidorDistribuidor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblInvestidorDistribuidor(int id)
        {
            var tblInvestidorDistribuidor = await _context.TblInvestidorDistribuidor.FindAsync(id);
            if (tblInvestidorDistribuidor == null)
            {
                return NotFound();
            }

            _context.TblInvestidorDistribuidor.Remove(tblInvestidorDistribuidor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblInvestidorDistribuidorExists(int id)
        {
            return _context.TblInvestidorDistribuidor.Any(e => e.Id == id);
        }
    }
}
