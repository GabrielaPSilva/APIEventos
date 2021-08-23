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
    [Route("api/[controller]")]
    [ApiController]
    public class CondicaoController : ControllerBase
    {
        private readonly DataContext _context;

        public CondicaoController(DataContext context)
        {
            _context = context;
        }

        #region Condição Remuneração
        // GET: api/Condicao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCondicaoRemuneracao>>> GetTblCondicaoRemuneracao()
        {
            return await _context.TblCondicaoRemuneracao.ToListAsync();
        }

        // GET: api/Condicao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCondicaoRemuneracao>> GetTblCondicaoRemuneracao(int id)
        {
            var tblCondicaoRemuneracao = await _context.TblCondicaoRemuneracao.FindAsync(id);

            if (tblCondicaoRemuneracao == null)
            {
                return NotFound();
            }

            return tblCondicaoRemuneracao;
        }

        // PUT: api/Condicao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCondicaoRemuneracao(int id, TblCondicaoRemuneracao tblCondicaoRemuneracao)
        {
            if (id != tblCondicaoRemuneracao.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblCondicaoRemuneracao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCondicaoRemuneracaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Condicao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblCondicaoRemuneracao>> PostTblCondicaoRemuneracao(TblCondicaoRemuneracao tblCondicaoRemuneracao)
        {
            _context.TblCondicaoRemuneracao.Add(tblCondicaoRemuneracao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblCondicaoRemuneracao", new { id = tblCondicaoRemuneracao.Id }, tblCondicaoRemuneracao);
        }

        // DELETE: api/Condicao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCondicaoRemuneracao(int id)
        {
            var tblCondicaoRemuneracao = await _context.TblCondicaoRemuneracao.FindAsync(id);
            if (tblCondicaoRemuneracao == null)
            {
                return NotFound();
            }

            _context.TblCondicaoRemuneracao.Remove(tblCondicaoRemuneracao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblCondicaoRemuneracaoExists(int id)
        {
            return _context.TblCondicaoRemuneracao.Any(e => e.Id == id);
        }
        #endregion

        #region Tipo Condição

        // GET: api/ListaCondicoes/TipoCondicao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoCondicao>>> TipoCondicao()
        {
            try
            {
                List<TblTipoCondicao> tipoCondicoes = await _context.TblTipoCondicao.Where(c => c.Ativo == true).OrderBy(c => c.TipoCondicao).AsNoTracking().ToListAsync();

                if (tipoCondicoes.Count() == 0)
                {
                    return NotFound();
                }

                if (tipoCondicoes != null)
                {
                    return Ok(tipoCondicoes);
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

        // GET: api/ListaCondicoes/GetTipoCondicao/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTipoCondicao>> GetTipoCondicao(int id)
        {
            TblTipoCondicao tblTipoCondicao = await _context.TblTipoCondicao.FindAsync(id);

            try
            {
                if (tblTipoCondicao != null)
                {
                    return Ok(tblTipoCondicao);
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

        //POST: api/ListaCondicoes/CadastrarTipoCondicao/TipoCondicaoModel
        [HttpPost]
        public async Task<ActionResult<TipoCondicaoModel>> CadastrarTipoCondicao(TipoCondicaoModel tblTipoCondicaoModel)
        {
            TblTipoCondicao itensTipoCondicao = new TblTipoCondicao
            {
                TipoCondicao = tblTipoCondicaoModel.TipoCondicao,
                UsuarioModificacao = tblTipoCondicaoModel.UsuarioModificacao
            };

            try
            {
                _context.TblTipoCondicao.Add(itensTipoCondicao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                   nameof(GetTipoCondicao),
                   new { id = itensTipoCondicao.Id },
                   Ok(itensTipoCondicao));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/ListaCondicoes/EditarTipoCondicao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTipoCondicao(int id, TipoCondicaoModel tipoCondicao)
        {
            try
            {
                TblTipoCondicao registroTipoCondicao = _context.TblTipoCondicao.Find(id);

                if (registroTipoCondicao != null)
                {
                    registroTipoCondicao.TipoCondicao = tipoCondicao.TipoCondicao == null ? registroTipoCondicao.TipoCondicao : tipoCondicao.TipoCondicao;
                    registroTipoCondicao.UsuarioModificacao = tipoCondicao.UsuarioModificacao == null ? registroTipoCondicao.UsuarioModificacao : tipoCondicao.UsuarioModificacao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroTipoCondicao);
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
            catch (DbUpdateConcurrencyException e) when (!TipoCondicaoExists(tipoCondicao.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/ListarCondicoes/DeletarTipoCondicao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTipoCondicao(int id)
        {
            TblTipoCondicao tblTipoCondicao = await _context.TblTipoCondicao.FindAsync(id);

            if (tblTipoCondicao == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblTipoCondicao.Remove(tblTipoCondicao);
                await _context.SaveChangesAsync();
                return Ok(tblTipoCondicao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DESATIVA: api/ListarCondicoes/DesativaTipoCondicao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativaTipoCondicao(int id)
        {
            TblTipoCondicao registroTipoCondicao = _context.TblTipoCondicao.Find(id);

            if (registroTipoCondicao != null)
            {
                registroTipoCondicao.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroTipoCondicao);
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

        private bool TipoCondicaoExists(int id)
        {
            return _context.TblTipoCondicao.Any(e => e.Id == id);
        }

        #endregion
    }
}
