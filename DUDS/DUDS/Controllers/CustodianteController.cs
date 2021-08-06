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
    public class CustodianteController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public CustodianteController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/Custodiante/Custodiante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCustodiante>>> Custodiante()
        {
            try
            {
                List<TblCustodiante> custodiantes = await _context.TblCustodiante.Where(c => c.Ativo == true).OrderBy(c => c.NomeCustodiante).AsNoTracking().ToListAsync();

                if (custodiantes != null)
                {
                    return Ok(new { custodiantes, Mensagem.SucessoListar });
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

        // GET: api/Custodiante/GetCustodiante/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCustodiante>> GetCustodiante(int id)
        {
            TblCustodiante tblCustodiante = await _context.TblCustodiante.FindAsync(id);

            try
            {
                if (tblCustodiante != null)
                {
                    return Ok(new { tblCustodiante, Mensagem.SucessoCadastrado });
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

        //POST: api/Custodiante/CadastrarCustodiante/CustodianteModel
        [HttpPost]
        public async Task<ActionResult<CustodianteModel>> CadastrarCustodiante(CustodianteModel tblCustodianteModel)
        {
            TblCustodiante itensCustodiante = new TblCustodiante
            {
                NomeCustodiante = tblCustodianteModel.NomeCustodiante,
                UsuarioModificacao = tblCustodianteModel.UsuarioModificacao,
                DataModificacao = tblCustodianteModel.DataModificacao
            };

            try
            {
                _context.TblCustodiante.Add(itensCustodiante);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetCustodiante),
                    new { id = itensCustodiante.Id },
                   Ok(new { itensCustodiante, Mensagem.SucessoCadastrado }));
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
            }
        }

        //PUT: api/Custodiante/EditarCustodiante/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCustodiante(int id, CustodianteModel custodiante)
        {
            try
            {
                TblCustodiante registroCustodiante = _context.TblCustodiante.Find(id);

                if (registroCustodiante != null)
                {
                    registroCustodiante.NomeCustodiante = custodiante.NomeCustodiante == null ? registroCustodiante.NomeCustodiante : custodiante.NomeCustodiante;

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
            catch (DbUpdateConcurrencyException) when (!CustodianteExists(custodiante.Id))
            {
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/Custodiante/DeletarCustodiante/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCustodiante(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblCustodiante tblCustodiante = await _context.TblCustodiante.FindAsync(id);

                if (tblCustodiante == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblCustodiante.Remove(tblCustodiante);
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

        // DESATIVA: api/Custodiante/DesativarCustodiante/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarCustodiante(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblCustodiante registroCustodiante = _context.TblCustodiante.Find(id);

                if (registroCustodiante != null)
                {
                    registroCustodiante.Ativo = false;

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

        private bool CustodianteExists(int id)
        {
            return _context.TblCustodiante.Any(e => e.Id == id);
        }
    }
}
