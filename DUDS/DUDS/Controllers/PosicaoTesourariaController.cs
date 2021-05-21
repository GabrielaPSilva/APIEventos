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
    public class PosicaoTesourariaController : ControllerBase
    {
        private readonly DataContext _context;
        public PosicaoTesourariaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoTesouraria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoTesouraria>>> GetTblPosicaoTesouraria([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoTesouraria = await _context.TblPosicaoTesouraria.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoTesouraria == null)
            {
                NotFound();
            }
            return posicaoTesouraria;
        }

        //// GET: api/PosicaoTesouraria/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblPosicaoTesouraria>> GetTblPosicaoTesouraria(DateTime id)
        //{
        //    var tblPosicaoTesouraria = await _context.TblPosicaoTesouraria.FindAsync(id);

        //    if (tblPosicaoTesouraria == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblPosicaoTesouraria;
        //}

        //// PUT: api/PosicaoTesouraria/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblPosicaoTesouraria(DateTime id, TblPosicaoTesouraria tblPosicaoTesouraria)
        //{
        //    if (id != tblPosicaoTesouraria.DataRef)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblPosicaoTesouraria).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblPosicaoTesourariaExists(id))
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

        //// POST: api/PosicaoTesouraria
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblPosicaoTesouraria>> PostTblPosicaoTesouraria(TblPosicaoTesouraria tblPosicaoTesouraria)
        //{
        //    _context.TblPosicaoTesouraria.Add(tblPosicaoTesouraria);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblPosicaoTesourariaExists(tblPosicaoTesouraria.DataRef))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblPosicaoTesouraria", new { id = tblPosicaoTesouraria.DataRef }, tblPosicaoTesouraria);
        //}

        //// DELETE: api/PosicaoTesouraria/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblPosicaoTesouraria(DateTime id)
        //{
        //    var tblPosicaoTesouraria = await _context.TblPosicaoTesouraria.FindAsync(id);
        //    if (tblPosicaoTesouraria == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblPosicaoTesouraria.Remove(tblPosicaoTesouraria);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblPosicaoTesourariaExists(DateTime id)
        //{
        //    return _context.TblPosicaoTesouraria.Any(e => e.DataRef == id);
        //}
    }
}
