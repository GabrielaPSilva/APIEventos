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

                if (contratos != null)
                {
                    return Ok(new { contratos, Mensagem.SucessoListar });
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

        // GET: api/Contrato/GetContrato/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContrato>> GetContrato(int id)
        {
            TblContrato tblContrato = await _context.TblContrato.FindAsync(id);

            try
            {
                if (tblContrato != null)
                {
                    return Ok(new { tblContrato, Mensagem.SucessoCadastrado });
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

        //POST: api/Contrato/CadastrarContrato/ContratoModel
        [HttpPost]
        public async Task<ActionResult<ContratoModel>> CadastrarContrato(ContratoModel tblContratoModel)
        {
            TblContrato itensContrato = new TblContrato
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

            try
            {
                _context.TblContrato.Add(itensContrato);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetContrato),
                    new { id = itensContrato.Id },
                      Ok(new { itensContrato, Mensagem.SucessoCadastrado }));
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
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
                    registroContrato.Versao = contrato.Versao == null ? registroContrato.Versao : contrato.Versao;
                    registroContrato.Status = contrato.Status == null ? registroContrato.Status : contrato.Status;
                    registroContrato.IdDocusign = contrato.IdDocusign == null ? registroContrato.IdDocusign : contrato.IdDocusign;
                    registroContrato.DirecaoPagamento = contrato.DirecaoPagamento == null ? registroContrato.DirecaoPagamento : contrato.DirecaoPagamento;
                    registroContrato.ClausulaRetroatividade = contrato.ClausulaRetroatividade == false ? registroContrato.ClausulaRetroatividade : contrato.ClausulaRetroatividade;
                    registroContrato.DataRetroatividade = contrato.DataRetroatividade == null ? registroContrato.DataRetroatividade : contrato.DataRetroatividade;
                    registroContrato.DataAssinatura = contrato.DataAssinatura == null ? registroContrato.DataAssinatura : contrato.DataAssinatura;

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
            catch (DbUpdateConcurrencyException) when (!ContratoExists(contrato.Id))
            {
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/Contrato/DeletarContrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblContrato tblContrato = await _context.TblContrato.FindAsync(id);

                if (tblContrato == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblContrato.Remove(tblContrato);
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

        // DESATIVA: api/Contrato/DesativarContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblContrato registroContrato = _context.TblContrato.Find(id);

                if (registroContrato != null)
                {
                    registroContrato.Ativo = false;

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
                List<TblContratoDistribuicao> contratosDistribuicao = await _context.TblContratoDistribuicao.ToListAsync();
                
                if (contratosDistribuicao != null)
                {
                    return Ok(new { contratosDistribuicao, Mensagem.SucessoListar });
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

        // GET: api/Contrato/GetContratoDistribuicao/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoDistribuicao>> GetContratoDistribuicao(int id)
        {
            TblContratoDistribuicao tblContratoDistribuicao = await _context.TblContratoDistribuicao.FindAsync(id);

            try
            {
                if (tblContratoDistribuicao != null)
                {
                    return Ok(new { tblContratoDistribuicao, Mensagem.SucessoCadastrado });
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

        //POST: api/Contrato/CadastrarContratoDistribuicao/ContratoDistribuicaoModel
        [HttpPost]
        public async Task<ActionResult<ContratoDistribuicaoModel>> CadastrarContratoDistribuicao(ContratoDistribuicaoModel tblContratoDistribuicaoModel)
        {
            TblContratoDistribuicao itensContratoDistribuicao = new TblContratoDistribuicao
            {
                CodContrato = tblContratoDistribuicaoModel.CodContrato,
                CodFundo = tblContratoDistribuicaoModel.CodFundo,
                UsuarioModificacao = tblContratoDistribuicaoModel.UsuarioModificacao,
                DataModificacao = tblContratoDistribuicaoModel.DataModificacao
            };

            try
            {
                _context.TblContratoDistribuicao.Add(itensContratoDistribuicao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetContratoDistribuicao),
                    new { id = itensContratoDistribuicao.Id },
                    Ok(new { itensContratoDistribuicao, Mensagem.SucessoCadastrado }));
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
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
                    registroContratoDistribuicao.CodContrato = contratoDistribuicao.CodContrato == 0 ? registroContratoDistribuicao.CodContrato : contratoDistribuicao.CodContrato;
                    registroContratoDistribuicao.CodFundo  = contratoDistribuicao.CodFundo == 0 ? registroContratoDistribuicao.CodFundo : contratoDistribuicao.CodFundo;

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
            catch (DbUpdateConcurrencyException) when (!ContratoDistribuicaoExists(contratoDistribuicao.Id))
            {
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
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
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblContratoDistribuicao.Remove(tblContratoDistribuicao);
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

        private bool ContratoDistribuicaoExists(int id)
        {
            return _context.TblContratoDistribuicao.Any(e => e.Id == id);
        }
        #endregion
    }
}
