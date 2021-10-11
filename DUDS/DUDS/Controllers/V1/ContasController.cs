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

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
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
        // GET: api/Contas/GetContas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContas>>> GetContas()
        {
            try
            {
                var listaContas = await (
                                from contas in _context.TblContas
                                from tipoConta in _context.TblTipoConta.Where(c => c.Id == contas.CodTipoConta)
                                from fundo in _context.TblFundo.Where(c => c.Id == contas.CodFundo).DefaultIfEmpty()
                                from investidor in _context.TblInvestidor.Where(c => c.Id == contas.CodInvestidor).DefaultIfEmpty()
                                where contas.Ativo == true
                                select new
                                {
                                    contas.Id,
                                    contas.Agencia,
                                    contas.Banco,
                                    contas.Conta,
                                    contas.CodFundo,
                                    contas.CodInvestidor,
                                    contas.CodTipoConta,
                                    contas.UsuarioModificacao,
                                    contas.DataModificacao,
                                    contas.Ativo,
                                    tipoConta.TipoConta,
                                    tipoConta.DescricaoConta,
                                    NomeFundo = fundo == null ? String.Empty : fundo.NomeReduzido,
                                    NomeInvestidor = investidor == null ? String.Empty : investidor.NomeCliente,
                                }).AsNoTracking().ToListAsync();

                if (listaContas.Count == 0)
                {
                    return NotFound();
                }

                return Ok(listaContas);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Contas/GetContasById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContas>> GetContasById(int id)
        {
            try
            {
                var tblContas = await (
                               from contas in _context.TblContas
                               from tipoConta in _context.TblTipoConta.Where(c => c.Id == contas.CodTipoConta)
                               from fundo in _context.TblFundo.Where(c => c.Id == contas.CodFundo).DefaultIfEmpty()
                               from investidor in _context.TblInvestidor.Where(c => c.Id == contas.CodInvestidor).DefaultIfEmpty()
                               where contas.Id == id
                               select new
                               {
                                   contas.Id,
                                   contas.Agencia,
                                   contas.Banco,
                                   contas.Conta,
                                   contas.CodFundo,
                                   contas.CodInvestidor,
                                   contas.CodTipoConta,
                                   contas.UsuarioModificacao,
                                   contas.DataModificacao,
                                   contas.Ativo,
                                   tipoConta.TipoConta,
                                   tipoConta.DescricaoConta,
                                   NomeFundo = fundo == null ? String.Empty : fundo.NomeReduzido,
                                   NomeInvestidor = investidor == null ? String.Empty : investidor.NomeCliente,
                               }).FirstOrDefaultAsync();

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

        // GET: api/Contas/GetContasExistsBase/cod_fundo/cod_investidor/cod_tipo_conta
        [HttpGet("{cod_tipo_conta}")]
        public async Task<ActionResult<TblContas>> GetContasExistsBase(int cod_fundo, int cod_investidor, int cod_tipo_conta)
        {
            try
            {
                var tblContas = await (
                                 from contas in _context.TblContas
                                 from tipoConta in _context.TblTipoConta.Where(c => c.Id == contas.CodTipoConta)
                                 from fundo in _context.TblFundo.Where(c => c.Id == contas.CodFundo).DefaultIfEmpty()
                                 from investidor in _context.TblInvestidor.Where(c => c.Id == contas.CodInvestidor).DefaultIfEmpty()
                                 where contas.Ativo == false && (contas.CodFundo == cod_fundo || contas.CodInvestidor == cod_investidor) && contas.CodTipoConta == cod_tipo_conta
                                 select new
                                 {
                                     contas.Id,
                                     contas.Agencia,
                                     contas.Banco,
                                     contas.Conta,
                                     contas.CodFundo,
                                     contas.CodInvestidor,
                                     contas.CodTipoConta,
                                     contas.UsuarioModificacao,
                                     contas.DataModificacao,
                                     contas.Ativo,
                                     tipoConta.TipoConta,
                                     tipoConta.DescricaoConta,
                                     NomeFundo = fundo == null ? String.Empty : fundo.NomeReduzido,
                                     NomeInvestidor = investidor == null ? String.Empty : investidor.NomeCliente,
                                 }).FirstOrDefaultAsync();

                if (tblContas != null)
                {
                    return Ok(tblContas);
                }
                else
                {
                    tblContas = await (
                                 from contas in _context.TblContas
                                 from tipoConta in _context.TblTipoConta.Where(c => c.Id == contas.CodTipoConta)
                                 from fundo in _context.TblFundo.Where(c => c.Id == contas.CodFundo).DefaultIfEmpty()
                                 from investidor in _context.TblInvestidor.Where(c => c.Id == contas.CodInvestidor).DefaultIfEmpty()
                                 where (contas.CodFundo == cod_fundo || contas.CodInvestidor == cod_investidor) && contas.CodTipoConta == cod_tipo_conta
                                 select new
                                 {
                                     contas.Id,
                                     contas.Agencia,
                                     contas.Banco,
                                     contas.Conta,
                                     contas.CodFundo,
                                     contas.CodInvestidor,
                                     contas.CodTipoConta,
                                     contas.UsuarioModificacao,
                                     contas.DataModificacao,
                                     contas.Ativo,
                                     tipoConta.TipoConta,
                                     tipoConta.DescricaoConta,
                                     NomeFundo = fundo == null ? String.Empty : fundo.NomeReduzido,
                                     NomeInvestidor = investidor == null ? String.Empty : investidor.NomeCliente,
                                 }).FirstOrDefaultAsync();

                    return Ok(tblContas);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //POST: api/Contas/AddConta/ContaModel
        [HttpPost]
        public async Task<ActionResult<ContaModel>> AddConta(ContaModel tblContaModel)
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
                    { id = itensConta.Id }, itensConta);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Conta/UpdateConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConta(int id, ContaModel conta)
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

        // DELETE: api/Conta/DeleteConta/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConta(int id)
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

        // DESATIVA: api/Contas/DisableConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableConta(int id)
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

        // ATIVAR: api/Contas/ActivateContas/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateContas(int id)
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
    }
}
