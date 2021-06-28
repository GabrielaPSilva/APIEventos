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
    public class AnbimaAcoesController : ControllerBase
    {
        private readonly DataContext _context;

        public AnbimaAcoesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/AnbimaAcoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblXmlAnbimaAcoes>>> GetTblXmlAnbimaAcoes()
        {
            return await _context.TblXmlAnbimaAcoes.ToListAsync();
        }

        // GET: api/AnbimaAcoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblXmlAnbimaAcoes>> GetTblXmlAnbimaAcoes(int id)
        {
            var tblXmlAnbimaAcoes = await _context.TblXmlAnbimaAcoes.FindAsync(id);

            if (tblXmlAnbimaAcoes == null)
            {
                return NotFound();
            }

            return tblXmlAnbimaAcoes;
        }

        // PUT: api/AnbimaAcoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblXmlAnbimaAcoes(int id, TblXmlAnbimaAcoes tblXmlAnbimaAcoes)
        {
            if (id != tblXmlAnbimaAcoes.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblXmlAnbimaAcoes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblXmlAnbimaAcoesExists(id))
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

        // POST: api/AnbimaAcoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblXmlAnbimaAcoes>> PostTblXmlAnbimaAcoes(TblXmlAnbimaAcoes tblXmlAnbimaAcoes)
        {
            _context.TblXmlAnbimaAcoes.Add(tblXmlAnbimaAcoes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblXmlAnbimaAcoes", new { id = tblXmlAnbimaAcoes.Id }, tblXmlAnbimaAcoes);
        }

        // DELETE: api/AnbimaAcoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblXmlAnbimaAcoes(int id)
        {
            var tblXmlAnbimaAcoes = await _context.TblXmlAnbimaAcoes.FindAsync(id);
            if (tblXmlAnbimaAcoes == null)
            {
                return NotFound();
            }

            _context.TblXmlAnbimaAcoes.Remove(tblXmlAnbimaAcoes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblXmlAnbimaAcoesExists(int id)
        {
            return _context.TblXmlAnbimaAcoes.Any(e => e.Id == id);
        }
    }
}
