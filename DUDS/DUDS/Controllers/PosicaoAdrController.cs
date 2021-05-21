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
    public class PosicaoAdrController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicaoAdrController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoAdr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoAdr>>> GetTblPosicaoAdr([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }
            var posicaoAdr = await _context.TblPosicaoAdr.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoAdr == null)
            {
                NotFound();
            }
            return posicaoAdr;
        }

        //// GET: api/PosicaoAdr/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblPosicaoAdr>> GetTblPosicaoAdr(DateTime id)
        //{
        //    var tblPosicaoAdr = await _context.TblPosicaoAdr.FindAsync(id);

        //    if (tblPosicaoAdr == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblPosicaoAdr;
        //}

        //// PUT: api/PosicaoAdr/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblPosicaoAdr(DateTime id, TblPosicaoAdr tblPosicaoAdr)
        //{
        //    if (id != tblPosicaoAdr.DataRef)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblPosicaoAdr).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblPosicaoAdrExists(id))
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

        //// POST: api/PosicaoAdr
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblPosicaoAdr>> PostTblPosicaoAdr(TblPosicaoAdr tblPosicaoAdr)
        //{
        //    _context.TblPosicaoAdr.Add(tblPosicaoAdr);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblPosicaoAdrExists(tblPosicaoAdr.DataRef))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblPosicaoAdr", new { id = tblPosicaoAdr.DataRef }, tblPosicaoAdr);
        //}

        //// DELETE: api/PosicaoAdr/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblPosicaoAdr(DateTime id)
        //{
        //    var tblPosicaoAdr = await _context.TblPosicaoAdr.FindAsync(id);
        //    if (tblPosicaoAdr == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblPosicaoAdr.Remove(tblPosicaoAdr);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblPosicaoAdrExists(DateTime id)
        //{
        //    return _context.TblPosicaoAdr.Any(e => e.DataRef == id);
        //}
    }
}
