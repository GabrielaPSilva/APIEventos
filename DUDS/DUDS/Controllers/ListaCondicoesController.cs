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
    public class ListaCondicoesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public ListaCondicoesController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        #region Lista Condições
        // GET: api/ListaCondicoes/ListaCondicoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblListaCondicoes>>> ListaCondicoes()
        {
            try
            {
                List<TblListaCondicoes> listaCondicoes = await _context.TblListaCondicoes.Where(c => c.Ativo == true).OrderBy(c => c.CodAcordoCondicional).AsNoTracking().ToListAsync();

                if (listaCondicoes.Count() == 0)
                {
                    return NotFound();
                }

                if (listaCondicoes != null)
                {
                    return Ok(listaCondicoes);
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

        // GET: api/ListaCondicoes/GetListaCondicoes/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblListaCondicoes>> GetListaCondicoes(int id)
        {
            TblListaCondicoes tblListaCondicoes = await _context.TblListaCondicoes.FindAsync(id);

            try
            {
                if (tblListaCondicoes != null)
                {
                    return Ok(tblListaCondicoes);
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

        //POST: api/ListaCondicoes/CadastrarListaCondicoes/ListaCondicoesModel
        [HttpPost]
        public async Task<ActionResult<ListaCondicoesModel>> CadastrarListaCondicoes(ListaCondicoesModel tblListaCondicoesModel)
        {
            TblListaCondicoes itensListaCondicoes = new TblListaCondicoes
            {
                CodAcordoCondicional = tblListaCondicoesModel.CodAcordoCondicional,
                CodFundo = tblListaCondicoesModel.CodFundo,
                DataInicio = tblListaCondicoesModel.DataInicio,
                DataFim = tblListaCondicoesModel.DataFim,
                ValorPosicaoInicio = tblListaCondicoesModel.ValorPosicaoInicio,
                ValorPosicaoFim = tblListaCondicoesModel.ValorPosicaoFim,
                UsuarioModificacao = tblListaCondicoesModel.UsuarioModificacao
            };

            try
            {
                _context.TblListaCondicoes.Add(itensListaCondicoes);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetListaCondicoes),
                    new { id = itensListaCondicoes.Id },
                      Ok(itensListaCondicoes));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/ListaCondicoes/EditarListaCondicoes/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarListaCondicoes(int id, ListaCondicoesModel listaCondicoes)
        {
            try
            {
                TblListaCondicoes registroListaCondicoes = _context.TblListaCondicoes.Find(id);

                if (registroListaCondicoes != null)
                {
                    registroListaCondicoes.CodAcordoCondicional = listaCondicoes.CodAcordoCondicional == 0 ? registroListaCondicoes.CodAcordoCondicional : listaCondicoes.CodAcordoCondicional;
                    registroListaCondicoes.CodFundo = listaCondicoes.CodFundo == 0 ? registroListaCondicoes.CodFundo : listaCondicoes.CodFundo;
                    registroListaCondicoes.DataInicio = listaCondicoes.DataInicio == null ? registroListaCondicoes.DataInicio : listaCondicoes.DataInicio;
                    registroListaCondicoes.DataFim = listaCondicoes.DataFim == null ? registroListaCondicoes.DataFim : listaCondicoes.DataFim;
                    registroListaCondicoes.ValorPosicaoInicio = listaCondicoes.ValorPosicaoInicio == 0 ? registroListaCondicoes.ValorPosicaoInicio : listaCondicoes.ValorPosicaoInicio;
                    registroListaCondicoes.ValorPosicaoFim = listaCondicoes.ValorPosicaoFim == 0 ? registroListaCondicoes.ValorPosicaoFim : listaCondicoes.ValorPosicaoFim;
                    registroListaCondicoes.UsuarioModificacao = listaCondicoes.UsuarioModificacao == null ? registroListaCondicoes.UsuarioModificacao : listaCondicoes.UsuarioModificacao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroListaCondicoes);
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
            catch (DbUpdateConcurrencyException e) when (!ListaCondicoesExists(listaCondicoes.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/ListaCondicoes/DeletarListaCondicoes/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarListaCondicoes(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_lista_condicoes");

            if (!existeRegistro)
            {
                TblListaCondicoes tblListaCondicoes = await _context.TblListaCondicoes.FindAsync(id);

                if (tblListaCondicoes == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblListaCondicoes.Remove(tblListaCondicoes);
                    await _context.SaveChangesAsync();
                    return Ok(tblListaCondicoes);
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

        // DESATIVA: api/ListaCondicoes/DesativarListaCondicoes/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarListaCondicoes(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_lista_condicoes");

            if (!existeRegistro)
            {
                TblListaCondicoes registroListaCondicoes = _context.TblListaCondicoes.Find(id);

                if (registroListaCondicoes != null)
                {
                    registroListaCondicoes.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroListaCondicoes);
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

        private bool ListaCondicoesExists(int id)
        {
            return _context.TblListaCondicoes.Any(e => e.Id == id);
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
