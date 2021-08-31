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

        #region Gestor
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
                    return NotFound();
                }
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.InnerException.Message);
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
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Gestor/GetGestorExiste/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<TblCustodiante>> GetGestorExiste(string cnpj)
        {
            TblGestor tblGestor = new TblGestor();

            tblGestor = await _context.TblGestor.Where(c => c.Ativo == false && c.Cnpj == cnpj).FirstOrDefaultAsync();

            if (tblGestor != null)
            {
                return Ok(tblGestor);
            }
            else
            {
                tblGestor = await _context.TblGestor.Where(c => c.Cnpj == cnpj).FirstOrDefaultAsync();

                if (tblGestor != null)
                {
                    return Ok(tblGestor);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        //POST: api/Gestor/CadastrarGestor/GestorModel
        [HttpPost]
        public async Task<ActionResult<GestorModel>> CadastrarGestor(GestorModel tblGestorModel)
        {
            TblGestor itensGestor = new TblGestor
            {
                NomeGestor = tblGestorModel.NomeGestor,
                CodTipoClassificacaoGestor = tblGestorModel.CodTipoClassificacaoGestor,
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
                return BadRequest(e.InnerException.Message);
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
                    registroGestor.CodTipoClassificacaoGestor = gestor.CodTipoClassificacaoGestor == 0 ? registroGestor.CodTipoClassificacaoGestor : gestor.CodTipoClassificacaoGestor;
                    registroGestor.Cnpj = gestor.Cnpj == null ? registroGestor.Cnpj : gestor.Cnpj;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroGestor);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.InnerException.Message);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException e) when (!GestorExists(gestor.Id))
            {
                return NotFound(e.InnerException.Message);
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
                return BadRequest(e.InnerException.Message);
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
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // ATIVAR: api/Gestor/AtivarGestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> AtivarGestor(int id)
        {
            TblGestor registroGestor = await _context.TblGestor.FindAsync(id);

            if (registroGestor != null)
            {
                registroGestor.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroGestor);
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

        private bool GestorExists(int id)
        {
            return _context.TblGestor.Any(e => e.Id == id);
        }
        #endregion

        #region Tipo Classificação
        // GET: api/Gestor/TipoClassificacaoGestor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoClassificacaoGestor>>> TipoClassificacaoGestor()
        {
            try
            {
                var tiposClassificacaoGestor = await _context.TblTipoClassificacaoGestor.Where(c => c.Ativo == true).OrderBy(c => c.ClassificacaoGestor).ToListAsync();

                if (tiposClassificacaoGestor.Count() == 0)
                {
                    return NotFound();
                }

                if (tiposClassificacaoGestor != null)
                {
                    return Ok(tiposClassificacaoGestor);
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

        // GET: api/Gestor/GetTipoClassificacaoGestor/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTipoClassificacaoGestor>> GetTipoClassificacaoGestor(int id)
        {
            TblTipoClassificacaoGestor tblTipoClassificacaoGestor = await _context.TblTipoClassificacaoGestor.FindAsync(id);

            try
            {
                if (tblTipoClassificacaoGestor != null)
                {
                    return Ok(tblTipoClassificacaoGestor);
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

        //POST: api/Gestor/CadastrarTipoClassificacaoGestor/TipoContaModel
        [HttpPost]
        public async Task<ActionResult<TipoClassificacaoGestorModel>> CadastrarTipoClassificacaoGestor(TipoClassificacaoGestorModel tblTipoClassificacaoModel)
        {
            TblTipoClassificacaoGestor itensTipoClassificacao = new TblTipoClassificacaoGestor
            {
                Id = tblTipoClassificacaoModel.Id,
                ClassificacaoGestor = tblTipoClassificacaoModel.ClassificacaoGestor,
                UsuarioModificacao = tblTipoClassificacaoModel.UsuarioModificacao
            };

            try
            {
                _context.TblTipoClassificacaoGestor.Add(itensTipoClassificacao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetTipoClassificacaoGestor),
                    new
                    {
                        Id = itensTipoClassificacao.Id,
                    },
                     Ok(itensTipoClassificacao));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Gestor/EditarTipoClassificacaoGestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTipoClassificacaoGestor(int id, TipoClassificacaoGestorModel tipoClassificacao)
        {
            try
            {
                TblTipoClassificacaoGestor registroTipoClassificacao = _context.TblTipoClassificacaoGestor.Find(id);

                if (registroTipoClassificacao != null)
                {
                    registroTipoClassificacao.ClassificacaoGestor = tipoClassificacao.ClassificacaoGestor == null ? registroTipoClassificacao.ClassificacaoGestor : tipoClassificacao.ClassificacaoGestor;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroTipoClassificacao);
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
            catch (DbUpdateConcurrencyException e) when (!TipoClassificacaoGestorExists(tipoClassificacao.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/Gestor/DeletarTipoClassificacaoGestor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTipoClassificacaoGestor(int id)
        {
            TblTipoClassificacaoGestor tblTipoClassificacao = await _context.TblTipoClassificacaoGestor.FindAsync(id);

            if (tblTipoClassificacao == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblTipoClassificacaoGestor.Remove(tblTipoClassificacao);
                await _context.SaveChangesAsync();
                return Ok(tblTipoClassificacao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DESATIVA: api/Gestor/DesativarTipoClassificacaoGestor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarTipoClassificacaoGestor(int id)
        {
            TblTipoClassificacaoGestor registroTipoClassificacao = _context.TblTipoClassificacaoGestor.Find(id);

            if (registroTipoClassificacao != null)
            {
                registroTipoClassificacao.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroTipoClassificacao);
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

        private bool TipoClassificacaoGestorExists(int id)
        {
            return _context.TblTipoClassificacaoGestor.Any(e => e.Id == id);
        }
        #endregion
    }
}
