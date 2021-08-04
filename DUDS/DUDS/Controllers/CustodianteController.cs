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
    public class CustodianteController : ControllerBase
    {
        private readonly DataContext _context;

        public CustodianteController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Custodiante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCustodiante>>> Custodiante()
        {
            return await _context.TblCustodiante.Where(c => c.Ativo == true).OrderBy(c => c.NomeCustodiante).AsNoTracking().ToListAsync();
        }

        // GET: api/Custodiante/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCustodiante>> GetCustodiante(int id)
        {
            var tblCustodiante = await _context.TblCustodiante.FindAsync(id);

            if (tblCustodiante == null)
            {
                return NotFound();
            }

            return Ok(tblCustodiante);
        }

        [HttpPost]
        public async Task<ActionResult<CustodianteModel>> CadastrarCustodiante(CustodianteModel tblCustodianteModel)
        {
            var itensCustodiante = new TblCustodiante
            {
                NomeCustodiante = tblCustodianteModel.NomeCustodiante,
                UsuarioModificacao = tblCustodianteModel.UsuarioModificacao,
                DataModificacao = tblCustodianteModel.DataModificacao
            };

            _context.TblCustodiante.Add(itensCustodiante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCustodiante),
                new { id = itensCustodiante.Id },
                Ok(itensCustodiante));
        }

        //PUT: api/Custodiante/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCustodiante(int id, CustodianteModel custodiante)
        {
            try
            {
                var registroCustodiante = _context.TblCustodiante.Find(id);

                if (registroCustodiante != null)
                {
                    registroCustodiante.NomeCustodiante = custodiante.NomeCustodiante == null ? registroCustodiante.NomeCustodiante : custodiante.NomeCustodiante;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException) when (!CustodianteExists(custodiante.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Custodiante/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCustodiante(int id)
        {
            var tblCustodiante = await _context.TblCustodiante.FindAsync(id);

            if (tblCustodiante == null)
            {
                return NotFound();
            }

            _context.TblCustodiante.Remove(tblCustodiante);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DESATIVA: api/Custodiante/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarCustodiante(int id)
        {
            var registroCustodiante = _context.TblCustodiante.Find(id);

            if (registroCustodiante != null)
            {
                registroCustodiante.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool CustodianteExists(int id)
        {
            return _context.TblCustodiante.Any(e => e.Id == id);
        }
    }
}
