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
    public class GestorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public GestorController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/Gestor/Gestor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGestor>>> Gestor()
        {
            try
            {
                List<TblGestor> gestores = await _context.TblGestor.Where(c => c.Ativo == true).OrderBy(c => c.NomeGestor).AsNoTracking().ToListAsync();

                if (gestores.Count() == 0)
                {
                    return NotFound();
                }

                if (gestores != null)
                {
                    return Ok(gestores);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e);
            }
        }

        // GET: api/Gestor/GetGestor/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGestor>> GetGestor(int id)
        {
            TblGestor tblGestor = await _context.TblGestor.FindAsync(id);

            try
            {
                if (tblGestor != null)
                {
                    return Ok(tblGestor);
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

        //POST: api/Gestor/CadastrarGestor/GestorModel
        [HttpPost]
        public async Task<ActionResult<GestorModel>> CadastrarGestor(GestorModel tblGestorModel)
        {
            TblGestor itensGestor = new TblGestor
            {
                NomeGestor = tblGestorModel.NomeGestor,
                Cnpj = tblGestorModel.Cnpj,
                UsuarioModificacao = tblGestorModel.UsuarioModificacao
            };

            try
            {
                _context.TblGestor.Add(itensGestor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetGestor),
                    new { id = itensGestor.Id },
                    Ok(itensGestor));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Gestor/EditarGestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarGestor(int id, GestorModel gestor)
        {
            try
            {
                TblGestor registroGestor = _context.TblGestor.Find(id);

                if (registroGestor != null)
                {
                    registroGestor.NomeGestor = gestor.NomeGestor == null ? registroGestor.NomeGestor : gestor.NomeGestor;
                    registroGestor.Cnpj = gestor.Cnpj == null ? registroGestor.Cnpj : gestor.Cnpj;
                    registroGestor.UsuarioModificacao = gestor.UsuarioModificacao == null ? registroGestor.UsuarioModificacao : gestor.UsuarioModificacao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroGestor);
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
            catch (DbUpdateConcurrencyException e) when (!GestorExists(gestor.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/Gestor/DeletarGestor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarGestor(int id)
        {
            var tblGestor = await _context.TblGestor.FindAsync(id);

            if (tblGestor == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblGestor.Remove(tblGestor);
                await _context.SaveChangesAsync();
                return Ok(tblGestor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DESATIVA: api/Gestor/DesativarGestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarGestor(int id)
        {
            TblGestor registroGestor = _context.TblGestor.Find(id);

            if (registroGestor != null)
            {
                registroGestor.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroGestor);
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
        
        private bool GestorExists(int id)
        {
            return _context.TblGestor.Any(e => e.Id == id);
        }
    }
}
