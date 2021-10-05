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
    public class GrupoRebateController : ControllerBase
    {
        private readonly DataContext _context;

        public GrupoRebateController(DataContext context)
        {
            _context = context;
        }

        #region Grupo Rebate
        // GET: api/GrupoRebate/GetTblGrupoRebate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGrupoRebate>>> GetGrupoRebate()
        {
            try
            {
                var grupoRebate = await _context.TblGrupoRebate.Where(c => c.Ativo == true).OrderBy(c => c.NomeGrupoRebate).ToListAsync();

                if (grupoRebate.Count == 0)
                {
                    return NotFound();
                }

                return Ok(grupoRebate);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/GrupoRebate/GetTblGrupoRebateById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblGrupoRebate>> GetGrupoRebateById(int id)
        {
            try
            {
                TblGrupoRebate tblGrupoRebate = await _context.TblGrupoRebate.FindAsync(id);

                if (tblGrupoRebate == null)
                {
                    return NotFound();
                }

                return Ok(tblGrupoRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/GrupoRebate/AddGrupoRebate/GrupoRebateModel
        [HttpPost]
        public async Task<ActionResult<AdministradorModel>> AddGrupoRebate(GrupoRebateModel tblGrupoRebateModel)
        {
            TblGrupoRebate itensGrupoRebate = new TblGrupoRebate
            {
                NomeGrupoRebate = tblGrupoRebateModel.NomeGrupoRebate,
                UsuarioModificacao = tblGrupoRebateModel.UsuarioModificacao
            };

            try
            {
                _context.TblGrupoRebate.Add(itensGrupoRebate);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                   nameof(GetGrupoRebate),
                   new { id = itensGrupoRebate.Id }, itensGrupoRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/GrupoRebate/UpdateGrupoRebate/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrupoRebate(int id, GrupoRebateModel grupoRebate)
        {
            try
            {
                TblGrupoRebate registroGrupoRebate = await _context.TblGrupoRebate.FindAsync(id);

                if (registroGrupoRebate != null)
                {
                    registroGrupoRebate.NomeGrupoRebate = grupoRebate.NomeGrupoRebate == null ? registroGrupoRebate.NomeGrupoRebate : grupoRebate.NomeGrupoRebate;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroGrupoRebate);
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
            catch (DbUpdateConcurrencyException e) when (!GrupoRebateExists(grupoRebate.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/GrupoRebate/DeleteGrupoRebate/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupoRebate(int id)
        {
            TblGrupoRebate tblGrupoRebate = await _context.TblGrupoRebate.FindAsync(id);

            if (tblGrupoRebate == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblGrupoRebate.Remove(tblGrupoRebate);
                await _context.SaveChangesAsync();
                return Ok(tblGrupoRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DESATIVA: api/GrupoRebate/DisableGrupoRebate/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableGrupoRebate(int id)
        {
            TblGrupoRebate registroGrupoRebate = await _context.TblGrupoRebate.FindAsync(id);

            if (registroGrupoRebate != null)
            {
                registroGrupoRebate.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroGrupoRebate);
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

        // ATIVAR: api/GrupoRebate/ActivateGrupoRebate/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateGrupoRebate(int id)
        {
            TblGrupoRebate registroGrupoRebate = await _context.TblGrupoRebate.FindAsync(id);

            if (registroGrupoRebate != null)
            {
                registroGrupoRebate.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroGrupoRebate);
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

        private bool GrupoRebateExists(int id)
        {
            return _context.TblGrupoRebate.Any(e => e.Id == id);
        }

        #endregion

        #region Email Grupo Rebate
        // GET: api/GrupoRebate/GetEmailGrupoRebate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblEmailGrupoRebate>>> GetEmailGrupoRebate()
        {
            try
            {
                var emailGrupoRebate = await _context.TblEmailGrupoRebate.Where(c => c.Ativo == true).OrderBy(c => c.Email).ToListAsync();

                if (emailGrupoRebate.Count == 0)
                {
                    return NotFound();
                }

                return Ok(emailGrupoRebate);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/GrupoRebate/GetEmailGrupoRebateById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblEmailGrupoRebate>> GetEmailGrupoRebateById(int id)
        {
            try
            {
                TblEmailGrupoRebate tblEmailGrupoRebate = await _context.TblEmailGrupoRebate.FindAsync(id);

                if (tblEmailGrupoRebate == null)
                {
                    return NotFound();
                }

                return Ok(tblEmailGrupoRebate);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion
    }
}
