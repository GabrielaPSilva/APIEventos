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
    public class InvestidorController : ControllerBase
    {
        private readonly DataContext _context;

        public InvestidorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Investidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblInvestidor>>> Investidor()
        {
            return await _context.TblInvestidor.ToListAsync();
        }

        // GET: api/Investidor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblInvestidor>> GetInvestidor(int id)
        {
            var tblInvestidor = await _context.TblInvestidor.FindAsync(id);

            if (tblInvestidor == null)
            {
                return NotFound();
            }

            return tblInvestidor;
        }

        // PUT: api/Investidor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblInvestidor(int id, TblInvestidor tblInvestidor)
        //{
        //    if (id != tblInvestidor.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblInvestidor).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblInvestidorExists(id))
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

        //// POST: api/Investidor
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblInvestidor>> PostTblInvestidor(TblInvestidor tblInvestidor)
        //{
        //    _context.TblInvestidor.Add(tblInvestidor);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTblInvestidor", new { id = tblInvestidor.Id }, tblInvestidor);
        //}

        //// DELETE: api/Investidor/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblInvestidor(int id)
        //{
        //    var tblInvestidor = await _context.TblInvestidor.FindAsync(id);
        //    if (tblInvestidor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblInvestidor.Remove(tblInvestidor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblInvestidorExists(int id)
        //{
        //    return _context.TblInvestidor.Any(e => e.Id == id);
        //}
    }
}
