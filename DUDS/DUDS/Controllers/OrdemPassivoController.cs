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
    public class OrdemPassivoController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdemPassivoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/OrdemPassivo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblOrdemPassivo>>> GetTblOrdemPassivo()
        {
            return await _context.TblOrdemPassivo.ToListAsync();
        }

        // GET: api/OrdemPassivo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblOrdemPassivo>> GetTblOrdemPassivo(int id)
        {
            var tblOrdemPassivo = await _context.TblOrdemPassivo.FindAsync(id);

            if (tblOrdemPassivo == null)
            {
                return NotFound();
            }

            return tblOrdemPassivo;
        }

        // PUT: api/OrdemPassivo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblOrdemPassivo(int id, TblOrdemPassivo tblOrdemPassivo)
        //{
        //    if (id != tblOrdemPassivo.NumOrdem)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblOrdemPassivo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblOrdemPassivoExists(id))
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

        //// POST: api/OrdemPassivo
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblOrdemPassivo>> PostTblOrdemPassivo(TblOrdemPassivo tblOrdemPassivo)
        //{
        //    _context.TblOrdemPassivo.Add(tblOrdemPassivo);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblOrdemPassivoExists(tblOrdemPassivo.NumOrdem))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblOrdemPassivo", new { id = tblOrdemPassivo.NumOrdem }, tblOrdemPassivo);
        //}

        //// DELETE: api/OrdemPassivo/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblOrdemPassivo(int id)
        //{
        //    var tblOrdemPassivo = await _context.TblOrdemPassivo.FindAsync(id);
        //    if (tblOrdemPassivo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblOrdemPassivo.Remove(tblOrdemPassivo);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblOrdemPassivoExists(int id)
        //{
        //    return _context.TblOrdemPassivo.Any(e => e.NumOrdem == id);
        //}
    }
}
