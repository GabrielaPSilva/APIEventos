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

        // GET: api/Administrador/Administrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAdministrador>>> Administrador()
        {
            try
            {
                List<TblAdministrador> administradores = await _context.TblAdministrador.Where(c => c.Ativo == true).OrderBy(c => c.NomeAdministrador).AsNoTracking().ToListAsync();

                if (administradores.Count() == 0)
                {
                    return NotFound();
                }

                if (administradores != null)
                {
                    return Ok(administradores);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        // GET: api/Administrador/GetAdministrador/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAdministrador>> GetAdministrador(int id)
        {
            TblAdministrador tblAdministrador = await _context.TblAdministrador.FindAsync(id);

            try
            {
                if (tblAdministrador != null)
                {
                    return Ok(tblAdministrador);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Administrador/CadastrarAdministrador/AdministradorModel
        [HttpPost]
        public async Task<ActionResult<AdministradorModel>> CadastrarAdministrador(AdministradorModel tblAdministradorModel)
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
                return BadRequest(e);
            }
        }

        //PUT: api/Administrador/EditarAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarAdministrador(int id, AdministradorModel administrador)
        {
            try
            {
                TblAdministrador registroAdministrador = _context.TblAdministrador.Find(id);

                if (registroAdministrador != null)
                {
                    registroAdministrador.NomeAdministrador = administrador.NomeAdministrador == null ? registroAdministrador.NomeAdministrador : administrador.NomeAdministrador;
                    registroAdministrador.Cnpj = administrador.Cnpj == null ? registroAdministrador.Cnpj : administrador.Cnpj;
                    registroAdministrador.UsuarioModificacao = administrador.UsuarioModificacao == null ? registroAdministrador.UsuarioModificacao : administrador.UsuarioModificacao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroAdministrador);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException e) when (!AdministradorExists(administrador.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/Administrador/DeletarAdministrador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAdministrador(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_administrador");

            if (!existeRegistro)
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
                    return BadRequest(e);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // DESATIVA: api/Administrador/DesativarAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarAdministrador(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_administrador");

            if (!existeRegistro)
            {
                TblAdministrador registroAdministrador = _context.TblAdministrador.Find(id);

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
                        return BadRequest(e);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private bool AdministradorExists(int id)
        {
            return _context.TblAdministrador.Any(e => e.Id == id);
        }
    }
}
