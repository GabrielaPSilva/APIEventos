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
    [Route("api/[controller]")]
    [ApiController]
    public class AcordoDistribuicaosController : ControllerBase
    {
        private readonly DataContext _context;

        public AcordoDistribuicaosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/AcordoDistribuicaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAcordoDistribuicao>>> GetTblAcordoDistribuicao()
        {
            return await _context.TblAcordoDistribuicao.ToListAsync();
        }

        // GET: api/AcordoDistribuicaos/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TblAcordoDistribuicao>> GetTblAcordoDistribuicao(long id)
        //{
        //    var tblAcordoDistribuicao = await _context.TblAcordoDistribuicao.FindAsync(id);

        //    if (tblAcordoDistribuicao == null)
        //    {
        //        return NotFound();
        //    }

        //    return tblAcordoDistribuicao;
        //}

        //// PUT: api/AcordoDistribuicaos/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblAcordoDistribuicao(long id, TblAcordoDistribuicao tblAcordoDistribuicao)
        //{
        //    if (id != tblAcordoDistribuicao.CodCliente)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblAcordoDistribuicao).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblAcordoDistribuicaoExists(id))
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

        //// POST: api/AcordoDistribuicaos
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblAcordoDistribuicao>> PostTblAcordoDistribuicao(TblAcordoDistribuicao tblAcordoDistribuicao)
        //{
        //    _context.TblAcordoDistribuicao.Add(tblAcordoDistribuicao);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblAcordoDistribuicaoExists(tblAcordoDistribuicao.CodCliente))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblAcordoDistribuicao", new { id = tblAcordoDistribuicao.CodCliente }, tblAcordoDistribuicao);
        //}

        //// DELETE: api/AcordoDistribuicaos/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblAcordoDistribuicao(long id)
        //{
        //    var tblAcordoDistribuicao = await _context.TblAcordoDistribuicao.FindAsync(id);
        //    if (tblAcordoDistribuicao == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblAcordoDistribuicao.Remove(tblAcordoDistribuicao);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblAcordoDistribuicaoExists(long id)
        //{
        //    return _context.TblAcordoDistribuicao.Any(e => e.CodCliente == id);
        //}
    }
}
