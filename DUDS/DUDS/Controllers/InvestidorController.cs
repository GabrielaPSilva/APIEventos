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
    public class InvestidorController : ControllerBase
    {
        private readonly DataContext _context;

        public InvestidorController(DataContext context)
        {
            _context = context;
        }
        // GET: api/Investidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblInvestidor>>> Investidor()
        {
            return await _context.TblInvestidor.Where(c => c.Ativo == true).OrderBy(c => c.NomeCliente).AsNoTracking().ToListAsync();
        }

        // GET: api/Investidor/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGestor>> GetGestor(int id)
        {
            var tblGestor = await _context.TblGestor.FindAsync(id);

            if (tblGestor == null)
            {
                return NotFound();
            }

            return Ok(tblGestor);
        }

        [HttpPost]
        public async Task<ActionResult<GestorModel>> CadastrarGestor(GestorModel tblGestorModel)
        {
            var itensGestor = new TblGestor
            {
                NomeGestor = tblGestorModel.NomeGestor,
                Cnpj = tblGestorModel.Cnpj,
                CodGestorAdm = tblGestorModel.CodGestorAdm,
                DataModificacao = tblGestorModel.DataModificacao,
                UsuarioModificacao = tblGestorModel.UsuarioModificacao,
                Ativo = tblGestorModel.Ativo
            };

            _context.TblGestor.Add(itensGestor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetGestor),
                new { id = itensGestor.Id },
                Ok(itensGestor));
        }


        //PUT: api/Investidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarGestor(int id, InvestidorModel investidor)
        {
            try
            {
                var registroInvestidor = _context.TblInvestidor.Find(id);

                if (registroInvestidor != null)
                {
                    registroInvestidor.NomeCliente = investidor.NomeCliente == null ? registroInvestidor.NomeCliente : investidor.NomeCliente;
                    registroInvestidor.Cnpj = investidor.Cnpj == null ? registroInvestidor.Cnpj : investidor.Cnpj;
                    registroInvestidor.TipoCliente = investidor.TipoCliente == null ? registroInvestidor.TipoCliente : investidor.TipoCliente;
                    registroInvestidor.CodAdministrador = investidor.CodAdministrador == 0 ? registroInvestidor.CodAdministrador : investidor.CodAdministrador;
                    registroInvestidor.CodGestor = investidor.CodGestor == 0 ? registroInvestidor.CodGestor : investidor.CodGestor;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException) when (!InvestidorExists(investidor.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Investidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarInvestidor(int id)
        {
            var tblInvestidor = await _context.TblInvestidor.FindAsync(id);

            if (tblInvestidor == null)
            {
                return NotFound();
            }

            _context.TblInvestidor.Remove(tblInvestidor);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DESATIVA: api/Investidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarInvestidor(int id)
        {
            var registroInvestidor = _context.TblInvestidor.Find(id);

            if (registroInvestidor != null)
            {
                registroInvestidor.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool InvestidorExists(int id)
        {
            return _context.TblInvestidor.Any(e => e.Id == id);
        }
    }
}
