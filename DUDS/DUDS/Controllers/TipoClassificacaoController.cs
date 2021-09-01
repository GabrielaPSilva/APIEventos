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
    public class TipoClassificacaoController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoClassificacaoController(DataContext context)
        {
            _context = context;
        }

        #region Tipo Classificação
        // GET: api/TipoClassificacao/TipoClassificacaoGestor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoClassificacao>>> TipoClassificacao()
        {
            try
            {
                var tiposClassificacao = await _context.TblTipoClassificacao.Where(c => c.Ativo == true).OrderBy(c => c.Classificacao).ToListAsync();

                if (tiposClassificacao.Count() == 0)
                {
                    return NotFound();
                }

                if (tiposClassificacao != null)
                {
                    return Ok(tiposClassificacao);
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

        // GET: api/TipoClassificacao/GetTipoClassificacao/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTipoClassificacao>> GetTipoClassificacao(int id)
        {
            TblTipoClassificacao tblTipoClassificacao = await _context.TblTipoClassificacao.FindAsync(id);

            try
            {
                if (tblTipoClassificacao != null)
                {
                    return Ok(tblTipoClassificacao);
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

        //POST: api/TipoClassificacao/CadastrarTipoClassificacao/TipoContaModel
        [HttpPost]
        public async Task<ActionResult<TipoClassificacaoModel>> CadastrarTipoClassificacao(TipoClassificacaoModel tblTipoClassificacaoModel)
        {
            TblTipoClassificacao itensTipoClassificacao = new TblTipoClassificacao
            {
                Id = tblTipoClassificacaoModel.Id,
                Classificacao = tblTipoClassificacaoModel.Classificacao,
                UsuarioModificacao = tblTipoClassificacaoModel.UsuarioModificacao
            };

            try
            {
                _context.TblTipoClassificacao.Add(itensTipoClassificacao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetTipoClassificacao),
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

        //PUT: api/TipoClassificacao/EditarTipoClassificacao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTipoClassificacao(int id, TipoClassificacaoModel tipoClassificacao)
        {
            try
            {
                TblTipoClassificacao registroTipoClassificacao = _context.TblTipoClassificacao.Find(id);

                if (registroTipoClassificacao != null)
                {
                    registroTipoClassificacao.Classificacao = tipoClassificacao.Classificacao == null ? registroTipoClassificacao.Classificacao : tipoClassificacao.Classificacao;

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
            catch (DbUpdateConcurrencyException e) when (!TipoClassificacaoExists(tipoClassificacao.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/TipoClassificacao/DeletarTipoClassificacao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTipoClassificacao(int id)
        {
            TblTipoClassificacao tblTipoClassificacao = await _context.TblTipoClassificacao.FindAsync(id);

            if (tblTipoClassificacao == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblTipoClassificacao.Remove(tblTipoClassificacao);
                await _context.SaveChangesAsync();
                return Ok(tblTipoClassificacao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DESATIVA: api/TipoClassificacao/DesativarTipoClassificacao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarTipoClassificacao(int id)
        {
            TblTipoClassificacao registroTipoClassificacao = _context.TblTipoClassificacao.Find(id);

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

        private bool TipoClassificacaoExists(int id)
        {
            return _context.TblTipoClassificacao.Any(e => e.Id == id);
        }
        #endregion
    }
}
