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
    public class PosicaoOpcaoFuturoController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicaoOpcaoFuturoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoOpcaoFuturo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoOpcaoFuturo>>> GetTblPosicaoOpcaoFuturo([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }
            var posicaoOpcaoFuturo = await _context.TblPosicaoOpcaoFuturo.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoOpcaoFuturo == null)
            {
                NotFound();
            }
            return posicaoOpcaoFuturo;
        }

        //// GET: api/PosicaoOpcaoFuturo/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblPosicaoOpcaoFuturo>> GetTblPosicaoOpcaoFuturo(DateTime id)
        //{
        //    var tblPosicaoOpcaoFuturo = await _context.TblPosicaoOpcaoFuturo.FindAsync(id);

        //    if (tblPosicaoOpcaoFuturo == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblPosicaoOpcaoFuturo;
        //}

        //// PUT: api/PosicaoOpcaoFuturo/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblPosicaoOpcaoFuturo(DateTime id, TblPosicaoOpcaoFuturo tblPosicaoOpcaoFuturo)
        //{
        //    if (id != tblPosicaoOpcaoFuturo.DataRef)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblPosicaoOpcaoFuturo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblPosicaoOpcaoFuturoExists(id))
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

        //// POST: api/PosicaoOpcaoFuturo
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblPosicaoOpcaoFuturo>> PostTblPosicaoOpcaoFuturo(TblPosicaoOpcaoFuturo tblPosicaoOpcaoFuturo)
        //{
        //    _context.TblPosicaoOpcaoFuturo.Add(tblPosicaoOpcaoFuturo);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblPosicaoOpcaoFuturoExists(tblPosicaoOpcaoFuturo.DataRef))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblPosicaoOpcaoFuturo", new { id = tblPosicaoOpcaoFuturo.DataRef }, tblPosicaoOpcaoFuturo);
        //}

        //// DELETE: api/PosicaoOpcaoFuturo/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblPosicaoOpcaoFuturo(DateTime id)
        //{
        //    var tblPosicaoOpcaoFuturo = await _context.TblPosicaoOpcaoFuturo.FindAsync(id);
        //    if (tblPosicaoOpcaoFuturo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblPosicaoOpcaoFuturo.Remove(tblPosicaoOpcaoFuturo);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblPosicaoOpcaoFuturoExists(DateTime id)
        //{
        //    return _context.TblPosicaoOpcaoFuturo.Any(e => e.DataRef == id);
        //}
    }
}
