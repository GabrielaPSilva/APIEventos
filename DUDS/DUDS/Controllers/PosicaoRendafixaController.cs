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
    public class PosicaoRendafixaController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicaoRendafixaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoRendafixa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoRendafixa>>> GetTblPosicaoRendafixa([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }
            var posicaoRendafixa = await _context.TblPosicaoRendafixa.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoRendafixa == null)
            {
                NotFound();
            }
            return posicaoRendafixa;
        }

        //// GET: api/PosicaoRendafixa/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblPosicaoRendafixa>> GetTblPosicaoRendafixa(DateTime id)
        //{
        //    var tblPosicaoRendafixa = await _context.TblPosicaoRendafixa.FindAsync(id);

        //    if (tblPosicaoRendafixa == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblPosicaoRendafixa;
        //}

        //// PUT: api/PosicaoRendafixa/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblPosicaoRendafixa(DateTime id, TblPosicaoRendafixa tblPosicaoRendafixa)
        //{
        //    if (id != tblPosicaoRendafixa.DataRef)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblPosicaoRendafixa).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblPosicaoRendafixaExists(id))
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

        //// POST: api/PosicaoRendafixa
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblPosicaoRendafixa>> PostTblPosicaoRendafixa(TblPosicaoRendafixa tblPosicaoRendafixa)
        //{
        //    _context.TblPosicaoRendafixa.Add(tblPosicaoRendafixa);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblPosicaoRendafixaExists(tblPosicaoRendafixa.DataRef))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblPosicaoRendafixa", new { id = tblPosicaoRendafixa.DataRef }, tblPosicaoRendafixa);
        //}

        //// DELETE: api/PosicaoRendafixa/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblPosicaoRendafixa(DateTime id)
        //{
        //    var tblPosicaoRendafixa = await _context.TblPosicaoRendafixa.FindAsync(id);
        //    if (tblPosicaoRendafixa == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblPosicaoRendafixa.Remove(tblPosicaoRendafixa);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblPosicaoRendafixaExists(DateTime id)
        //{
        //    return _context.TblPosicaoRendafixa.Any(e => e.DataRef == id);
        //}
    }
}
