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
//    [Produces("application/json")]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PosicaoAcaoController : ControllerBase
//    {
//        private readonly DataContext _context;

//        public PosicaoAcaoController(DataContext context)
//        {
//            _context = context;
//        }

//        // GET: api/PosicaoAcao
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<TblPosicaoAcao>>> GetTblPosicaoAcao([FromQuery] DateTime dataPosicaoInicio, [FromQuery] int[] codFundo, [FromQuery] DateTime? dataPosicaoFim = null)
//        {
//            if (dataPosicaoFim == null)
//            {
//                dataPosicaoFim = dataPosicaoInicio;
//            }
            
//            var posicaoAcao = await _context.TblPosicaoAcao.AsNoTracking()
//                //.Include(p => p.CodFundoNavigation)
                
//                .Where(p => codFundo.Contains(p.CodFundo) && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim)
//                .ToListAsync();
//            if (posicaoAcao == null)
//            {
//                NotFound();
//            }

//            return posicaoAcao;
//            //return await _context.TblPosicaoAcao.AsNoTracking().Where(p => codFundo.Contains(p.CodFundo)  && p.DataRef >= dataPosicaoInicio && p.DataRef <= dataPosicaoFim && ( ativo.Length == 0 || ativo.Contains(p.Ativo))).ToListAsync();
//            //return await _context.TblPosicaoAcao.AsNoTracking().Where(p => p.CodFundo == codFundo && p.DataRef == dataPosicao).ToListAsync();
//            //return await _context.TblPosicaoAcao.ToListAsync();
//        }

//        /*
//        // GET: api/PosicaoAcao/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<TblPosicaoAcao>> GetTblPosicaoAcao(DateTime id)
//        {
//            var tblPosicaoAcao = await _context.TblPosicaoAcao.FindAsync(id);

//            if (tblPosicaoAcao == null)
//            {
//                return NotFound();
//            }

//            return tblPosicaoAcao;
//        }

//        // PUT: api/PosicaoAcao/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutTblPosicaoAcao(DateTime id, TblPosicaoAcao tblPosicaoAcao)
//        {
//            if (id != tblPosicaoAcao.DataRef)
//            {
//                return BadRequest();
//            }

//            _context.Entry(tblPosicaoAcao).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!TblPosicaoAcaoExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/PosicaoAcao
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<TblPosicaoAcao>> PostTblPosicaoAcao(TblPosicaoAcao tblPosicaoAcao)
//        {
//            _context.TblPosicaoAcao.Add(tblPosicaoAcao);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (TblPosicaoAcaoExists(tblPosicaoAcao.DataRef))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetTblPosicaoAcao", new { id = tblPosicaoAcao.DataRef }, tblPosicaoAcao);
//        }

//        // DELETE: api/PosicaoAcao/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteTblPosicaoAcao(DateTime id)
//        {
//            var tblPosicaoAcao = await _context.TblPosicaoAcao.FindAsync(id);
//            if (tblPosicaoAcao == null)
//            {
//                return NotFound();
//            }

//            _context.TblPosicaoAcao.Remove(tblPosicaoAcao);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool TblPosicaoAcaoExists(DateTime id)
//        {
//            return _context.TblPosicaoAcao.Any(e => e.DataRef == id);
//        }
//        */
//    }
}
