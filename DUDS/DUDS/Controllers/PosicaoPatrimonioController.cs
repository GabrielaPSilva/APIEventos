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
    public class PosicaoPatrimonioController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicaoPatrimonioController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoPatrimonio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoPatrimonio>>> GetTblPosicaoPatrimonio([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoPatrimonio = await _context.TblPosicaoPatrimonio.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoPatrimonio == null)
            {
                NotFound();
            }

            return posicaoPatrimonio;
        }

        //    // GET: api/PosicaoPatrimonio/5
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<TblPosicaoPatrimonio>> GetTblPosicaoPatrimonio(DateTime id)
        //    {
        //        var tblPosicaoPatrimonio = await _context.TblPosicaoPatrimonio.FindAsync(id);

        //        if (tblPosicaoPatrimonio == null)
        //        {
        //            return NotFound();
        //        }

        //        return tblPosicaoPatrimonio;
        //    }

        //    // PUT: api/PosicaoPatrimonio/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutTblPosicaoPatrimonio(DateTime id, TblPosicaoPatrimonio tblPosicaoPatrimonio)
        //    {
        //        if (id != tblPosicaoPatrimonio.DataRef)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(tblPosicaoPatrimonio).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TblPosicaoPatrimonioExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/PosicaoPatrimonio
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<TblPosicaoPatrimonio>> PostTblPosicaoPatrimonio(TblPosicaoPatrimonio tblPosicaoPatrimonio)
        //    {
        //        _context.TblPosicaoPatrimonio.Add(tblPosicaoPatrimonio);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            if (TblPosicaoPatrimonioExists(tblPosicaoPatrimonio.DataRef))
        //            {
        //                return Conflict();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return CreatedAtAction("GetTblPosicaoPatrimonio", new { id = tblPosicaoPatrimonio.DataRef }, tblPosicaoPatrimonio);
        //    }

        //    // DELETE: api/PosicaoPatrimonio/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteTblPosicaoPatrimonio(DateTime id)
        //    {
        //        var tblPosicaoPatrimonio = await _context.TblPosicaoPatrimonio.FindAsync(id);
        //        if (tblPosicaoPatrimonio == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.TblPosicaoPatrimonio.Remove(tblPosicaoPatrimonio);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool TblPosicaoPatrimonioExists(DateTime id)
        //    {
        //        return _context.TblPosicaoPatrimonio.Any(e => e.DataRef == id);
        //    }

    }
}
