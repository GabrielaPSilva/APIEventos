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
    public class AdministradorController : ControllerBase
    {
        private readonly DataContext _context;

        public AdministradorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Administrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAdministrador>>> Administrador()
        {
            return await _context.TblAdministrador.Where(c => c.Ativo == true).OrderBy(c => c.NomeAdministrador).AsNoTracking().ToListAsync();
        }

        // GET: api/Administrador/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAdministrador>> GetAdministrador(int id)
        {
            var tblAdministrador = await _context.TblAdministrador.FindAsync(id);

            if (tblAdministrador == null)
            {
                return NotFound();
            }

            return Ok(tblAdministrador);
        }

        [HttpPost]
        public async Task<ActionResult<AdministradorModel>> CadastrarAdministrador(AdministradorModel tblAdministradorModel)
        {
            var itensAdministrador = new TblAdministrador
            {
                NomeAdministrador = tblAdministradorModel.NomeAdministrador,
                Cnpj = tblAdministradorModel.Cnpj,
                DataModificacao = tblAdministradorModel.DataModificacao,
                UsuarioModificacao = tblAdministradorModel.UsuarioModificacao,
                Ativo = tblAdministradorModel.Ativo
            };

            _context.TblAdministrador.Add(itensAdministrador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAdministrador),
                new { id = itensAdministrador.Id },
                Ok(itensAdministrador));
        }

        //PUT: api/Administrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarAdministrador(int id, AdministradorModel administrador)
        {
            try
            {
                var registroAdministrador = _context.TblAdministrador.Find(id);

                if (registroAdministrador != null)
                {
                    registroAdministrador.NomeAdministrador = administrador.NomeAdministrador == null ? registroAdministrador.NomeAdministrador : administrador.NomeAdministrador;
                    registroAdministrador.Cnpj = administrador.Cnpj == null ? registroAdministrador.Cnpj : administrador.Cnpj;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException) when (!AdministradorExists(administrador.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Administrador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAdministrador(int id)
        {
            var tblAdministrador = await _context.TblAdministrador.FindAsync(id);

            if (tblAdministrador == null)
            {
                return NotFound();
            }

            _context.TblAdministrador.Remove(tblAdministrador);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DESATIVA: api/Administrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarAdministrador(int id)
        {
            var registroAdministrador = _context.TblAdministrador.Find(id);

            if (registroAdministrador != null)
            {
                registroAdministrador.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool AdministradorExists(int id)
        {
            return _context.TblAdministrador.Any(e => e.Id == id);
        }
    }
}
