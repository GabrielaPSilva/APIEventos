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
    public class PosicaoFuturoController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicaoFuturoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoFuturo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoFuturo>>> GetTblPosicaoFuturo([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }
            var posicaoFuturo = await _context.TblPosicaoFuturo.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoFuturo == null)
            {
                NotFound();
            }
            return posicaoFuturo;
        }

        //// GET: api/PosicaoFuturo/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblPosicaoFuturo>> GetTblPosicaoFuturo(DateTime id)
        //{
        //    var tblPosicaoFuturo = await _context.TblPosicaoFuturo.FindAsync(id);

        //    if (tblPosicaoFuturo == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblPosicaoFuturo;
        //}

        //// PUT: api/PosicaoFuturo/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblPosicaoFuturo(DateTime id, TblPosicaoFuturo tblPosicaoFuturo)
        //{
        //    if (id != tblPosicaoFuturo.DataRef)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblPosicaoFuturo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblPosicaoFuturoExists(id))
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

        //// POST: api/PosicaoFuturo
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblPosicaoFuturo>> PostTblPosicaoFuturo(TblPosicaoFuturo tblPosicaoFuturo)
        //{
        //    _context.TblPosicaoFuturo.Add(tblPosicaoFuturo);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblPosicaoFuturoExists(tblPosicaoFuturo.DataRef))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblPosicaoFuturo", new { id = tblPosicaoFuturo.DataRef }, tblPosicaoFuturo);
        //}

        //// DELETE: api/PosicaoFuturo/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblPosicaoFuturo(DateTime id)
        //{
        //    var tblPosicaoFuturo = await _context.TblPosicaoFuturo.FindAsync(id);
        //    if (tblPosicaoFuturo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblPosicaoFuturo.Remove(tblPosicaoFuturo);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblPosicaoFuturoExists(DateTime id)
        //{
        //    return _context.TblPosicaoFuturo.Any(e => e.DataRef == id);
        //}
    }
}
