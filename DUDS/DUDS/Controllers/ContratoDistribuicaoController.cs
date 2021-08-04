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
    public class ContratoDistribuicaoController : ControllerBase
    {
        private readonly DataContext _context;

        public ContratoDistribuicaoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ContratoDistribuicao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoDistribuicao>>> GetTblContratoDistribuicao()
        {
            return await _context.TblContratoDistribuicao.ToListAsync();
        }

        // GET: api/ContratoDistribuicao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoDistribuicao>> GetTblContratoDistribuicao(int id)
        {
            var tblContratoDistribuicao = await _context.TblContratoDistribuicao.FindAsync(id);

            if (tblContratoDistribuicao == null)
            {
                return NotFound();
            }

            return tblContratoDistribuicao;
        }

        // PUT: api/ContratoDistribuicao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblContratoDistribuicao(int id, TblContratoDistribuicao tblContratoDistribuicao)
        {
            if (id != tblContratoDistribuicao.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblContratoDistribuicao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblContratoDistribuicaoExists(id))
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

        // POST: api/ContratoDistribuicao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblContratoDistribuicao>> PostTblContratoDistribuicao(TblContratoDistribuicao tblContratoDistribuicao)
        {
            _context.TblContratoDistribuicao.Add(tblContratoDistribuicao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblContratoDistribuicao", new { id = tblContratoDistribuicao.Id }, tblContratoDistribuicao);
        }

        // DELETE: api/ContratoDistribuicao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblContratoDistribuicao(int id)
        {
            var tblContratoDistribuicao = await _context.TblContratoDistribuicao.FindAsync(id);
            if (tblContratoDistribuicao == null)
            {
                return NotFound();
            }

            _context.TblContratoDistribuicao.Remove(tblContratoDistribuicao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblContratoDistribuicaoExists(int id)
        {
            return _context.TblContratoDistribuicao.Any(e => e.Id == id);
        }
    }
}
