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
    public class XmlAnbimaDespesasController : ControllerBase
    {
        private readonly DataContext _context;

        public XmlAnbimaDespesasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/XmlAnbimaDespesas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblXmlAnbimaDespesas>>> GetTblXmlAnbimaDespesas()
        {
            return await _context.TblXmlAnbimaDespesas.ToListAsync();
        }

        // GET: api/XmlAnbimaDespesas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblXmlAnbimaDespesas>> GetTblXmlAnbimaDespesas(int id)
        {
            var tblXmlAnbimaDespesas = await _context.TblXmlAnbimaDespesas.FindAsync(id);

            if (tblXmlAnbimaDespesas == null)
            {
                return NotFound();
            }

            return tblXmlAnbimaDespesas;
        }

        // PUT: api/XmlAnbimaDespesas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblXmlAnbimaDespesas(int id, TblXmlAnbimaDespesas tblXmlAnbimaDespesas)
        {
            if (id != tblXmlAnbimaDespesas.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblXmlAnbimaDespesas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblXmlAnbimaDespesasExists(id))
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

        // POST: api/XmlAnbimaDespesas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblXmlAnbimaDespesas>> PostTblXmlAnbimaDespesas(TblXmlAnbimaDespesas tblXmlAnbimaDespesas)
        {
            _context.TblXmlAnbimaDespesas.Add(tblXmlAnbimaDespesas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblXmlAnbimaDespesas", new { id = tblXmlAnbimaDespesas.Id }, tblXmlAnbimaDespesas);
        }

        // DELETE: api/XmlAnbimaDespesas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblXmlAnbimaDespesas(int id)
        {
            var tblXmlAnbimaDespesas = await _context.TblXmlAnbimaDespesas.FindAsync(id);
            if (tblXmlAnbimaDespesas == null)
            {
                return NotFound();
            }

            _context.TblXmlAnbimaDespesas.Remove(tblXmlAnbimaDespesas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblXmlAnbimaDespesasExists(int id)
        {
            return _context.TblXmlAnbimaDespesas.Any(e => e.Id == id);
        }
    }
}
