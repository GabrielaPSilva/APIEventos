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
    public class PosicaoCotaFundoController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicaoCotaFundoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoCotaFundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoCotaFundo>>> GetTblPosicaoCotaFundo([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            //return await _context.TblPosicaoCotaFundo.ToListAsync();

            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }

            var posicaoCotaFundo = await _context.TblPosicaoCotaFundo.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoCotaFundo == null)
            {
                NotFound();
            }

            return posicaoCotaFundo;

            
        }

        // GET: api/PosicaoCotaFundo/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblPosicaoCotaFundo>> GetTblPosicaoCotaFundo(DateTime id)
        //{
        //    var tblPosicaoCotaFundo = await _context.TblPosicaoCotaFundo.FindAsync(id);

        //    if (tblPosicaoCotaFundo == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblPosicaoCotaFundo;
        //}

        // PUT: api/PosicaoCotaFundo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblPosicaoCotaFundo(DateTime id, TblPosicaoCotaFundo tblPosicaoCotaFundo)
        //{
        //    if (id != tblPosicaoCotaFundo.DataRef)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblPosicaoCotaFundo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblPosicaoCotaFundoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        }

        // POST: api/PosicaoCotaFundo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblPosicaoCotaFundo>> PostTblPosicaoCotaFundo(TblPosicaoCotaFundo tblPosicaoCotaFundo)
        //{
        //    _context.TblPosicaoCotaFundo.Add(tblPosicaoCotaFundo);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblPosicaoCotaFundoExists(tblPosicaoCotaFundo.DataRef))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblPosicaoCotaFundo", new { id = tblPosicaoCotaFundo.DataRef }, tblPosicaoCotaFundo);
        //}

        // DELETE: api/PosicaoCotaFundo/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblPosicaoCotaFundo(DateTime id)
        //{
        //    var tblPosicaoCotaFundo = await _context.TblPosicaoCotaFundo.FindAsync(id);
        //    if (tblPosicaoCotaFundo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblPosicaoCotaFundo.Remove(tblPosicaoCotaFundo);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblPosicaoCotaFundoExists(DateTime id)
        //{
        //    return _context.TblPosicaoCotaFundo.Any(e => e.DataRef == id);
        //}
    }
