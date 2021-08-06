﻿using System;
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
    public class AcordoRemuneracaoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public AcordoRemuneracaoController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: api/AcordoRemuneracao/AcordoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAcordoRemuneracao>>> AcordoRemuneracao()
        {
            try
            {
                List<TblAcordoRemuneracao> acordosRemuneracao = await _context.TblAcordoRemuneracao.Where(c => c.Ativo == true).AsNoTracking().ToListAsync();

                if (acordosRemuneracao != null)
                {
                    return Ok(new { acordosRemuneracao, Mensagem.SucessoListar });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new
                {
                    Erro = true,
                    Mensagem.ErroPadrao
                });
            }
        }

        // GET: api/AcordoRemuneracao/GetAcordoRemuneracao/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAcordoRemuneracao>> GetAcordoRemuneracao(int id)
        {
            TblAcordoRemuneracao tblAcordoRemuneracao = await _context.TblAcordoRemuneracao.FindAsync(id);

            try
            {
                if (tblAcordoRemuneracao != null)
                {
                    return Ok(new { tblAcordoRemuneracao, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }

            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new
                {
                    Erro = true,
                    Mensagem.ErroPadrao
                });
            }
        }

        // POST: api/AcordoRemuneracao/CadastrarAcordoRemuneracao/AcordoRemuneracaoModel
        [HttpPost]
        public async Task<ActionResult<AcordoRemuneracaoModel>> CadastrarAcordoRemuneracao(AcordoRemuneracaoModel tblAcordoRemuneracaoModel)
        {
            TblAcordoRemuneracao itensAcordoRemuneracao = new TblAcordoRemuneracao
            {
                CodContratoDistribuicao = tblAcordoRemuneracaoModel.CodContratoDistribuicao,
                Inicio = tblAcordoRemuneracaoModel.Inicio,
                Fim = tblAcordoRemuneracaoModel.Fim,
                Percentual = tblAcordoRemuneracaoModel.Percentual,
                TipoTaxa = tblAcordoRemuneracaoModel.TipoTaxa,
                TipoRange = tblAcordoRemuneracaoModel.TipoRange,
                DataVigenciaInicio = tblAcordoRemuneracaoModel.DataVigenciaInicio,
                DataVigenciaFim = tblAcordoRemuneracaoModel.DataVigenciaFim,
                UsuarioModificacao = tblAcordoRemuneracaoModel.UsuarioModificacao,
                DataModificacao = tblAcordoRemuneracaoModel.DataModificacao
            };

            try
            {
                _context.TblAcordoRemuneracao.Add(itensAcordoRemuneracao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                   nameof(GetAcordoRemuneracao),
                   new { id = itensAcordoRemuneracao.Id },
                   Ok(new { itensAcordoRemuneracao, Mensagem.SucessoCadastrado }));
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
            }
        }

        //PUT: api/AcordoRemuneracao/EditarAcordoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarAcordoRemuneracao(int id, AcordoRemuneracaoModel acordoRemuneracao)
        {
            try
            {
                TblAcordoRemuneracao registroAcordoRemuneracao = _context.TblAcordoRemuneracao.Find(id);

                if (registroAcordoRemuneracao != null)
                {
                    registroAcordoRemuneracao.CodContratoDistribuicao = acordoRemuneracao.CodContratoDistribuicao == 0 ? registroAcordoRemuneracao.CodContratoDistribuicao : acordoRemuneracao.CodContratoDistribuicao;
                    registroAcordoRemuneracao.Inicio = acordoRemuneracao.Inicio == 0 ? registroAcordoRemuneracao.Inicio : acordoRemuneracao.Inicio;
                    registroAcordoRemuneracao.Fim = acordoRemuneracao.Fim == 0 ? registroAcordoRemuneracao.Fim : acordoRemuneracao.Fim;
                    registroAcordoRemuneracao.Percentual = acordoRemuneracao.Percentual == 0 ? registroAcordoRemuneracao.Percentual : acordoRemuneracao.Percentual;
                    registroAcordoRemuneracao.TipoTaxa = acordoRemuneracao.TipoTaxa == null ? registroAcordoRemuneracao.TipoTaxa : acordoRemuneracao.TipoTaxa;
                    registroAcordoRemuneracao.TipoRange = acordoRemuneracao.TipoRange == null ? registroAcordoRemuneracao.TipoRange : acordoRemuneracao.TipoRange;
                    registroAcordoRemuneracao.DataVigenciaInicio = acordoRemuneracao.DataVigenciaInicio == null ? registroAcordoRemuneracao.DataVigenciaInicio : acordoRemuneracao.DataVigenciaInicio;
                    registroAcordoRemuneracao.DataVigenciaFim = acordoRemuneracao.DataVigenciaFim == null ? registroAcordoRemuneracao.DataVigenciaFim : acordoRemuneracao.DataVigenciaFim;

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
            catch (DbUpdateConcurrencyException) when (!AcordoRemuneracaoExists(acordoRemuneracao.Id))
            {
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/AcordoRemuneracao/DeletarAcordoRemuneracao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAcordoRemuneracao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_acordo_remuneracao");

            if (!existeRegistro)
            {
                TblAcordoRemuneracao tblAcordoRemuneracao = await _context.TblAcordoRemuneracao.FindAsync(id);

                if (tblAcordoRemuneracao == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblAcordoRemuneracao.Remove(tblAcordoRemuneracao);
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

        // DESATIVA: api/AcordoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarAcordoRemuneracao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_acordo_remuneracao");

            if (!existeRegistro)
            {
                TblAcordoRemuneracao registroAcordoRemuneracao = _context.TblAcordoRemuneracao.Find(id);

                if (registroAcordoRemuneracao != null)
                {
                    registroAcordoRemuneracao.Ativo = false;

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

        private bool AcordoRemuneracaoExists(int id)
        {
            return _context.TblAcordoRemuneracao.Any(e => e.Id == id);
        }
    }
}
