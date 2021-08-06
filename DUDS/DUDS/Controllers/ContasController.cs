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
                List<TblContas> contas = await _context.TblContas.Where(c => c.Ativo == true).ToListAsync();

                if (contas != null)
                {
                    return Ok(new { contas, Mensagem.SucessoListar });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        // GET: api/Contas/GetContas/codFundo/codTipoConta
        [HttpGet]
        public async Task<ActionResult<TblContas>> GetContas(int codFundo, int codTipoConta)
        {
            TblContas tblContas = await _context.TblContas.FindAsync(codFundo, codTipoConta);

            try
            {
                if (tblContas != null)
                {
                    return Ok(new { tblContas, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        //POST: api/Contas/CadastrarConta/ContaModel
        [HttpPost]
        public async Task<ActionResult<ContaModel>> CadastrarConta(ContaModel tblContaModel)
        {
            TblContas itensConta = new TblContas
            {
                CodFundo = tblContaModel.CodFundo,
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
                    {
                        codFundo = itensConta.CodFundo,
                        codTipoConta = itensConta.CodTipoConta
                    },
                    Ok(new { itensConta, Mensagem.SucessoCadastrado }));
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
            }
        }

        //PUT: api/Conta/EditarConta/codFundo/codTipoConta
        [HttpPut("{codFundo}/{codTipoConta}")]
        public async Task<IActionResult> EditarConta(int codFundo, int codTipoConta, ContaModel conta)
        {
            try
            {
                TblContas registroConta = _context.TblContas.Find(codFundo, codTipoConta);

                if (registroConta != null)
                {
                    registroConta.CodFundo = conta.CodFundo == 0 ? registroConta.CodFundo : conta.CodFundo;
                    registroConta.CodTipoConta = conta.CodTipoConta == 0 ? registroConta.CodTipoConta : conta.CodTipoConta;
                    registroConta.Banco = conta.Banco == null ? registroConta.Banco : conta.Banco;
                    registroConta.Agencia = conta.Agencia == null ? registroConta.Agencia : conta.Agencia;
                    registroConta.Conta = conta.Conta == null ? registroConta.Conta : conta.Conta;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { Mensagem.SucessoAtualizado });
                    }
                    catch (Exception)
                    {
                        return BadRequest(new { Erro = true, Mensagem.ErroAtualizar });
                    }
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (DbUpdateConcurrencyException) when (!ContasExists(conta.Id))
            {
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/Conta/DeletarConta/codFundo/codTipoConta
        [HttpDelete("{codFundo}/{codTipoConta}")]
        public async Task<IActionResult> DeletarConta(int codFundo, int codTipoConta, int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblContas tblConta = await _context.TblContas.FindAsync(codFundo, codTipoConta);

                if (tblConta == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblContas.Remove(tblConta);
                    await _context.SaveChangesAsync();
                    return Ok(new { Mensagem.SucessoExcluido });
                }
                catch (Exception)
                {
                    return BadRequest(new { Erro = true, Mensagem.ErroExcluir });
                }
            }
            else
            {
                return BadRequest(new { Erro = true, Mensagem.ExisteRegistroDesativar });
            }
        }

        // DESATIVA: api/Contas/DesativarConta/codFundo/codTipoConta
        [HttpPut("{codFundo}/{codTipoConta}")]
        public async Task<IActionResult> DesativarConta(int codFundo, int codTipoConta, int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblContas registroConta = _context.TblContas.Find(codFundo, codTipoConta);

                if (registroConta != null)
                {
                    registroConta.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { Mensagem.SucessoDesativado });
                    }
                    catch (Exception)
                    {
                        return BadRequest(new { Erro = true, Mensagem.ErroDesativar });
                    }
                }
                else
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }
            }
            else
            {
                return BadRequest(new { Erro = true, Mensagem.ExisteRegistroDesativar });
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
                List<TblTipoConta> tiposConta = await _context.TblTipoConta.Where(c => c.Ativo == true).OrderBy(c => c.TipoConta).ToListAsync();

                if (tiposConta != null)
                {
                    return Ok(new { tiposConta, Mensagem.SucessoListar });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        // GET: api/Contas/TipoContas/id
        [HttpGet]
        public async Task<ActionResult<TblTipoConta>> GetTipoContas(int id)
        {
            TblTipoConta tblContas = await _context.TblTipoConta.FindAsync(id);

            try
            {
                if (tblContas != null)
                {
                    return Ok(new { tblContas, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
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
                    nameof(GetContas),
                    new
                    {
                        Id = itensTipoConta.Id,
                    },
                     Ok(new { itensTipoConta, Mensagem.SucessoCadastrado }));
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
            }
        }

        //PUT: api/Contas/TipoConta/id
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
                        return Ok(new { Mensagem.SucessoAtualizado });
                    }
                    catch (Exception)
                    {
                        return BadRequest(new { Erro = true, Mensagem.ErroAtualizar });
                    }
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (DbUpdateConcurrencyException) when (!TipoContasExists(tipoConta.Id))
            {
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/Contas/DeletarTipoConta/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTipoConta(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblTipoConta tblTipoConta = await _context.TblTipoConta.FindAsync(id);

                if (tblTipoConta == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblTipoConta.Remove(tblTipoConta);
                    await _context.SaveChangesAsync();
                    return Ok(new { Mensagem.SucessoExcluido });
                }
                catch (Exception)
                {
                    return BadRequest(new { Erro = true, Mensagem.ErroExcluir });
                }
            }
            else
            {
                return BadRequest(new { Erro = true, Mensagem.ExisteRegistroDesativar });
            }
        }

        // DESATIVA: api/DesativarTipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarTipoConta(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblTipoConta registroTipoConta = _context.TblTipoConta.Find(id);

                if (registroTipoConta != null)
                {
                    registroTipoConta.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { Mensagem.SucessoDesativado });
                    }
                    catch (Exception)
                    {
                        return BadRequest(new { Erro = true, Mensagem.ErroDesativar });
                    }
                }
                else
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }
            }
            else
            {
                return BadRequest(new { Erro = true, Mensagem.ExisteRegistroDesativar });
            }
        }

        private bool TipoContasExists(int id)
        {
            return _context.TblTipoConta.Any(e => e.Id == id);
        }

        #endregion
    }
}
