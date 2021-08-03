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
    public class ContratoController : ControllerBase
    {
        private readonly DataContext _context;

        public ContratoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Contrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContrato>>> Contrato()
        {
            try
            {
                var contratos = await _context.TblContrato.Where(c => c.Ativo == true).AsNoTracking().ToListAsync();

                if (contratos != null)
                {
                    return Ok(contratos);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (InvalidOperationException e)
            {
                return BadRequest();
            }
        }

        // GET: api/Contrato/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContrato>> GetContrato(int id)
        {
            var tblContrato = await _context.TblContrato.FindAsync(id);

            if (tblContrato == null)
            {
                return NotFound();
            }

            return Ok(tblContrato);
        }

        [HttpPost]
        public async Task<ActionResult<ContratoModel>> CadastrarContrato(ContratoModel tblContratoModel)
        {
            var itensContrato = new TblContrato
            {
                CodDistribuidor = tblContratoModel.CodDistribuidor,
                TipoContrato = tblContratoModel.TipoContrato,
                Versao = tblContratoModel.Versao,
                Status = tblContratoModel.Status,
                IdDocusign = tblContratoModel.IdDocusign,
                DirecaoPagamento = tblContratoModel.DirecaoPagamento,
                ClausulaRetroatividade = tblContratoModel.ClausulaRetroatividade,
                DataRetroatividade = tblContratoModel.DataRetroatividade,
                DataAssinatura = tblContratoModel.DataAssinatura,
                Ativo = tblContratoModel.Ativo,
                UsuarioModificacao = tblContratoModel.UsuarioModificacao,
                DataModificacao = tblContratoModel.DataModificacao
            };

            _context.TblContrato.Add(itensContrato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetContrato),
                new { id = itensContrato.Id },
                Ok(itensContrato));
        }

        //PUT: api/Contrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarContrato(int id, ContratoModel contrato)
        {
            try
            {
                var registroContrato = _context.TblContrato.Find(id);

                if (registroContrato != null)
                {
                    registroContrato.CodDistribuidor = (contrato.CodDistribuidor == null || contrato.CodDistribuidor == 0) ? registroContrato.CodDistribuidor : contrato.CodDistribuidor;
                    registroContrato.TipoContrato = contrato.TipoContrato == null ? registroContrato.TipoContrato : contrato.TipoContrato;
                    registroContrato.Versao = contrato.Versao == null ? registroContrato.Versao : contrato.Versao;
                    registroContrato.Status = contrato.Status == null ? registroContrato.Status : contrato.Status;
                    registroContrato.IdDocusign = contrato.IdDocusign == null ? registroContrato.IdDocusign : contrato.IdDocusign;
                    registroContrato.DirecaoPagamento = contrato.DirecaoPagamento == null ? registroContrato.DirecaoPagamento : contrato.DirecaoPagamento;
                    registroContrato.ClausulaRetroatividade = contrato.ClausulaRetroatividade == false ? registroContrato.ClausulaRetroatividade : contrato.ClausulaRetroatividade;
                    registroContrato.DataRetroatividade = contrato.DataRetroatividade == null ? registroContrato.DataRetroatividade : contrato.DataRetroatividade;
                    registroContrato.DataAssinatura = contrato.DataAssinatura == null ? registroContrato.DataAssinatura : contrato.DataAssinatura;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException) when (!ContratoExists(contrato.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Contrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContrato(int id)
        {
            var tblContrato = await _context.TblContrato.FindAsync(id);

            if (tblContrato == null)
            {
                return NotFound();
            }

            _context.TblContrato.Remove(tblContrato);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DESATIVA: api/Contrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarContrato(int id)
        {
            var registroContrato = _context.TblContrato.Find(id);

            if (registroContrato != null)
            {
                registroContrato.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool ContratoExists(int id)
        {
            return _context.TblContrato.Any(e => e.Id == id);
        }

        #region Contrato Distribuicao
        // GET: api/ContratoDistribuicao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoDistribuicao>>> ContratoDistribuicao()
        {
            return await _context.TblContratoDistribuicao.ToListAsync();
        }

        // GET: api/ContratoDistribuicao/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoDistribuicao>> GetContratoDistribuicao(int id)
        {
            var tblContratoDistribuicao = await _context.TblContratoDistribuicao.FindAsync(id);

            if (tblContratoDistribuicao == null)
            {
                return NotFound();
            }

            return Ok(tblContratoDistribuicao);
        }

        [HttpPost]
        public async Task<ActionResult<ContratoDistribuicaoModel>> CadastrarContrato(ContratoDistribuicaoModel tblContratoDistribuicaoModel)
        {
            var itensContratoDistribuicao = new TblContratoDistribuicao
            {
                CodContrato = tblContratoDistribuicaoModel.CodContrato,
                CodFundo = tblContratoDistribuicaoModel.CodFundo,
                UsuarioModificacao = tblContratoDistribuicaoModel.UsuarioModificacao,
                DataModificacao = tblContratoDistribuicaoModel.DataModificacao
            };

            _context.TblContratoDistribuicao.Add(itensContratoDistribuicao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetContratoDistribuicao),
                new { id = itensContratoDistribuicao.Id },
                Ok(itensContratoDistribuicao));
        }

        #endregion
    }
}
