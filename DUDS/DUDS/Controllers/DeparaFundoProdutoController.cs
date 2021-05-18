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
    public class DeparaFundoProdutoController : ControllerBase
    {
        private readonly DataContext _context;

        public DeparaFundoProdutoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DeparaFundoProduto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDeparaFundoproduto>>> GetTblDeparaFundoproduto()
        {
            return await _context.TblDeparaFundoproduto.ToListAsync();
        }

        //    // GET: api/DeparaFundoProduto/5
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<TblDeparaFundoproduto>> GetTblDeparaFundoproduto(int id)
        //    {
        //        var tblDeparaFundoproduto = await _context.TblDeparaFundoproduto.FindAsync(id);

        //        if (tblDeparaFundoproduto == null)
        //        {
        //            return NotFound();
        //        }

        //        return tblDeparaFundoproduto;
        //    }

        //    // PUT: api/DeparaFundoProduto/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutTblDeparaFundoproduto(int id, TblDeparaFundoproduto tblDeparaFundoproduto)
        //    {
        //        if (id != tblDeparaFundoproduto.Id)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(tblDeparaFundoproduto).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TblDeparaFundoprodutoExists(id))
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

        //    // POST: api/DeparaFundoProduto
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<TblDeparaFundoproduto>> PostTblDeparaFundoproduto(TblDeparaFundoproduto tblDeparaFundoproduto)
        //    {
        //        _context.TblDeparaFundoproduto.Add(tblDeparaFundoproduto);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetTblDeparaFundoproduto", new { id = tblDeparaFundoproduto.Id }, tblDeparaFundoproduto);
        //    }

        //    // DELETE: api/DeparaFundoProduto/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteTblDeparaFundoproduto(int id)
        //    {
        //        var tblDeparaFundoproduto = await _context.TblDeparaFundoproduto.FindAsync(id);
        //        if (tblDeparaFundoproduto == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.TblDeparaFundoproduto.Remove(tblDeparaFundoproduto);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool TblDeparaFundoprodutoExists(int id)
        //    {
        //        return _context.TblDeparaFundoproduto.Any(e => e.Id == id);
        //    }
    }
}
