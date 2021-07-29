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
    public class ContasController : ControllerBase
    {
        private readonly DataContext _context;

        public ContasController(DataContext context)
        {
            _context = context;
        }

        #region Conta
        // GET: api/Contas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContas>>> Contas()
        {
            return await _context.TblContas.Where(c => c.Ativo == true).ToListAsync();
        }

        // GET: api/Contas/5
        [HttpGet]
        public async Task<ActionResult<TblContas>> GetContas(int codFundo, int codTipoConta)
        {
            var tblContas = await _context.TblContas.FindAsync(codFundo, codTipoConta);

            if (tblContas == null)
            {
                return NotFound();
            }

            return Ok(tblContas);
        }

        [HttpPost]
        public async Task<ActionResult<ContaModel>> CadastrarConta(ContaModel tblContaModel)
        {
            var itensConta = new TblContas
            {
                CodFundo = tblContaModel.CodFundo,
                CodTipoConta = tblContaModel.CodTipoConta,
                Banco = tblContaModel.Banco,
                Agencia = tblContaModel.Agencia,
                Conta = tblContaModel.Conta,
                UsuarioModificacao = tblContaModel.UsuarioModificacao
            };

            _context.TblContas.Add(itensConta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetContas),
                new { codFundo = itensConta.CodFundo,
                      codTipoConta = itensConta.CodTipoConta },
                Ok(itensConta));
        }

        //PUT: api/Conta/id
        [HttpPut("{codFundo}/{codTipoConta}")]
        public async Task<IActionResult> EditarConta(int codFundo, int codTipoConta, ContaModel conta)
        {
            try
            {
                var registroConta = _context.TblContas.Find(codFundo, codTipoConta);

                if (registroConta != null)
                {
                    registroConta.CodFundo = conta.CodFundo == 0 ? registroConta.CodFundo : conta.CodFundo;
                    registroConta.CodTipoConta = conta.CodTipoConta == 0 ? registroConta.CodTipoConta : conta.CodTipoConta;
                    registroConta.Banco = conta.Banco == null ? registroConta.Banco : conta.Banco;
                    registroConta.Agencia = conta.Agencia == null ? registroConta.Agencia : conta.Agencia;
                    registroConta.Conta = conta.Conta == null ? registroConta.Conta : conta.Conta;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Conta/id
        [HttpDelete("{codFundo}/{codTipoConta}")]
        public async Task<IActionResult> DeletarConta(int codFundo, int codTipoConta)
        {
            var tblConta = await _context.TblContas.FindAsync(codFundo, codTipoConta);

            if (tblConta == null)
            {
                return NotFound();
            }

            _context.TblContas.Remove(tblConta);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DESATIVA: api/Fundo/id
        [HttpPut("{codFundo}/{codTipoConta}")]
        public async Task<IActionResult> DesativarConta(int codFundo, int codTipoConta)
        {
            var registroConta = _context.TblContas.Find(codFundo, codTipoConta);

            if (registroConta != null)
            {
                registroConta.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool ContasExists(int id)
        {
            return _context.TblContas.Any(e => e.Id == id);
        }
        #endregion

        #region Tipo Conta
        // GET: api/TipoContas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoConta>>> TipoContas()
        {
            return await _context.TblTipoConta.Where(c => c.Ativo == true).OrderBy(c => c.TipoConta).ToListAsync();
        }

        // GET: api/TipoContas/5
        [HttpGet]
        public async Task<ActionResult<TblTipoConta>> GetTipoContas(int id)
        {
            var tblContas = await _context.TblTipoConta.FindAsync(id);

            if (tblContas == null)
            {
                return NotFound();
            }

            return tblContas;
        }

        [HttpPost]
        public async Task<ActionResult<TipoContaModel>> CadastrarTipoConta(TipoContaModel tblTipoContaModel)
        {
            var itensTipoConta = new TblTipoConta
            {
                Id = tblTipoContaModel.Id,
                TipoConta = tblTipoContaModel.TipoConta,
                DescricaoConta = tblTipoContaModel.DescricaoConta,
                UsuarioModificacao = tblTipoContaModel.UsuarioModificacao
            };

            _context.TblTipoConta.Add(itensTipoConta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetContas),
                new
                {
                    Id = itensTipoConta.Id,
                },
                Ok(itensTipoConta));
        }

        //PUT: api/TipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTipoConta(int id, TipoContaModel tipoConta)
        {
            try
            {
                var registroTipoConta = _context.TblTipoConta.Find(id);

                if (registroTipoConta != null)
                {
                    registroTipoConta.TipoConta = tipoConta.TipoConta == null ? registroTipoConta.TipoConta : tipoConta.TipoConta;
                    registroTipoConta.DescricaoConta = tipoConta.DescricaoConta == null ? registroTipoConta.DescricaoConta : tipoConta.DescricaoConta;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException) when (!TipoContasExists(tipoConta.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/TipoConta/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTipoConta(int id)
        {
            var tblTipoConta = await _context.TblTipoConta.FindAsync(id);

            if (tblTipoConta == null)
            {
                return NotFound();
            }

            _context.TblTipoConta.Remove(tblTipoConta);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DESATIVA: api/TipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarTipoConta(int id)
        {
            var registroTipoConta = _context.TblTipoConta.Find(id);

            if (registroTipoConta != null)
            {
                registroTipoConta.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool TipoContasExists(int id)
        {
            return _context.TblTipoConta.Any(e => e.Id == id);
        }

        #endregion
    }
}
