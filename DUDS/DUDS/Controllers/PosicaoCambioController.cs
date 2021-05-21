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
    public class PosicaoCambioController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicaoCambioController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoCambio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoCambio>>> GetTblPosicaoCambio([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoCambio = await _context.TblPosicaoCambio.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoCambio == null)
            {
                NotFound();
            }

            return posicaoCambio;
        }

        //// GET: api/PosicaoCambio/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblPosicaoCambio>> GetTblPosicaoCambio(DateTime id)
        //{
        //    var tblPosicaoCambio = await _context.TblPosicaoCambio.FindAsync(id);

        //    if (tblPosicaoCambio == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblPosicaoCambio;
        //}

        //// PUT: api/PosicaoCambio/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblPosicaoCambio(DateTime id, TblPosicaoCambio tblPosicaoCambio)
        //{
        //    if (id != tblPosicaoCambio.DataRef)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblPosicaoCambio).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblPosicaoCambioExists(id))
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

        //// POST: api/PosicaoCambio
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblPosicaoCambio>> PostTblPosicaoCambio(TblPosicaoCambio tblPosicaoCambio)
        //{
        //    _context.TblPosicaoCambio.Add(tblPosicaoCambio);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblPosicaoCambioExists(tblPosicaoCambio.DataRef))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblPosicaoCambio", new { id = tblPosicaoCambio.DataRef }, tblPosicaoCambio);
        //}

        //// DELETE: api/PosicaoCambio/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblPosicaoCambio(DateTime id)
        //{
        //    var tblPosicaoCambio = await _context.TblPosicaoCambio.FindAsync(id);
        //    if (tblPosicaoCambio == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblPosicaoCambio.Remove(tblPosicaoCambio);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblPosicaoCambioExists(DateTime id)
        //{
        //    return _context.TblPosicaoCambio.Any(e => e.DataRef == id);
        //}
    }
}
