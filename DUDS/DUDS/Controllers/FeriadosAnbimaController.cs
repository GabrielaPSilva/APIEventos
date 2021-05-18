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
    [Route("api/[controller]")]
    [ApiController]
    public class FeriadosAnbimaController : ControllerBase
    {
        private readonly DataContext _context;

        public FeriadosAnbimaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/FeriadosAnbima
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblFeriadosAnbima>>> GetTblFeriadosAnbima([FromQuery] DateTime dataFeriadoInicio, [FromQuery] DateTime? dataFeriadoFim = null)
        {
            //return await _context.TblFeriadosAnbima.ToListAsync();
            if (dataFeriadoFim == null)
            {
                dataFeriadoFim = dataFeriadoInicio;
            }

            var feriadosAnbima = await _context.TblFeriadosAnbima.AsNoTracking()
                .Where(f => f.DataFeriado >= dataFeriadoInicio && f.DataFeriado <= dataFeriadoFim)
                .ToListAsync();

            if (feriadosAnbima == null)
            {
                NotFound();
            }

            return feriadosAnbima;
                
        }

        // get: api/feriadosanbima/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblFeriadosAnbima>> GetTblFeriadosAnbima(int id)
        //{
        //    var tblferiadosanbima = await _context.TblFeriadosAnbima.FindAsync(id);

        //    if (tblferiadosanbima == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblferiadosanbima;
        //}

        //    // PUT: api/FeriadosAnbima/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutTblFeriadosAnbima(int id, TblFeriadosAnbima tblFeriadosAnbima)
        //    {
        //        if (id != tblFeriadosAnbima.Id)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(tblFeriadosAnbima).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TblFeriadosAnbimaExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/FeriadosAnbima
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<TblFeriadosAnbima>> PostTblFeriadosAnbima(TblFeriadosAnbima tblFeriadosAnbima)
        //    {
        //        _context.TblFeriadosAnbima.Add(tblFeriadosAnbima);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetTblFeriadosAnbima", new { id = tblFeriadosAnbima.Id }, tblFeriadosAnbima);
        //    }

        //    // DELETE: api/FeriadosAnbima/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteTblFeriadosAnbima(int id)
        //    {
        //        var tblFeriadosAnbima = await _context.TblFeriadosAnbima.FindAsync(id);
        //        if (tblFeriadosAnbima == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.TblFeriadosAnbima.Remove(tblFeriadosAnbima);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool TblFeriadosAnbimaExists(int id)
        //    {
        //        return _context.TblFeriadosAnbima.Any(e => e.Id == id);
        //    }
        //}
    }
}
