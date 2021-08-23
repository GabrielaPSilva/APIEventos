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
    public class CondicaoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public CondicaoController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        #region Condição Remuneração
        // GET: api/Condicao/CondicaoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCondicaoRemuneracao>>> CondicaoRemuneracao()
        {
            try
            {
                List<TblCondicaoRemuneracao> condicoesRemuneracao = await _context.TblCondicaoRemuneracao.Where(c => c.Ativo == true).OrderBy(c => c.CodContratoRemuneracao).AsNoTracking().ToListAsync();

                if (condicoesRemuneracao.Count() == 0)
                {
                    return NotFound();
                }

                if (condicoesRemuneracao != null)
                {
                    return Ok(condicoesRemuneracao);
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

        // GET: api/Condicao/GetCondicaoRemuneracao/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCondicaoRemuneracao>> GetCondicaoRemuneracao(int id)
        {
            TblCondicaoRemuneracao tblCondicaoRemuneracao = await _context.TblCondicaoRemuneracao.FindAsync(id);

            try
            {
                if (tblCondicaoRemuneracao != null)
                {
                    return Ok(tblCondicaoRemuneracao);
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

        //POST: api/Condicao/CadastrarCondicaoRemuneracao/CondicaoRemuneracaoModel
        [HttpPost]
        public async Task<ActionResult<CondicaoRemuneracaoModel>> CadastrarCondicaoRemuneracao(CondicaoRemuneracaoModel tblCondicaoRemuneracaoModel)
        {
            TblCondicaoRemuneracao itensCondicaoRemuneracao = new TblCondicaoRemuneracao
            {
                CodContratoRemuneracao = tblCondicaoRemuneracaoModel.CodContratoRemuneracao,
                CodFundo = tblCondicaoRemuneracaoModel.CodFundo,
                DataInicio = tblCondicaoRemuneracaoModel.DataInicio,
                DataFim = tblCondicaoRemuneracaoModel.DataFim,
                ValorPosicaoInicio = tblCondicaoRemuneracaoModel.ValorPosicaoInicio,
                ValorPosicaoFim = tblCondicaoRemuneracaoModel.ValorPosicaoFim,
                ValorPgtoFixo = tblCondicaoRemuneracaoModel.ValorPgtoFixo,
                UsuarioModificacao = tblCondicaoRemuneracaoModel.UsuarioModificacao
            };

            try
            {
                _context.TblCondicaoRemuneracao.Add(itensCondicaoRemuneracao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetCondicaoRemuneracao),
                    new { id = itensCondicaoRemuneracao.Id },
                    Ok(itensCondicaoRemuneracao));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Condicao/EditarCondicaoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCondicaoRemuneracao(int id, CondicaoRemuneracaoModel condicaoRemuneracao)
        {
            try
            {
                TblCondicaoRemuneracao registroCondicaoRemuneracao = _context.TblCondicaoRemuneracao.Find(id);

                if (registroCondicaoRemuneracao != null)
                {
                    registroCondicaoRemuneracao.CodContratoRemuneracao = condicaoRemuneracao.CodContratoRemuneracao == 0 ? registroCondicaoRemuneracao.CodContratoRemuneracao : condicaoRemuneracao.CodContratoRemuneracao;
                    registroCondicaoRemuneracao.CodFundo = condicaoRemuneracao.CodFundo == 0 ? registroCondicaoRemuneracao.CodFundo : condicaoRemuneracao.CodFundo;
                    registroCondicaoRemuneracao.DataInicio = condicaoRemuneracao.DataInicio == null ? registroCondicaoRemuneracao.DataInicio : condicaoRemuneracao.DataInicio;
                    registroCondicaoRemuneracao.DataFim = condicaoRemuneracao.DataFim == null ? registroCondicaoRemuneracao.DataFim : condicaoRemuneracao.DataFim;
                    registroCondicaoRemuneracao.ValorPosicaoInicio = (condicaoRemuneracao.ValorPosicaoInicio == null || condicaoRemuneracao.ValorPosicaoInicio == 0) ? registroCondicaoRemuneracao.ValorPosicaoInicio : condicaoRemuneracao.ValorPosicaoInicio;
                    registroCondicaoRemuneracao.ValorPosicaoFim = (condicaoRemuneracao.ValorPosicaoFim == null || condicaoRemuneracao.ValorPosicaoFim == 0) ? registroCondicaoRemuneracao.ValorPosicaoFim : condicaoRemuneracao.ValorPosicaoFim;
                    registroCondicaoRemuneracao.ValorPgtoFixo = (condicaoRemuneracao.ValorPgtoFixo == null || condicaoRemuneracao.ValorPgtoFixo == 0) ? registroCondicaoRemuneracao.ValorPgtoFixo : condicaoRemuneracao.ValorPgtoFixo;
                    registroCondicaoRemuneracao.UsuarioModificacao = condicaoRemuneracao.UsuarioModificacao == null ? registroCondicaoRemuneracao.UsuarioModificacao : condicaoRemuneracao.UsuarioModificacao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroCondicaoRemuneracao);
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
            catch (DbUpdateConcurrencyException e) when (!CondicaoRemuneracaoExists(condicaoRemuneracao.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/Condicao/DeletarCondicaoRemuneracao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCondicaoRemuneracao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_condicao_remuneracao");

            if (!existeRegistro)
            {
                TblCondicaoRemuneracao tblCondicaoRemuneracao = await _context.TblCondicaoRemuneracao.FindAsync(id);

                if (tblCondicaoRemuneracao == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblCondicaoRemuneracao.Remove(tblCondicaoRemuneracao);
                    await _context.SaveChangesAsync();
                    return Ok(tblCondicaoRemuneracao);
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

        // DESATIVA: api/Condicao/DesativarCondicaoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarCondicaoRemuneracao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_condicao_remuneracao");

            if (!existeRegistro)
            {
                TblCondicaoRemuneracao registroCondicaoRemuneracao = _context.TblCondicaoRemuneracao.Find(id);

                if (registroCondicaoRemuneracao != null)
                {
                    registroCondicaoRemuneracao.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroCondicaoRemuneracao);
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

        private bool CondicaoRemuneracaoExists(int id)
        {
            return _context.TblCondicaoRemuneracao.Any(e => e.Id == id);
        }
        #endregion

        #region Tipo Condição

        // GET: api/Condicao/TipoCondicao
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

        // GET: api/Condicao/GetTipoCondicao/id
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

        //POST: api/Condicao/CadastrarTipoCondicao/TipoCondicaoModel
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

        //PUT: api/Condicao/EditarTipoCondicao/id
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

        // DELETE: api/Condicao/DeletarTipoCondicao/id
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

        // DESATIVA: api/Condicao/DesativaTipoCondicao/id
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
