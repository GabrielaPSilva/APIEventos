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
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly DataContext _context;

        public ContratoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Contrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContrato>>> Contrato()
        {
            return await _context.TblContrato.ToListAsync();
        }

        // GET: api/Contrato/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContrato>> GetContrato(int id)
        {
            var tblContrato = await _context.TblContrato.FindAsync(id);

            if (tblContrato == null)
            {
                return NotFound();
            }

            return tblContrato;
        }

        // PUT: api/Contrato/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblContrato(int id, TblContrato tblContrato)
        //{
        //    if (id != tblContrato.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblContrato).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblContratoExists(id))
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

        //// POST: api/Contrato
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblContrato>> PostTblContrato(TblContrato tblContrato)
        //{
        //    _context.TblContrato.Add(tblContrato);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTblContrato", new { id = tblContrato.Id }, tblContrato);
        //}

        //// DELETE: api/Contrato/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblContrato(int id)
        //{
        //    var tblContrato = await _context.TblContrato.FindAsync(id);
        //    if (tblContrato == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblContrato.Remove(tblContrato);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblContratoExists(int id)
        //{
        //    return _context.TblContrato.Any(e => e.Id == id);
        //}
    }
}
