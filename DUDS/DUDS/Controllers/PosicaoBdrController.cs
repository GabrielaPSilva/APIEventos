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
    public class PosicaoBdrController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicaoBdrController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoBdr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoBdr>>> GetTblPosicaoBdr([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoBdr = await _context.TblPosicaoBdr.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();

            if (posicaoBdr == null)
            {
                NotFound();
            }
            return posicaoBdr;
        }



        //// GET: api/PosicaoBdr/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblPosicaoBdr>> GetTblPosicaoBdr(DateTime id)
        //{
        //    var tblPosicaoBdr = await _context.TblPosicaoBdr.FindAsync(id);

        //    if (tblPosicaoBdr == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblPosicaoBdr;
        //}

        //// PUT: api/PosicaoBdr/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblPosicaoBdr(DateTime id, TblPosicaoBdr tblPosicaoBdr)
        //{
        //    if (id != tblPosicaoBdr.DataRef)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblPosicaoBdr).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblPosicaoBdrExists(id))
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

        //// POST: api/PosicaoBdr
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblPosicaoBdr>> PostTblPosicaoBdr(TblPosicaoBdr tblPosicaoBdr)
        //{
        //    _context.TblPosicaoBdr.Add(tblPosicaoBdr);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblPosicaoBdrExists(tblPosicaoBdr.DataRef))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblPosicaoBdr", new { id = tblPosicaoBdr.DataRef }, tblPosicaoBdr);
        //}

        //// DELETE: api/PosicaoBdr/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblPosicaoBdr(DateTime id)
        //{
        //    var tblPosicaoBdr = await _context.TblPosicaoBdr.FindAsync(id);
        //    if (tblPosicaoBdr == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblPosicaoBdr.Remove(tblPosicaoBdr);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblPosicaoBdrExists(DateTime id)
        //{
        //    return _context.TblPosicaoBdr.Any(e => e.DataRef == id);
        //}
    }
}
