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
    public class ClienteController : ControllerBase
    {
        private readonly DataContext _context;

        public ClienteController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCliente>>> GetTblCliente()
        {
            return await _context.TblCliente.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCliente>> GetTblCliente(long id)
        {
            var tblCliente = await _context.TblCliente.FindAsync(id);

            if (tblCliente == null)
            {
                return NotFound();
            }

            return tblCliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCliente(long id, TblCliente tblCliente)
        {
            if (id != tblCliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblCliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblCliente>> PostTblCliente(TblCliente tblCliente)
        {
            _context.TblCliente.Add(tblCliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblClienteExists(tblCliente.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblCliente", new { id = tblCliente.Id }, tblCliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCliente(long id)
        {
            var tblCliente = await _context.TblCliente.FindAsync(id);
            if (tblCliente == null)
            {
                return NotFound();
            }

            _context.TblCliente.Remove(tblCliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblClienteExists(long id)
        {
            return _context.TblCliente.Any(e => e.Id == id);
        }
    }
}
