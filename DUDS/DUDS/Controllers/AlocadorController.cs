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
    public class AlocadorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public AlocadorController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService; 
        }

        // GET: api/Alocador/Alocador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAlocador>>> Alocador()
        {
            try
            {
                List<TblAlocador> alocadores = await _context.TblAlocador.AsNoTracking().ToListAsync();

                if (alocadores.Count() == 0)
                {
                    return BadRequest(Mensagem.ErroListar);
                }

                if (alocadores != null)
                {
                    return Ok(new { alocadores, Mensagem.SucessoListar });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // GET: api/Alocador/GetAlocador/cod_investidor/cod_contrato_distribuicao
        [HttpGet("{cod_investidor}/{cod_contrato_distribuicao}")]
        public async Task<ActionResult<TblAlocador>> GetAlocador(int cod_investidor, int cod_contrato_distribuicao)
        {
            TblAlocador tblAlocador = await _context.TblAlocador.FindAsync(cod_investidor, cod_contrato_distribuicao);

            try
            {
                if (tblAlocador != null)
                {
                    return Ok(new { tblAlocador, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        //POST: api/Alocador/CadastrarAlocador/AlocadorModel
        [HttpPost]
        public async Task<ActionResult<AlocadorController>> CadastrarAlocador(AlocadorModel tblAlocadorModel)
        {
            TblAlocador itensAlocador = new TblAlocador
            {
                CodInvestidor = tblAlocadorModel.CodInvestidor,
                CodContratoDistribuicao = tblAlocadorModel.CodContratoDistribuicao,
                UsuarioModificacao = tblAlocadorModel.UsuarioModificacao,
                DataModificacao = tblAlocadorModel.DataModificacao
            };

            try
            {
                _context.TblAlocador.Add(itensAlocador);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetAlocador),
                    new {
                            cod_investidor = itensAlocador.CodInvestidor,
                            cod_contrato_distribuicao = itensAlocador.CodContratoDistribuicao
                        },
                    Ok(new { itensAlocador, Mensagem.SucessoCadastrado }));
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar });
            }
        }

        //PUT: api/Alocador/EditarAlocador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarAlocador(int id, AlocadorModel alocador)
        {
            try
            {
                TblAlocador registroAlocador = _context.TblAlocador.Where(c => c.Id == id).FirstOrDefault();

                if (registroAlocador != null)
                {
                    registroAlocador.CodInvestidor = alocador.CodInvestidor == 0 ? registroAlocador.CodInvestidor : alocador.CodInvestidor;
                    registroAlocador.CodContratoDistribuicao = alocador.CodContratoDistribuicao == 0 ? registroAlocador.CodContratoDistribuicao : alocador.CodContratoDistribuicao;
                    registroAlocador.UsuarioModificacao = alocador.UsuarioModificacao == null ? registroAlocador.UsuarioModificacao : alocador.UsuarioModificacao;
                    registroAlocador.DataModificacao = alocador.DataModificacao == null ? registroAlocador.DataModificacao : alocador.DataModificacao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(new { registroAlocador, Mensagem.SucessoAtualizado });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Erro = e, Mensagem.ErroAtualizar });
                    }
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (DbUpdateConcurrencyException e) when (!AlocadorExists(alocador.Id))
            {
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/Alocador/DeletarAlocador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAlocador(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_alocador");

            if (!existeRegistro)
            {
                TblAlocador tblAlocador = _context.TblAlocador.Where(c => c.Id == id).FirstOrDefault();

                if (tblAlocador == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblAlocador.Remove(tblAlocador);
                    await _context.SaveChangesAsync();
                    return Ok(new { Mensagem.SucessoExcluido });
                }
                catch (Exception e)
                {
                    return BadRequest(new { Erro = e, Mensagem.ErroExcluir });
                }
            }
            else
            {
                return BadRequest(Mensagem.ExisteRegistroDesativar);
            }
        }

        private bool AlocadorExists(int id)
        {
            return _context.TblAlocador.Any(e => e.Id == id);
        }
    }
}
