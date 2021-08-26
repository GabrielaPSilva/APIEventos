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
using System.Collections.Concurrent;

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

        #region Contrato
        // GET: api/Contrato/Contrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContrato>>> Contrato()
        {
            try
            {
                List<TblContrato> contratos = await _context.TblContrato.Where(c => c.Ativo == true).OrderBy(c => c.CodDistribuidor).AsNoTracking().ToListAsync();

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
                    return NotFound();
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
                CodTipoContrato = tblContratoModel.CodTipoContrato,
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
                    registroContrato.CodDistribuidor = contrato.CodDistribuidor == 0 ? registroContrato.CodDistribuidor : contrato.CodDistribuidor;
                    registroContrato.CodTipoContrato = contrato.CodTipoContrato == 0 ? registroContrato.CodTipoContrato : contrato.CodTipoContrato;
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
        #endregion

        #region Sub Contrato

        // GET: api/Contrato/SubContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblSubContrato>>> SubContrato()
        {
            try
            {
                List<TblSubContrato> subContratos = await _context.TblSubContrato.Where(c => c.Status == "Ativo").OrderBy(c => c.CodContrato).AsNoTracking().ToListAsync();

                if (subContratos.Count() == 0)
                {
                    return NotFound();
                }

                if (subContratos != null)
                {
                    return Ok(subContratos);
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

        // GET: api/Contrato/GetSubContrato/
        [HttpGet("{id}")]
        public async Task<ActionResult<TblSubContrato>> GetSubContrato(int id)
        {
            TblSubContrato tblSubContrato = await _context.TblSubContrato.FindAsync(id);

            try
            {
                if (tblSubContrato != null)
                {
                    return Ok(tblSubContrato);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/CadastrarSubContrato/SubContratoModel
        [HttpPost]
        public async Task<ActionResult<SubContratoModel>> CadastrarSubContrato(SubContratoModel tblSubContratoModel)
        {
            TblSubContrato itensSubContrato = new TblSubContrato
            {
                CodContrato = tblSubContratoModel.CodContrato,
                Versao = tblSubContratoModel.Versao,
                Status = tblSubContratoModel.Status,
                IdDocusign = tblSubContratoModel.IdDocusign,
                DataInclusaoContrato = tblSubContratoModel.DataInclusaoContrato,
                ClausulaRetroatividade = tblSubContratoModel.ClausulaRetroatividade,
                DataRetroatividade = tblSubContratoModel.DataRetroatividade,
                DataAssinatura = tblSubContratoModel.DataAssinatura,
                DataVigenciaInicio = tblSubContratoModel.DataVigenciaInicio,
                DataVigenciaFim = tblSubContratoModel.DataVigenciaFim,
                UsuarioModificacao = tblSubContratoModel.UsuarioModificacao
            };

            try
            {
                _context.TblSubContrato.Add(itensSubContrato);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetSubContrato),
                    new { id = itensSubContrato.Id },
                     Ok(itensSubContrato));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/EditarSubContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarSubContrato(int id, SubContratoModel subContrato)
        {
            try
            {
                TblSubContrato registroSubContrato = _context.TblSubContrato.Find(id);

                if (registroSubContrato != null)
                {
                    registroSubContrato.CodContrato = subContrato.CodContrato == 0 ? registroSubContrato.CodContrato : subContrato.CodContrato;
                    registroSubContrato.Versao = subContrato.Versao == null ? registroSubContrato.Versao : subContrato.Versao;
                    registroSubContrato.Status = subContrato.Status == null ? registroSubContrato.Status : subContrato.Status;
                    registroSubContrato.IdDocusign = subContrato.IdDocusign == null ? registroSubContrato.IdDocusign : subContrato.IdDocusign;
                    registroSubContrato.DataInclusaoContrato = subContrato.DataInclusaoContrato == null ? registroSubContrato.DataInclusaoContrato : subContrato.DataInclusaoContrato;
                    registroSubContrato.ClausulaRetroatividade = subContrato.ClausulaRetroatividade == false ? registroSubContrato.ClausulaRetroatividade : subContrato.ClausulaRetroatividade;
                    registroSubContrato.DataRetroatividade = subContrato.DataRetroatividade == null ? registroSubContrato.DataRetroatividade : subContrato.DataRetroatividade;
                    registroSubContrato.DataAssinatura = subContrato.DataAssinatura == null ? registroSubContrato.DataAssinatura : subContrato.DataAssinatura;
                    registroSubContrato.DataVigenciaInicio = subContrato.DataVigenciaInicio == null ? registroSubContrato.DataVigenciaInicio : subContrato.DataVigenciaInicio;
                    registroSubContrato.DataVigenciaFim = subContrato.DataVigenciaFim == null ? registroSubContrato.DataVigenciaFim : subContrato.DataVigenciaFim;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroSubContrato);
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
            catch (DbUpdateConcurrencyException e) when (!SubContratoExists(subContrato.Id))
            {
                return BadRequest(e);
            }
        }


        // DELETE: api/Contrato/DeletarSubContrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarSubContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_sub_contrato");

            if (!existeRegistro)
            {
                TblSubContrato tblSubContrato = await _context.TblSubContrato.FindAsync(id);

                if (tblSubContrato == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblSubContrato.Remove(tblSubContrato);
                    await _context.SaveChangesAsync();
                    return Ok(tblSubContrato);
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

        // DESATIVA: api/Contrato/DesativarSubContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarSubContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_sub_contrato");

            if (!existeRegistro)
            {
                var registroSubContrato = _context.TblSubContrato.Find(id);

                if (registroSubContrato != null)
                {
                    registroSubContrato.Status = "Inativo";

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroSubContrato);
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

        private bool SubContratoExists(int id)
        {
            return _context.TblSubContrato.Any(e => e.Id == id);
        }

        #endregion

        #region Contrato Alocador
        // GET: api/Contrato/ContratoAlocador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoAlocador>>> ContratoAlocador()
        {
            try
            {
                List<TblContratoAlocador> contratoAlocadores = await _context.TblContratoAlocador.AsNoTracking().ToListAsync();

                if (contratoAlocadores.Count() == 0)
                {
                    return NotFound();
                }

                if (contratoAlocadores != null)
                {
                    return Ok(contratoAlocadores);
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

        // GET: api/Contrato/GetContratoAlocador/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoAlocador>> GetContratoAlocador(int id)
        {
            TblContratoAlocador tblContratoAlocador = await _context.TblContratoAlocador.FindAsync(id);

            try
            {
                if (tblContratoAlocador != null)
                {
                    return Ok(tblContratoAlocador);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/CadastrarContratoAlocador/ContratoAlocadorModel
        [HttpPost]
        public async Task<ActionResult<ContratoAlocadorModel>> CadastrarContratoAlocador(ContratoAlocadorModel tblContratoAlocadorModel)
        {
            TblContratoAlocador itensContratoAlocador = new TblContratoAlocador
            {
                CodInvestidor = tblContratoAlocadorModel.CodInvestidor,
                CodSubContrato = tblContratoAlocadorModel.CodSubContrato,
                DataTransferencia = tblContratoAlocadorModel.DataTransferencia,
                UsuarioModificacao = tblContratoAlocadorModel.UsuarioModificacao
            };

            try
            {
                _context.TblContratoAlocador.Add(itensContratoAlocador);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetContratoAlocador),
                    new { id = itensContratoAlocador.Id },
                     Ok(itensContratoAlocador));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/EditarContratoAlocador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarContratoAlocador(int id, ContratoAlocadorModel contratoAlocador)
        {
            try
            {
                TblContratoAlocador registroContratoAlocador = _context.TblContratoAlocador.Find(id);

                if (registroContratoAlocador != null)
                {
                    registroContratoAlocador.CodInvestidor = contratoAlocador.CodInvestidor == 0 ? registroContratoAlocador.CodInvestidor : contratoAlocador.CodInvestidor;
                    registroContratoAlocador.CodSubContrato = contratoAlocador.CodSubContrato == 0 ? registroContratoAlocador.CodSubContrato : contratoAlocador.CodSubContrato;
                    registroContratoAlocador.DataTransferencia = contratoAlocador.DataTransferencia == null ? registroContratoAlocador.DataTransferencia : contratoAlocador.DataTransferencia;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroContratoAlocador);
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
            catch (DbUpdateConcurrencyException e) when (!ContratoAlocadorExists(contratoAlocador.Id))
            {
                return BadRequest(e);
            }
        }


        // DELETE: api/Contrato/DeletarContratoAlocador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContratoAlocador(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contrato_alocador");

            if (!existeRegistro)
            {
                TblContratoAlocador tblContratoAlocador = await _context.TblContratoAlocador.FindAsync(id);

                if (tblContratoAlocador == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblContratoAlocador.Remove(tblContratoAlocador);
                    await _context.SaveChangesAsync();
                    return Ok(tblContratoAlocador);
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

        private bool ContratoAlocadorExists(int id)
        {
            return _context.TblContratoAlocador.Any(e => e.Id == id);
        }
        #endregion

        #region Contrato Fundo
        // GET: api/Contrato/ContratoFundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoFundo>>> ContratoFundo()
        {
            try
            {
                List<TblContratoFundo> contratoFundos = await _context.TblContratoFundo.OrderBy(c => c.CodSubContrato).AsNoTracking().ToListAsync();

                if (contratoFundos.Count() == 0)
                {
                    return NotFound();
                }

                if (contratoFundos != null)
                {
                    return Ok(contratoFundos);
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

        // GET: api/Contrato/GetContratoFundo/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoFundo>> GetContratoFundo(int id)
        {
            TblContratoFundo tblContratoFundo = await _context.TblContratoFundo.FindAsync(id);

            try
            {
                if (tblContratoFundo != null)
                {
                    return Ok(tblContratoFundo);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/CadastrarContratoFundo/ContratoFundoModel
        [HttpPost]
        public async Task<ActionResult<ContratoFundoModel>> CadastrarContratoFundo(ContratoFundoModel tblContratoFundoModel)
        {
            TblContratoFundo itensContratoFundo = new TblContratoFundo
            {
                CodSubContrato = tblContratoFundoModel.CodSubContrato,
                CodFundo = tblContratoFundoModel.CodFundo,
                CodTipoCondicao = tblContratoFundoModel.CodTipoCondicao,
                UsuarioModificacao = tblContratoFundoModel.UsuarioModificacao
            };

            try
            {
                _context.TblContratoFundo.Add(itensContratoFundo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetContratoFundo),
                    new { id = itensContratoFundo.Id },
                     Ok(itensContratoFundo));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/EditarContratoFundo/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarContratoFundo(int id, ContratoFundoModel contratoFundo)
        {
            try
            {
                TblContratoFundo registroContratoFundo = _context.TblContratoFundo.Find(id);

                if (registroContratoFundo != null)
                {
                    registroContratoFundo.CodSubContrato = contratoFundo.CodSubContrato == 0 ? registroContratoFundo.CodSubContrato : contratoFundo.CodSubContrato;
                    registroContratoFundo.CodFundo = contratoFundo.CodFundo == 0 ? registroContratoFundo.CodFundo : contratoFundo.CodFundo;
                    registroContratoFundo.CodTipoCondicao = contratoFundo.CodTipoCondicao == 0 ? registroContratoFundo.CodTipoCondicao : contratoFundo.CodTipoCondicao;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroContratoFundo);
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
            catch (DbUpdateConcurrencyException e) when (!ContratoFundoExists(contratoFundo.Id))
            {
                return BadRequest(e);
            }
        }


        // DELETE: api/Contrato/DeletarContratoFundo/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContratoFundo(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contrato_fundo");

            if (!existeRegistro)
            {
                TblContratoFundo tblContratoFundo = await _context.TblContratoFundo.FindAsync(id);

                if (tblContratoFundo == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblContratoFundo.Remove(tblContratoFundo);
                    await _context.SaveChangesAsync();
                    return Ok(tblContratoFundo);
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

        private bool ContratoFundoExists(int id)
        {
            return _context.TblContratoFundo.Any(e => e.Id == id);
        }
        #endregion

        #region Contrato Remuneração
        // GET: api/Contrato/ContratoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoRemuneracao>>> ContratoRemuneracao()
        {
            try
            {
                List<TblContratoRemuneracao> contratoRemuneracao = await _context.TblContratoRemuneracao.OrderBy(c => c.CodContratoFundo).AsNoTracking().ToListAsync();

                if (contratoRemuneracao.Count() == 0)
                {
                    return NotFound();
                }

                if (contratoRemuneracao != null)
                {
                    return Ok(contratoRemuneracao);
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

        // GET: api/Contrato/GetContratoRemuneracao/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoRemuneracao>> GetContratoRemuneracao(int id)
        {
            TblContratoRemuneracao tblContratoRemuneracao = await _context.TblContratoRemuneracao.FindAsync(id);

            try
            {
                if (tblContratoRemuneracao != null)
                {
                    return Ok(tblContratoRemuneracao);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/CadastrarContratoRemuneracao/ContratoRemuneracaoModel
        [HttpPost]
        public async Task<ActionResult<ContratoRemuneracaoModel>> CadastrarContratoRemuneracao(ContratoRemuneracaoModel tblContratoRemuneracaoModel)
        {
            TblContratoRemuneracao itensContratoRemuneracao = new TblContratoRemuneracao
            {
                CodContratoFundo = tblContratoRemuneracaoModel.CodContratoFundo,
                PercentualAdm = tblContratoRemuneracaoModel.PercentualAdm,
                PercentualPfee = tblContratoRemuneracaoModel.PercentualPfee,
                UsuarioModificacao = tblContratoRemuneracaoModel.UsuarioModificacao,
            };

            try
            {
                _context.TblContratoRemuneracao.Add(itensContratoRemuneracao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetContratoRemuneracao),
                    new { id = itensContratoRemuneracao.Id },
                     Ok(itensContratoRemuneracao));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/EditarContratoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarContratoRemuneracao(int id, ContratoRemuneracaoModel contratoRemuneracao)
        {
            try
            {
                TblContratoRemuneracao registroContratoRemuneracao = _context.TblContratoRemuneracao.Find(id);

                if (registroContratoRemuneracao != null)
                {
                    registroContratoRemuneracao.CodContratoFundo = contratoRemuneracao.CodContratoFundo == 0 ? registroContratoRemuneracao.CodContratoFundo : contratoRemuneracao.CodContratoFundo;
                    registroContratoRemuneracao.PercentualAdm = contratoRemuneracao.PercentualAdm == 0 ? registroContratoRemuneracao.PercentualAdm : contratoRemuneracao.PercentualAdm;
                    registroContratoRemuneracao.PercentualPfee = contratoRemuneracao.PercentualPfee == 0 ? registroContratoRemuneracao.PercentualPfee : contratoRemuneracao.PercentualPfee;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroContratoRemuneracao);
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
            catch (DbUpdateConcurrencyException e) when (!ContratoRemuneracaoExists(contratoRemuneracao.Id))
            {
                return BadRequest(e);
            }
        }


        // DELETE: api/Contrato/DeletarContratoRemuneracao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContratoRemuneracao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contrato_remuneracao");

            if (!existeRegistro)
            {
                TblContratoRemuneracao tblContratoRemuneracao = await _context.TblContratoRemuneracao.FindAsync(id);

                if (tblContratoRemuneracao == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblContratoRemuneracao.Remove(tblContratoRemuneracao);
                    await _context.SaveChangesAsync();
                    return Ok(tblContratoRemuneracao);
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

        private bool ContratoRemuneracaoExists(int id)
        {
            return _context.TblContratoRemuneracao.Any(e => e.Id == id);
        }
        #endregion

        #region Estrutura de Contratos Válidos e Inválidos
        // GET: api/Contrato/GetEstruturaContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstruturaContratoValidoModel>>> GetEstruturaContratoValidos()
        {
            // Left Join utilizando LINQ - para Gabriela
            var estruturaContrato = await (from contrato in _context.TblContrato
                                           join subContrato in _context.TblSubContrato on contrato.Id equals subContrato.CodContrato
                                           join contratoAlocador in _context.TblContratoAlocador on subContrato.Id equals (int?)contratoAlocador.CodSubContrato into contratoSubContratoAlocador
                                           from contratoSubcontratoAlocadorNull in contratoSubContratoAlocador.DefaultIfEmpty()
                                           join contratoFundo in _context.TblContratoFundo on subContrato.Id equals contratoFundo.CodSubContrato
                                           join contratoRemuneracao in _context.TblContratoRemuneracao on contratoFundo.Id equals contratoRemuneracao.CodContratoFundo
                                           where subContrato.Status != "Inativo"
                                           select new
                                           {
                                               contratoRemuneracao.PercentualAdm,
                                               contratoRemuneracao.PercentualPfee,
                                               contrato.CodTipoContrato,
                                               contrato.Parceiro,
                                               contrato.CodDistribuidor,
                                               subContrato.Versao,
                                               subContrato.Status,
                                               subContrato.ClausulaRetroatividade,
                                               subContrato.DataRetroatividade,
                                               subContrato.DataVigenciaInicio,
                                               subContrato.DataVigenciaFim,
                                               CodInvestidor = (contratoSubcontratoAlocadorNull == null? (int?)null : contratoSubcontratoAlocadorNull.CodInvestidor),
                                               contratoFundo.CodFundo,
                                               contratoFundo.CodTipoCondicao
                                           }).AsNoTracking().ToListAsync();


            /*
            var estruturaContrato = await _context.TblContrato
                .Join(_context.TblSubContrato,
                contrato => contrato.Id,
                subContrato => subContrato.CodContrato,
                (contrato, subContrato) => new
                {
                    contrato.CodTipoContrato,
                    contrato.Parceiro,
                    contrato.CodDistribuidor,
                    subContrato.Versao,
                    subContrato.Status,
                    subContrato.ClausulaRetroatividade,
                    subContrato.DataRetroatividade,
                    subContrato.DataVigenciaInicio,
                    subContrato.DataVigenciaFim,
                    IdSubContrato = subContrato.Id,
                    IdContrato = contrato.Id
                }
                )
                .Join(_context.TblContratoAlocador,
                contratoSubContrato => contratoSubContrato.IdSubContrato,
                contratoAlocador => contratoAlocador.CodSubContrato,
                (contratoSubContrato, contratoAlocador) => new
                {
                    IdInvestidor = contratoAlocador.CodInvestidor,
                    IdContratoAlocacor = contratoAlocador.Id,
                    contratoSubContrato
                }
                )
                .Join(_context.TblContratoFundo,
                contratoSubContratoAlocador => contratoSubContratoAlocador.contratoSubContrato.IdSubContrato,
                contratoFundo => contratoFundo.CodSubContrato,
                (contratoSubContratoAlocador, contratoFundo) => new
                {
                    IdContratoFundo = contratoFundo.Id,
                    contratoFundo.CodFundo,
                    contratoFundo.CodTipoCondicao,
                    contratoSubContratoAlocador
                }
                )
                .Join(_context.TblContratoRemuneracao,
                contratoSubContratoAlocadorFundo => contratoSubContratoAlocadorFundo.IdContratoFundo,
                contratoRemuneracao => contratoRemuneracao.CodContratoFundo,
                (contratoSubContratoAlocadorFundo, contratoRemuneracao) => new
                {
                    contratoRemuneracao.PercentualAdm,
                    contratoRemuneracao.PercentualPfee,
                    contratoSubContratoAlocadorFundo.contratoSubContratoAlocador.contratoSubContrato.CodTipoContrato,
                    contratoSubContratoAlocadorFundo.contratoSubContratoAlocador.contratoSubContrato.Parceiro,
                    contratoSubContratoAlocadorFundo.contratoSubContratoAlocador.contratoSubContrato.CodDistribuidor,
                    contratoSubContratoAlocadorFundo.contratoSubContratoAlocador.contratoSubContrato.Versao,
                    contratoSubContratoAlocadorFundo.contratoSubContratoAlocador.contratoSubContrato.Status,
                    contratoSubContratoAlocadorFundo.contratoSubContratoAlocador.contratoSubContrato.ClausulaRetroatividade,
                    contratoSubContratoAlocadorFundo.contratoSubContratoAlocador.contratoSubContrato.DataRetroatividade,
                    contratoSubContratoAlocadorFundo.contratoSubContratoAlocador.contratoSubContrato.DataVigenciaInicio,
                    contratoSubContratoAlocadorFundo.contratoSubContratoAlocador.contratoSubContrato.DataVigenciaFim,
                    contratoSubContratoAlocadorFundo.contratoSubContratoAlocador.IdInvestidor,
                    contratoSubContratoAlocadorFundo.CodFundo,
                    contratoSubContratoAlocadorFundo.CodTipoCondicao
                }
                ).Where(c => c.Status != "Inativo").AsNoTracking().ToListAsync();
            */
            // Verificando a possibilidade de utilizar funções em paralelo para aumentar a velocidade de processamento.
            ConcurrentBag < EstruturaContratoValidoModel > estruturaContratoValidoModel = new ConcurrentBag<EstruturaContratoValidoModel>();
            Parallel.ForEach(
                estruturaContrato,
                x =>
                {
                    EstruturaContratoValidoModel c = new EstruturaContratoValidoModel
                    {
                        ClausulaRetroatividade = x.ClausulaRetroatividade,
                        CodDistribuidor = x.CodDistribuidor,
                        CodFundo = x.CodFundo,
                        CodTipoCondicao = x.CodTipoCondicao,
                        DataRetroatividade = x.DataRetroatividade,
                        DataVigenciaFim = x.DataVigenciaFim,
                        DataVigenciaInicio = x.DataVigenciaInicio,
                        IdInvestidor = x.CodInvestidor,
                        Parceiro = x.Parceiro,
                        PercentualAdm = x.PercentualAdm,
                        PercentualPfee = x.PercentualPfee,
                        Status = x.Status,
                        //TipoContrato = x.TipoContrato,
                        Versao = x.Versao
                    };
                    estruturaContratoValidoModel.Add(c);
                }

            );
            /*
            List<EstruturaContratoValidoModel> estruturaContratoValidoModel = estruturaContrato
                    .ConvertAll(
                    x => new EstruturaContratoValidoModel
                    {
                        ClausulaRetroatividade = x.ClausulaRetroatividade,
                        CodDistribuidor = x.CodDistribuidor,
                        CodFundo = x.CodFundo,
                        CodTipoCondicao = x.CodTipoCondicao,
                        DataRetroatividade = x.DataRetroatividade,
                        DataVigenciaFim = x.DataVigenciaFim,
                        DataVigenciaInicio = x.DataVigenciaInicio,
                        IdInvestidor = x.IdInvestidor,
                        Parceiro = x.Parceiro,
                        PercentualAdm = x.PercentualAdm,
                        PercentualPfee = x.PercentualPfee,
                        Status = x.Status,
                        //TipoContrato = x.CodTipoContrato,
                        Versao = x.Versao
                    }
                    );
            */
            try
            {
                if (estruturaContratoValidoModel.Count == 0)
                {
                    return NotFound();
                }

                if (estruturaContratoValidoModel != null)
                {
                    return Ok(estruturaContratoValidoModel);
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
        #endregion
    }
}
