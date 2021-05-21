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
    public class PosicaoOpcaoAcaoController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicaoOpcaoAcaoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PosicaoOpcaoAcao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPosicaoOpcaoAcao>>> GetTblPosicaoOpcaoAcao([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
        {
            if (dataPosicaoFim == null)
            {
                dataPosicaoFim = dataPosicaoInicio;
            }
            var posicaoOpcaoAcao = await _context.TblPosicaoOpcaoAcao.AsNoTracking()
                //.Include(p => p.CodFundoNavigation)
                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
                .ToListAsync();
            if (posicaoOpcaoAcao == null)
            {
                NotFound();
            }
            return posicaoOpcaoAcao;
        }

        //// GET: api/PosicaoOpcaoAcao/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblPosicaoOpcaoAcao>> GetTblPosicaoOpcaoAcao(DateTime id)
        //{
        //    var tblPosicaoOpcaoAcao = await _context.TblPosicaoOpcaoAcao.FindAsync(id);

        //    if (tblPosicaoOpcaoAcao == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblPosicaoOpcaoAcao;
        //}

        //// PUT: api/PosicaoOpcaoAcao/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblPosicaoOpcaoAcao(DateTime id, TblPosicaoOpcaoAcao tblPosicaoOpcaoAcao)
        //{
        //    if (id != tblPosicaoOpcaoAcao.DataRef)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblPosicaoOpcaoAcao).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblPosicaoOpcaoAcaoExists(id))
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

        //// POST: api/PosicaoOpcaoAcao
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblPosicaoOpcaoAcao>> PostTblPosicaoOpcaoAcao(TblPosicaoOpcaoAcao tblPosicaoOpcaoAcao)
        //{
        //    _context.TblPosicaoOpcaoAcao.Add(tblPosicaoOpcaoAcao);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblPosicaoOpcaoAcaoExists(tblPosicaoOpcaoAcao.DataRef))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblPosicaoOpcaoAcao", new { id = tblPosicaoOpcaoAcao.DataRef }, tblPosicaoOpcaoAcao);
        //}

        //// DELETE: api/PosicaoOpcaoAcao/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblPosicaoOpcaoAcao(DateTime id)
        //{
        //    var tblPosicaoOpcaoAcao = await _context.TblPosicaoOpcaoAcao.FindAsync(id);
        //    if (tblPosicaoOpcaoAcao == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblPosicaoOpcaoAcao.Remove(tblPosicaoOpcaoAcao);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblPosicaoOpcaoAcaoExists(DateTime id)
        //{
        //    return _context.TblPosicaoOpcaoAcao.Any(e => e.DataRef == id);
        //}
    }
}
