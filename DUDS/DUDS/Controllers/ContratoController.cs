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
            try
            {
                var contratos = await _context.TblContrato.Where(c => c.Ativo == true).AsNoTracking().ToListAsync();

                if (contratos != null)
                {
                    return Ok(contratos);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (InvalidOperationException e)
            {
                return BadRequest();
            }
        }

        // GET: api/Contrato/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContrato>> GetContrato(int id)
        {
            var tblContrato = await _context.TblContrato.FindAsync(id);

            if (tblContrato == null)
            {
                return NotFound();
            }

            return Ok(tblContrato);
        }

        [HttpPost]
        public async Task<ActionResult<ContratoModel>> CadastrarContrato(ContratoModel tblContratoModel)
        {
            var itensContrato = new TblContrato
            {
                CodDistribuidor = tblContratoModel.CodDistribuidor,
                TipoContrato = tblContratoModel.TipoContrato,
                Versao = tblContratoModel.Versao,
                Status = tblContratoModel.Status,
                IdDocusign = tblContratoModel.IdDocusign,
                DirecaoPagamento = tblContratoModel.DirecaoPagamento,
                ClausulaRetroatividade = tblContratoModel.ClausulaRetroatividade,
                DataRetroatividade = tblContratoModel.DataRetroatividade,
                DataAssinatura = tblContratoModel.DataAssinatura,
                Ativo = tblContratoModel.Ativo,
                UsuarioModificacao = tblContratoModel.UsuarioModificacao,
                DataModificacao = tblContratoModel.DataModificacao
            };

            _context.TblContrato.Add(itensContrato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetContrato),
                new { id = itensContrato.Id },
                Ok(itensContrato));
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

        // DELETE: api/Contrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContrato(int id)
        {
            var tblContrato = await _context.TblContrato.FindAsync(id);

            if (tblContrato == null)
            {
                return NotFound();
            }

            _context.TblContrato.Remove(tblContrato);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DESATIVA: api/Contrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarContrato(int id)
        {
            var registroContrato = _context.TblContrato.Find(id);

            if (registroContrato != null)
            {
                registroContrato.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool ContratoExists(int id)
        {
            return _context.TblContrato.Any(e => e.Id == id);
        }
    }
}
