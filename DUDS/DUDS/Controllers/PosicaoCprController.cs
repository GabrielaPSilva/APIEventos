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
    public class PosicaoCprController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicaoCprController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoCpr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoCpr>>> GetTblPosicaoCpr([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoCpr = await _context.TblPosicaoCpr.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoCpr == null)
            {
                NotFound();
            }

            return posicaoCpr;

            //// GET: api/PosicaoCpr/5
            //[HttpGet("{id}")]
            //public async Task<ActionResult<TblPosicaoCpr>> GetTblPosicaoCpr(int id)
            //{
            //    var tblPosicaoCpr = await _context.TblPosicaoCpr.FindAsync(id);

            //    if (tblPosicaoCpr == null)
            //    {
            //        return NotFound();
            //    }

            //    return tblPosicaoCpr;
            //}

            //// PUT: api/PosicaoCpr/5
            //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            //[HttpPut("{id}")]
            //public async Task<IActionResult> PutTblPosicaoCpr(int id, TblPosicaoCpr tblPosicaoCpr)
            //{
            //    if (id != tblPosicaoCpr.Id)
            //    {
            //        return BadRequest();
            //    }

            //    _context.Entry(tblPosicaoCpr).State = EntityState.Modified;

            //    try
            //    {
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!TblPosicaoCprExists(id))
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

            //// POST: api/PosicaoCpr
            //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            //[HttpPost]
            //public async Task<ActionResult<TblPosicaoCpr>> PostTblPosicaoCpr(TblPosicaoCpr tblPosicaoCpr)
            //{
            //    _context.TblPosicaoCpr.Add(tblPosicaoCpr);
            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetTblPosicaoCpr", new { id = tblPosicaoCpr.Id }, tblPosicaoCpr);
            //}

            //// DELETE: api/PosicaoCpr/5
            //[HttpDelete("{id}")]
            //public async Task<IActionResult> DeleteTblPosicaoCpr(int id)
            //{
            //    var tblPosicaoCpr = await _context.TblPosicaoCpr.FindAsync(id);
            //    if (tblPosicaoCpr == null)
            //    {
            //        return NotFound();
            //    }

            //    _context.TblPosicaoCpr.Remove(tblPosicaoCpr);
            //    await _context.SaveChangesAsync();

            //    return NoContent();
            //}

            //private bool TblPosicaoCprExists(int id)
            //{
            //    return _context.TblPosicaoCpr.Any(e => e.Id == id);
            //}
        }
    }
}

