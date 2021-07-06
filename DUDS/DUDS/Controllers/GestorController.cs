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
    public class GestorController : ControllerBase
    {
        private readonly DataContext _context;

        public GestorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Gestor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGestor>>> Gestor()
        {
            return await _context.TblGestor.ToListAsync();
        }

        // GET: api/Gestor/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGestor>> GetGestor(int id)
        {
            var tblGestor = await _context.TblGestor.FindAsync(id);

            if (tblGestor == null)
            {
                return NotFound();
            }

            return tblGestor;
        }

        // PUT: api/Gestor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblGestor(int id, TblGestor tblGestor)
        //{
        //    if (id != tblGestor.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblGestor).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblGestorExists(id))
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

        //// POST: api/Gestor
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblGestor>> PostTblGestor(TblGestor tblGestor)
        //{
        //    _context.TblGestor.Add(tblGestor);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTblGestor", new { id = tblGestor.Id }, tblGestor);
        //}

        //// DELETE: api/Gestor/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblGestor(int id)
        //{
        //    var tblGestor = await _context.TblGestor.FindAsync(id);
        //    if (tblGestor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblGestor.Remove(tblGestor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblGestorExists(int id)
        //{
        //    return _context.TblGestor.Any(e => e.Id == id);
        //}
    }
}
