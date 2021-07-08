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
    public class ContasController : ControllerBase
    {
        private readonly DataContext _context;

        public ContasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Contas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContas>>> Contas()
        {
            return await _context.TblContas.ToListAsync();
        }

        // GET: api/Contas/5
        [HttpGet]
        public async Task<ActionResult<TblContas>> GetContas(int codFundo, int codTipoConta)
        {
            var tblContas = await _context.TblContas.FindAsync(codFundo, codTipoConta);

            if (tblContas == null)
            {
                return NotFound();
            }

            return tblContas;
        }

        [HttpPost]
        public async Task<ActionResult<ContaModel>> CadastrarConta(ContaModel tblContaModel)
        {
            var itensConta = new TblContas
            {
                CodFundo = tblContaModel.CodFundo,
                CodTipoConta = tblContaModel.CodTipoConta,
                Banco = tblContaModel.Banco,
                Agencia = tblContaModel.Agencia,
                Conta = tblContaModel.Conta,
                UsuarioModificacao = tblContaModel.UsuarioModificacao
            };

            _context.TblContas.Add(itensConta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetContas),
                new { codFundo = itensConta.CodFundo,
                      codTipoConta = itensConta.CodTipoConta },
                Ok(itensConta));
        }

        // GET: api/TipoContas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoConta>>> TipoContas()
        {
            return await _context.TblTipoConta.ToListAsync();
        }

        // GET: api/TipoContas/5
        [HttpGet]
        public async Task<ActionResult<TblTipoConta>> GetTipoContas(int id)
        {
            var tblContas = await _context.TblTipoConta.FindAsync(id);

            if (tblContas == null)
            {
                return NotFound();
            }

            return tblContas;
        }

        [HttpPost]
        public async Task<ActionResult<TipoContaModel>> CadastrarTipoConta(TipoContaModel tblTipoContaModel)
        {
            var itensTipoConta = new TblTipoConta
            {
                Id = tblTipoContaModel.Id,
                TipoConta = tblTipoContaModel.TipoConta,
                DescricaoConta = tblTipoContaModel.DescricaoConta,
                UsuarioModificacao = tblTipoContaModel.UsuarioModificacao
            };

            _context.TblTipoConta.Add(itensTipoConta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetContas),
                new
                {
                    Id = itensTipoConta.Id,
                },
                Ok(itensTipoConta));
        }

        // PUT: api/Contas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTblContas(int id, TblContas tblContas)
        //{
        //    if (id != tblContas.CodFundo)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tblContas).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TblContasExists(id))
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

        //// POST: api/Contas
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TblContas>> PostTblContas(TblContas tblContas)
        //{
        //    _context.TblContas.Add(tblContas);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TblContasExists(tblContas.CodFundo))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTblContas", new { id = tblContas.CodFundo }, tblContas);
        //}

        //// DELETE: api/Contas/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTblContas(int id)
        //{
        //    var tblContas = await _context.TblContas.FindAsync(id);
        //    if (tblContas == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TblContas.Remove(tblContas);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TblContasExists(int id)
        //{
        //    return _context.TblContas.Any(e => e.CodFundo == id);
        //}
    }
}
