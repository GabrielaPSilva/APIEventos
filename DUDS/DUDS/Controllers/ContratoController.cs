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
    public class ContratoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public ContratoController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/Contrato/Contrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContrato>>> Contrato()
        {
            try
            {
                List<TblContrato> contratos = await _context.TblContrato.Where(c => c.Ativo == true).AsNoTracking().ToListAsync();

                if (contratos.Count() == 0)
                {
                    return NotFound();
                }

                if (contratos != null)
                {
                    return Ok(contratos);
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

        // GET: api/Contrato/GetContrato/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContrato>> GetContrato(int id)
        {
            TblContrato tblContrato = await _context.TblContrato.FindAsync(id);

            try
            {
                if (tblContrato != null)
                {
                    return Ok(tblContrato);
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

        //POST: api/Contrato/CadastrarContrato/ContratoModel
        [HttpPost]
        public async Task<ActionResult<ContratoModel>> CadastrarContrato(ContratoModel tblContratoModel)
        {
            TblContrato itensContrato = new TblContrato
            {
                CodDistribuidor = tblContratoModel.CodDistribuidor,
                Parceiro = tblContratoModel.Parceiro,
                TipoContrato = tblContratoModel.TipoContrato,
                UsuarioModificacao = tblContratoModel.UsuarioModificacao
            };

            try
            {
                _context.TblContrato.Add(itensContrato);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetContrato),
                    new { id = itensContrato.Id },
                      Ok(itensContrato));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/EditarContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarContrato(int id, ContratoModel contrato)
        {
            try
            {
                TblContrato registroContrato = _context.TblContrato.Find(id);

                if (registroContrato != null)
                {
                    registroContrato.CodDistribuidor = (contrato.CodDistribuidor == null || contrato.CodDistribuidor == 0) ? registroContrato.CodDistribuidor : contrato.CodDistribuidor;
                    registroContrato.TipoContrato = contrato.TipoContrato == null ? registroContrato.TipoContrato : contrato.TipoContrato;
                    registroContrato.Parceiro = contrato.Parceiro == null ? registroContrato.Parceiro : contrato.Parceiro;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroContrato);
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
            catch (DbUpdateConcurrencyException e) when (!ContratoExists(contrato.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/Contrato/DeletarContrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contrato");

            if (!existeRegistro)
            {
                TblContrato tblContrato = await _context.TblContrato.FindAsync(id);

                if (tblContrato == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblContrato.Remove(tblContrato);
                    await _context.SaveChangesAsync();
                    return Ok(tblContrato);
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

        // DESATIVA: api/Contrato/DesativarContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contrato");

            if (!existeRegistro)
            {
                TblContrato registroContrato = _context.TblContrato.Find(id);

                if (registroContrato != null)
                {
                    registroContrato.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroContrato);
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

        private bool ContratoExists(int id)
        {
            return _context.TblContrato.Any(e => e.Id == id);
        }

        #region Contrato Distribuicao
        // GET: api/Contrato/ContratoDistribuicao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoDistribuicao>>> ContratoDistribuicao()
        {
            try
            {
                List<TblContratoDistribuicao> contratosDistribuicao = await _context.TblContratoDistribuicao.OrderBy(c => c.CodSubContrato).ToListAsync();

                if (contratosDistribuicao.Count() == 0)
                {
                    return NotFound();
                }

                if (contratosDistribuicao != null)
                {
                    return Ok(contratosDistribuicao);
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

        // GET: api/Contrato/GetContratoDistribuicao/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoDistribuicao>> GetContratoDistribuicao(int id)
        {
            TblContratoDistribuicao tblContratoDistribuicao = await _context.TblContratoDistribuicao.FindAsync(id);

            try
            {
                if (tblContratoDistribuicao != null)
                {
                    return Ok(tblContratoDistribuicao);
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

        //POST: api/Contrato/CadastrarContratoDistribuicao/ContratoDistribuicaoModel
        [HttpPost]
        public async Task<ActionResult<ContratoDistribuicaoModel>> CadastrarContratoDistribuicao(ContratoDistribuicaoModel tblContratoDistribuicaoModel)
        {
            TblContratoDistribuicao itensContratoDistribuicao = new TblContratoDistribuicao
            {
                CodSubContrato = tblContratoDistribuicaoModel.CodSubContrato,
                CodFundo = tblContratoDistribuicaoModel.CodFundo,
                UsuarioModificacao = tblContratoDistribuicaoModel.UsuarioModificacao
            };

            try
            {
                _context.TblContratoDistribuicao.Add(itensContratoDistribuicao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetContratoDistribuicao),
                    new { id = itensContratoDistribuicao.Id },
                    Ok(itensContratoDistribuicao));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/EditarContratoDistribuicao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarContratoDistribuicao(int id, ContratoDistribuicaoModel contratoDistribuicao)
        {
            try
            {
                TblContratoDistribuicao registroContratoDistribuicao = _context.TblContratoDistribuicao.Find(id);

                if (registroContratoDistribuicao != null)
                {
                    registroContratoDistribuicao.CodSubContrato = contratoDistribuicao.CodSubContrato == 0 ? registroContratoDistribuicao.CodSubContrato : contratoDistribuicao.CodSubContrato;
                    registroContratoDistribuicao.CodFundo  = contratoDistribuicao.CodFundo == 0 ? registroContratoDistribuicao.CodFundo : contratoDistribuicao.CodFundo;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroContratoDistribuicao);
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
            catch (DbUpdateConcurrencyException e) when (!ContratoDistribuicaoExists(contratoDistribuicao.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/Contrato/DeletarContratoDistribuicao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContratoDistribuicao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contrato_distribuicao");

            if (!existeRegistro)
            {
                TblContratoDistribuicao tblContratoDistribuicao = await _context.TblContratoDistribuicao.FindAsync(id);

                if (tblContratoDistribuicao == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblContratoDistribuicao.Remove(tblContratoDistribuicao);
                    await _context.SaveChangesAsync();
                    return Ok(tblContratoDistribuicao);
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

        private bool ContratoDistribuicaoExists(int id)
        {
            return _context.TblContratoDistribuicao.Any(e => e.Id == id);
        }
        #endregion
    }
}
