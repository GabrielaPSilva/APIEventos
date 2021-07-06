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
    public class CustodianteController : ControllerBase
    {
        private readonly DataContext _context;

        public CustodianteController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Custodiante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCustodiante>>> Custodiante()
        {
            return await _context.TblCustodiante.ToListAsync();
        }

        // GET: api/Custodiante/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCustodiante>> GetCustodiante(int id)
        {
            var tblCustodiante = await _context.TblCustodiante.FindAsync(id);

            if (tblCustodiante == null)
            {
                return NotFound();
            }

            return tblCustodiante;
        }

        // PUT: api/Custodiante/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblCustodiante(int id, TblCustodiante tblCustodiante)
        //{
        //    if (id != tblCustodiante.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblCustodiante).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblCustodianteExists(id))
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

        //// POST: api/Custodiante
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblCustodiante>> PostTblCustodiante(TblCustodiante tblCustodiante)
        //{
        //    _context.TblCustodiante.Add(tblCustodiante);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTblCustodiante", new { id = tblCustodiante.Id }, tblCustodiante);
        //}

        //// DELETE: api/Custodiante/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblCustodiante(int id)
        //{
        //    var tblCustodiante = await _context.TblCustodiante.FindAsync(id);
        //    if (tblCustodiante == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblCustodiante.Remove(tblCustodiante);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblCustodianteExists(int id)
        //{
        //    return _context.TblCustodiante.Any(e => e.Id == id);
        //}
    }
}
