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
    public class DistribuidorAdministradorController : ControllerBase
    {
        private readonly DataContext _context;

        public DistribuidorAdministradorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DistribuidorAdministrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDistribuidorAdministrador>>> GetTblDistribuidorAdministrador()
        {
            return await _context.TblDistribuidorAdministrador.ToListAsync();
        }

        // GET: api/DistribuidorAdministrador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblDistribuidorAdministrador>> GetTblDistribuidorAdministrador(int id)
        {
            var tblDistribuidorAdministrador = await _context.TblDistribuidorAdministrador.FindAsync(id);

            if (tblDistribuidorAdministrador == null)
            {
                return NotFound();
            }

            return tblDistribuidorAdministrador;
        }

        // PUT: api/DistribuidorAdministrador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblDistribuidorAdministrador(int id, TblDistribuidorAdministrador tblDistribuidorAdministrador)
        {
            if (id != tblDistribuidorAdministrador.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblDistribuidorAdministrador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblDistribuidorAdministradorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DistribuidorAdministrador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblDistribuidorAdministrador>> PostTblDistribuidorAdministrador(TblDistribuidorAdministrador tblDistribuidorAdministrador)
        {
            _context.TblDistribuidorAdministrador.Add(tblDistribuidorAdministrador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblDistribuidorAdministrador", new { id = tblDistribuidorAdministrador.Id }, tblDistribuidorAdministrador);
        }

        // DELETE: api/DistribuidorAdministrador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblDistribuidorAdministrador(int id)
        {
            var tblDistribuidorAdministrador = await _context.TblDistribuidorAdministrador.FindAsync(id);
            if (tblDistribuidorAdministrador == null)
            {
                return NotFound();
            }

            _context.TblDistribuidorAdministrador.Remove(tblDistribuidorAdministrador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblDistribuidorAdministradorExists(int id)
        {
            return _context.TblDistribuidorAdministrador.Any(e => e.Id == id);
        }
    }
}
