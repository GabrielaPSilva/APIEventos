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
    public class GestorController : ControllerBase
    {
        private readonly DataContext _context;

        public GestorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Gestor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGestor>>> Gestor()
        {
            return await _context.TblGestor.Where(c => c.Ativo == true).OrderBy(c => c.NomeGestor).AsNoTracking().ToListAsync();
        }

        // GET: api/Gestor/id
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


        //PUT: api/Gestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarGestor(int id, GestorModel gestor)
        {
            try
            {
                var registroGestor = _context.TblGestor.Find(id);

                if (registroGestor != null)
                {
                    registroGestor.NomeGestor = gestor.NomeGestor == null ? registroGestor.NomeGestor : gestor.NomeGestor;
                    registroGestor.Cnpj = gestor.Cnpj == null ? registroGestor.Cnpj : gestor.Cnpj;
                    registroGestor.CodGestorAdm = (gestor.CodGestorAdm == null || gestor.CodGestorAdm == 0) ? registroGestor.CodGestorAdm : gestor.CodGestorAdm;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException) when (!GestorExists(gestor.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Gestor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarGestor(int id)
        {
            var tblGestor = await _context.TblGestor.FindAsync(id);

            if (tblGestor == null)
            {
                return NotFound();
            }

            _context.TblGestor.Remove(tblGestor);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DESATIVA: api/Gestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarGestor(int id)
        {
            var registroGestor = _context.TblGestor.Find(id);

            if (registroGestor != null)
            {
                registroGestor.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        private bool GestorExists(int id)
        {
            return _context.TblGestor.Any(e => e.Id == id);
        }
    }
}
