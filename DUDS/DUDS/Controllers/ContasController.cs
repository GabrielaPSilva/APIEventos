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
    public class ContasController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public ContasController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        #region Conta
        // GET: api/Contas/Contas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContas>>> Contas()
        {
            try
            {
                var contas = await _context.TblContas.Where(c => c.Ativo == true).ToListAsync();

                if (contas.Count == 0)
                {
                    return NotFound();
                }

                return Ok(contas);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Contas/GetContas/cod_fundo/cod_tipo_conta
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContas>> GetContas(int id)
        {
            try
            {
                TblContas tblContas = await _context.TblContas.FindAsync(id);
                if (tblContas == null)
                {
                    NotFound();
                }
                return Ok(tblContas);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //// GET: api/Contas/GetContasExiste/cod_fundo/cod_investidor/cod_tipo_conta
        [HttpGet("{cod_tipo_conta}")]
        public async Task<ActionResult<TblContas>> GetContasExiste(int cod_fundo, int cod_investidor, int cod_tipo_conta)
        {
            TblContas tblContas = new TblContas();

            try
            {
                tblContas = await _context.TblContas.Where(c => c.Ativo == false && (c.CodFundo == cod_fundo || c.CodInvestidor == cod_investidor) && c.CodTipoConta == cod_tipo_conta).FirstOrDefaultAsync();

                if (tblContas != null)
                {
                    return Ok(tblContas);
                }
                else
                {
                    tblContas = await _context.TblContas.Where(c => c.CodTipoConta == cod_tipo_conta && (c.CodFundo == cod_fundo || c.CodInvestidor == cod_investidor)).FirstOrDefaultAsync();
                    return Ok(tblContas);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //POST: api/Contas/CadastrarConta/ContaModel
        [HttpPost]
        public async Task<ActionResult<ContaModel>> CadastrarConta(ContaModel tblContaModel)
        {
            TblContas itensConta = new TblContas
            {
                CodFundo = tblContaModel.CodFundo,
                CodInvestidor = tblContaModel.CodInvestidor,
                CodTipoConta = tblContaModel.CodTipoConta,
                Banco = tblContaModel.Banco,
                Agencia = tblContaModel.Agencia,
                Conta = tblContaModel.Conta,
                UsuarioModificacao = tblContaModel.UsuarioModificacao
            };

            try
            {
                _context.TblContas.Add(itensConta);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetContas),
                    new
                    { id = itensConta.Id },
                    Ok(itensConta));
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Conta/EditarConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarConta(int id, ContaModel conta)
        {
            try
            {
                TblContas registroConta = _context.TblContas.Find(id);

                if (registroConta != null)
                {
                    registroConta.CodFundo = conta.CodFundo == 0 ? registroConta.CodFundo : conta.CodFundo;
                    registroConta.CodInvestidor = conta.CodInvestidor == 0 ? registroConta.CodInvestidor : conta.CodInvestidor;
                    registroConta.CodTipoConta = conta.CodTipoConta == 0 ? registroConta.CodTipoConta : conta.CodTipoConta;
                    registroConta.Banco = conta.Banco == null ? registroConta.Banco : conta.Banco;
                    registroConta.Agencia = conta.Agencia == null ? registroConta.Agencia : conta.Agencia;
                    registroConta.Conta = conta.Conta == null ? registroConta.Conta : conta.Conta;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroConta);
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
            catch (DbUpdateConcurrencyException e) when (!ContasExists(conta.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/Conta/DeletarConta/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarConta(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contas");

            if (!existeRegistro)
            {
                TblContas tblConta = _context.TblContas.Find(id);

                if (tblConta == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblContas.Remove(tblConta);
                    await _context.SaveChangesAsync();
                    return Ok(tblConta);
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

        // DESATIVA: api/Contas/DesativarConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarConta(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contas");

            if (!existeRegistro)
            {
                TblContas registroConta = _context.TblContas.Find(id);

                if (registroConta != null)
                {
                    registroConta.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroConta);
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

        // ATIVAR: api/Contas/DesativarContas/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarContas(int id)
        {
            TblContas registroContas = await _context.TblContas.FindAsync(id);

            if (registroContas != null)
            {
                registroContas.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroContas);
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

        private bool ContasExists(int id)
        {
            return _context.TblContas.Any(e => e.Id == id);
        }
        #endregion

        #region Tipo Conta
        // GET: api/Contas/TipoContas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoConta>>> TipoContas()
        {
            try
            {
                var tiposConta = await _context.TblTipoConta.Where(c => c.Ativo == true).OrderBy(c => c.TipoConta).ToListAsync();

                if (tiposConta.Count == 0)
                {
                    return NotFound();
                }

                return Ok(tiposConta);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Contas/TipoContas/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTipoConta>> GetTipoContas(int id)
        {
            try
            {
                TblTipoConta tblContas = await _context.TblTipoConta.FindAsync(id);
                if (tblContas == null)
                {
                    NotFound();
                }

                return Ok(tblContas);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //POST: api/Contas/CadastrarTipoConta/TipoContaModel
        [HttpPost]
        public async Task<ActionResult<TipoContaModel>> CadastrarTipoConta(TipoContaModel tblTipoContaModel)
        {
            TblTipoConta itensTipoConta = new TblTipoConta
            {
                Id = tblTipoContaModel.Id,
                TipoConta = tblTipoContaModel.TipoConta,
                DescricaoConta = tblTipoContaModel.DescricaoConta,
                UsuarioModificacao = tblTipoContaModel.UsuarioModificacao
            };

            try
            {
                _context.TblTipoConta.Add(itensTipoConta);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetTipoContas),
                    new
                    {
                        Id = itensTipoConta.Id,
                    },
                     Ok(itensTipoConta));
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Contas/EditarTipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTipoConta(int id, TipoContaModel tipoConta)
        {
            try
            {
                TblTipoConta registroTipoConta = _context.TblTipoConta.Find(id);

                if (registroTipoConta != null)
                {
                    registroTipoConta.TipoConta = tipoConta.TipoConta == null ? registroTipoConta.TipoConta : tipoConta.TipoConta;
                    registroTipoConta.DescricaoConta = tipoConta.DescricaoConta == null ? registroTipoConta.DescricaoConta : tipoConta.DescricaoConta;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroTipoConta);
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
            catch (DbUpdateConcurrencyException e) when (!TipoContasExists(tipoConta.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/Contas/DeletarTipoConta/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTipoConta(int id)
        {
            TblTipoConta tblTipoConta = await _context.TblTipoConta.FindAsync(id);

            if (tblTipoConta == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblTipoConta.Remove(tblTipoConta);
                await _context.SaveChangesAsync();
                return Ok(tblTipoConta);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DESATIVA: api/Conta/DesativarTipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarTipoConta(int id)
        {
            TblTipoConta registroTipoConta = _context.TblTipoConta.Find(id);

            if (registroTipoConta != null)
            {
                registroTipoConta.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroTipoConta);
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

        // ATIVAR: api/Conta/AtivarTipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> AtivarTipoConta(int id)
        {
            TblTipoConta registroTipoConta = await _context.TblTipoConta.FindAsync(id);

            if (registroTipoConta != null)
            {
                registroTipoConta.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroTipoConta);
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

        private bool TipoContasExists(int id)
        {
            return _context.TblTipoConta.Any(e => e.Id == id);
        }

        #endregion
    }
}
