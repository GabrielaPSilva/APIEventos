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
    public class AdministradorController : ControllerBase
    {
        private readonly DataContext _context;

        public AdministradorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Administrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAdministrador>>> Administrador()
        {
            return await _context.TblAdministrador.ToListAsync();
        }

        // GET: api/Administrador/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAdministrador>> GetAdministrador(int id)
        {
            var tblAdministrador = await _context.TblAdministrador.FindAsync(id);

            if (tblAdministrador == null)
            {
                return NotFound();
            }

            return tblAdministrador;
        }

        // PUT: api/Administrador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblAdministrador(int id, TblAdministrador tblAdministrador)
        //{
        //    if (id != tblAdministrador.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblAdministrador).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblAdministradorExists(id))
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

        //// POST: api/Administrador
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblAdministrador>> PostTblAdministrador(TblAdministrador tblAdministrador)
        //{
        //    _context.TblAdministrador.Add(tblAdministrador);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTblAdministrador", new { id = tblAdministrador.Id }, tblAdministrador);
        //}

        //// DELETE: api/Administrador/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblAdministrador(int id)
        //{
        //    var tblAdministrador = await _context.TblAdministrador.FindAsync(id);
        //    if (tblAdministrador == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblAdministrador.Remove(tblAdministrador);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblAdministradorExists(int id)
        //{
        //    return _context.TblAdministrador.Any(e => e.Id == id);
        //}
    }
}
