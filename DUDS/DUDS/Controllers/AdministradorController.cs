using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using DUDS.Service.Interface;
using Newtonsoft.Json;

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public AdministradorController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/Administrador/GetAdministrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAdministrador>>> GetAdministrador()
        {
            try
            {
                List<TblAdministrador> administradores = await _context.TblAdministrador.Where(c => c.Ativo == true).OrderBy(c => c.NomeAdministrador).AsNoTracking().ToListAsync();

                if (administradores.Count == 0)
                {
                    return NotFound();
                }

                return Ok(administradores);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Administrador/GetAdministradorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAdministrador>> GetAdministradorById(int id)
        {
            try
            {
                TblAdministrador tblAdministrador = await _context.TblAdministrador.FindAsync(id);
                if (tblAdministrador == null)
                {
                    return NotFound();
                }

                return Ok(tblAdministrador);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Administrador/GetAdministradorExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<TblAdministrador>> GetAdministradorExistsBase(string cnpj)
        {
            TblAdministrador tblAdministrador = new TblAdministrador();

            try
            {
                tblAdministrador = await _context.TblAdministrador.Where(c => c.Ativo == false && c.Cnpj == cnpj).FirstOrDefaultAsync();
                if (tblAdministrador == null)
                {
                    return NotFound();
                }

                tblAdministrador = await _context.TblAdministrador.Where(c => c.Cnpj == cnpj).FirstOrDefaultAsync();
                if (tblAdministrador == null)
                {
                    return NotFound();
                }

                return Ok(tblAdministrador);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //POST: api/Administrador/AddAdministrador/AdministradorModel
        [HttpPost]
        public async Task<ActionResult<AdministradorModel>> AddAdministrador(AdministradorModel tblAdministradorModel)
        {
            TblAdministrador itensAdministrador = new TblAdministrador
            {
                NomeAdministrador = tblAdministradorModel.NomeAdministrador,
                Cnpj = tblAdministradorModel.Cnpj,
                UsuarioModificacao = tblAdministradorModel.UsuarioModificacao
            };

            try
            {
                _context.TblAdministrador.Add(itensAdministrador);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                   nameof(GetAdministrador),
                   new { id = itensAdministrador.Id },
                   Ok(itensAdministrador));
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Administrador/UpdateAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdministrador(int id, AdministradorModel administrador)
        {
            try
            {
                TblAdministrador registroAdministrador = await _context.TblAdministrador.FindAsync(id);

                if (registroAdministrador != null)
                {
                    registroAdministrador.NomeAdministrador = administrador.NomeAdministrador == null ? registroAdministrador.NomeAdministrador : administrador.NomeAdministrador;
                    registroAdministrador.Cnpj = administrador.Cnpj == null ? registroAdministrador.Cnpj : administrador.Cnpj;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroAdministrador);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.InnerException.Message);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException e) when (!AdministradorExists(administrador.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/Administrador/DeleteAdministrador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrador(int id)
        {
            TblAdministrador tblAdministrador = await _context.TblAdministrador.FindAsync(id);

            if (tblAdministrador == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblAdministrador.Remove(tblAdministrador);
                await _context.SaveChangesAsync();
                return Ok(tblAdministrador);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DESATIVA: api/Administrador/DisableAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableAdministrador(int id)
        {
            TblAdministrador registroAdministrador = await _context.TblAdministrador.FindAsync(id);

            if (registroAdministrador != null)
            {
                registroAdministrador.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroAdministrador);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // ATIVAR: api/Administrador/ActivateAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateAdministrador(int id)
        {
            TblAdministrador registroAdministrador = await _context.TblAdministrador.FindAsync(id);

            if (registroAdministrador != null)
            {
                registroAdministrador.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroAdministrador);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
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
