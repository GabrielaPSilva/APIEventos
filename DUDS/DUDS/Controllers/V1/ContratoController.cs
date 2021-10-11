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
using System.Collections.Concurrent;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
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
        // GET: api/Contrato/GetContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContrato>>> GetContrato()
        {
            try
            {
                List<TblContrato> contratos = await _context.TblContrato.Where(c => c.Ativo == true).OrderBy(c => c.CodDistribuidor).AsNoTracking().ToListAsync();

                if (contratos.Count == 0)
                {
                    return NotFound();
                }

                return Ok(contratos);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetContratoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContrato>> GetContratoById(int id)
        {
            try
            {
                TblContrato tblContrato = await _context.TblContrato.FindAsync(id);
                if (tblContrato == null)
                {
                    NotFound();
                }

                return Ok(tblContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/AddContrato/ContratoModel
        [HttpPost]
        public async Task<ActionResult<ContratoModel>> AddContrato(ContratoModel tblContratoModel)
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
                    new { id = itensContrato.Id }, itensContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContrato(int id, ContratoModel contrato)
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

        // DELETE: api/Contrato/DeleteContrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContrato(int id)
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

        // DESATIVA: api/Contrato/DisableContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableContrato(int id)
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

        // ATIVAR: api/Contrato/ActivateContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateContrato(int id)
        {
            TblContrato registroContrato = await _context.TblContrato.FindAsync(id);

            if (registroContrato != null)
            {
                registroContrato.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroContrato);
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

        private bool ContratoExists(int id)
        {
            return _context.TblContrato.Any(e => e.Id == id);
        }
        #endregion

        #region Sub Contrato

        // GET: api/Contrato/GetSubContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblSubContrato>>> GetSubContrato()
        {
            try
            {
                List<TblSubContrato> subContratos = await _context.TblSubContrato.Where(c => c.Status == "Ativo").OrderBy(c => c.CodContrato).AsNoTracking().ToListAsync();

                if (subContratos.Count == 0)
                {
                    return NotFound();
                }

                return Ok(subContratos);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetSubContratoById/
        [HttpGet("{id}")]
        public async Task<ActionResult<TblSubContrato>> GetSubContratoById(int id)
        {


            try
            {
                TblSubContrato tblSubContrato = await _context.TblSubContrato.FindAsync(id);
                if (tblSubContrato == null)
                {
                    NotFound();
                }
                return Ok(tblSubContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/AddSubContrato/SubContratoModel
        [HttpPost]
        public async Task<ActionResult<SubContratoModel>> AddSubContrato(SubContratoModel tblSubContratoModel)
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
                    new { id = itensSubContrato.Id }, itensSubContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateSubContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubContrato(int id, SubContratoModel subContrato)
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


        // DELETE: api/Contrato/DeleteSubContrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubContrato(int id)
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

        // DESATIVA: api/Contrato/DisableSubContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableSubContrato(int id)
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

        // ATIVAR: api/Administrador/ActivateSubContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateSubContrato(int id)
        {
            TblSubContrato registroSubContrato = await _context.TblSubContrato.FindAsync(id);

            if (registroSubContrato != null)
            {
                registroSubContrato.Status = "Ativo";

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroSubContrato);
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

        private bool SubContratoExists(int id)
        {
            return _context.TblSubContrato.Any(e => e.Id == id);
        }

        #endregion

        #region Contrato Alocador
        // GET: api/Contrato/GetContratoAlocador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoAlocador>>> GetContratoAlocador()
        {
            try
            {
                List<TblContratoAlocador> contratoAlocadores = await _context.TblContratoAlocador.AsNoTracking().ToListAsync();

                if (contratoAlocadores.Count == 0)
                {
                    return NotFound();
                }

                return Ok(contratoAlocadores);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetContratoAlocadorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoAlocador>> GetContratoAlocadorById(int id)
        {

            try
            {
                TblContratoAlocador tblContratoAlocador = await _context.TblContratoAlocador.FindAsync(id);
                if (tblContratoAlocador != null)
                {
                    NotFound();
                }
                return Ok(tblContratoAlocador);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/AddContratoAlocador/ContratoAlocadorModel
        [HttpPost]
        public async Task<ActionResult<ContratoAlocadorModel>> AddContratoAlocador(ContratoAlocadorModel tblContratoAlocadorModel)
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
                    new { id = itensContratoAlocador.Id }, itensContratoAlocador);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateContratoAlocador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContratoAlocador(int id, ContratoAlocadorModel contratoAlocador)
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


        // DELETE: api/Contrato/DeleteContratoAlocador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContratoAlocador(int id)
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
        // GET: api/Contrato/GetContratoFundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoFundo>>> GetContratoFundo()
        {
            try
            {
                List<TblContratoFundo> contratoFundos = await _context.TblContratoFundo.OrderBy(c => c.CodSubContrato).AsNoTracking().ToListAsync();

                if (contratoFundos.Count == 0)
                {
                    return NotFound();
                }

                return Ok(contratoFundos);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetContratoFundoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoFundo>> GetContratoFundoById(int id)
        {
            try
            {
                TblContratoFundo tblContratoFundo = await _context.TblContratoFundo.FindAsync(id);
                if (tblContratoFundo == null)
                {
                    NotFound();
                }
                return Ok(tblContratoFundo);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/AddContratoFundo/ContratoFundoModel
        [HttpPost]
        public async Task<ActionResult<ContratoFundoModel>> AddContratoFundo(ContratoFundoModel tblContratoFundoModel)
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
                    new { id = itensContratoFundo.Id }, itensContratoFundo);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateContratoFundo/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContratoFundo(int id, ContratoFundoModel contratoFundo)
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


        // DELETE: api/Contrato/DeleteContratoFundo/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContratoFundo(int id)
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
        // GET: api/Contrato/GetContratoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblContratoRemuneracao>>> GetContratoRemuneracao()
        {
            try
            {
                List<TblContratoRemuneracao> contratoRemuneracao = await _context.TblContratoRemuneracao.OrderBy(c => c.CodContratoFundo).AsNoTracking().ToListAsync();

                if (contratoRemuneracao.Count == 0)
                {
                    return NotFound();
                }

                return Ok(contratoRemuneracao);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetContratoRemuneracaoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblContratoRemuneracao>> GetContratoRemuneracaoById(int id)
        {
            try
            {
                TblContratoRemuneracao tblContratoRemuneracao = await _context.TblContratoRemuneracao.FindAsync(id);
                if (tblContratoRemuneracao == null)
                {
                    return NotFound();
                }
                return Ok(tblContratoRemuneracao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/AddContratoRemuneracao/ContratoRemuneracaoModel
        [HttpPost]
        public async Task<ActionResult<ContratoRemuneracaoModel>> AddContratoRemuneracao(ContratoRemuneracaoModel tblContratoRemuneracaoModel)
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
                    new { id = itensContratoRemuneracao.Id }, itensContratoRemuneracao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateContratoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContratoRemuneracao(int id, ContratoRemuneracaoModel contratoRemuneracao)
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


        // DELETE: api/Contrato/DeleteContratoRemuneracao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContratoRemuneracao(int id)
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

        

        #region Estrutura de Contratos Ativos e Inativos
        // GET: api/Contrato/GetEstruturaContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstruturaContratoModel>>> GetEstruturaContratoValidos()
        {
            // Left Join utilizando LINQ - para Gabriela
            var estruturaContrato = await (from contrato in _context.TblContrato
                                            from subContrato in _context.TblSubContrato.Where(sC => sC.CodContrato == contrato.Id)
                                            from contratoFundo in _context.TblContratoFundo.Where(cF => cF.CodSubContrato == subContrato.Id)
                                            from contratoRemuneracao in _context.TblContratoRemuneracao.Where(cR => cR.CodContratoFundo == contratoFundo.Id)
                                            from contratoAlocador in _context.TblContratoAlocador.Where(cA => cA.CodSubContrato == subContrato.Id).DefaultIfEmpty()
                                            from investidorDistribuidor in _context.TblInvestidorDistribuidor.Where(iD => iD.CodInvestidor == contratoAlocador.CodInvestidor).DefaultIfEmpty()
                                            where subContrato.Status != "Inativo"
                                            select new
                                            {
                                                CodContratoRemuneracao = contratoRemuneracao.Id,
                                                contratoRemuneracao.PercentualAdm,
                                                contratoRemuneracao.PercentualPfee,
                                                CodContrato = contrato.Id,
                                                contrato.CodTipoContrato,
                                                contrato.Parceiro,
                                                contrato.CodDistribuidor,
                                                CodSubContrato = subContrato.Id,
                                                subContrato.Versao,
                                                subContrato.Status,
                                                subContrato.ClausulaRetroatividade,
                                                subContrato.DataRetroatividade,
                                                subContrato.DataVigenciaInicio,
                                                subContrato.DataVigenciaFim,
                                                CodInvestidor = contratoAlocador == null ? (int?)null : contratoAlocador.CodInvestidor,
                                                CodContratoFundo = contratoFundo.Id,
                                                contratoFundo.CodFundo,
                                                contratoFundo.CodTipoCondicao,
                                                CodigoInvestidorDistribuidor = investidorDistribuidor == null ? String.Empty : investidorDistribuidor.CodInvestAdministrador,
                                                AdministradorCodigoInvestidor = investidorDistribuidor == null ? (int?)null : investidorDistribuidor.CodAdministrador,
                                                DistribuidorCodigoInvestidor = investidorDistribuidor == null ? (int?)null : investidorDistribuidor.CodDistribuidor
                                            }).AsNoTracking().ToListAsync();

            // Verificando a possibilidade de utilizar funções em paralelo para aumentar a velocidade de processamento.
            ConcurrentBag<EstruturaContratoModel> estruturaContratoValidoModel = new ConcurrentBag<EstruturaContratoModel>();
            Parallel.ForEach(
                estruturaContrato,
                x =>
                {
                    EstruturaContratoModel c = new EstruturaContratoModel
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
                        CodTipoContrato = x.CodTipoContrato,
                        Versao = x.Versao,
                        AdministradorCodigoInvestidor = x.AdministradorCodigoInvestidor,
                        CodigoInvestidorDistribuidor = x.CodigoInvestidorDistribuidor,
                        DistribuidorCodigoInvestidor = x.DistribuidorCodigoInvestidor,
                        CodContrato = x.CodContrato,
                        CodContratoFundo = x.CodContratoFundo,
                        CodSubContrato = x.CodSubContrato,
                        CodContratoRemuneracao = x.CodContratoRemuneracao
                    };
                    estruturaContratoValidoModel.Add(c);
                }
            );

            try
            {
                if (estruturaContratoValidoModel.IsEmpty)
                {
                    return NotFound();
                }

                return Ok(estruturaContratoValidoModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetEstruturaContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstruturaContratoModel>>> GetEstruturaContratoInativos()
        {
            // Left Join utilizando LINQ - para Gabriela
            var estruturaContrato = await (from contrato in _context.TblContrato
                                            from subContrato in _context.TblSubContrato.Where(sC => sC.CodContrato == contrato.Id)
                                            from contratoFundo in _context.TblContratoFundo.Where(cF => cF.CodSubContrato == subContrato.Id)
                                            from contratoRemuneracao in _context.TblContratoRemuneracao.Where(cR => cR.CodContratoFundo == contratoFundo.Id)
                                            from contratoAlocador in _context.TblContratoAlocador.Where(cA => cA.CodSubContrato == subContrato.Id).DefaultIfEmpty()
                                            from investidorDistribuidor in _context.TblInvestidorDistribuidor.Where(iD => iD.CodInvestidor == contratoAlocador.CodInvestidor).DefaultIfEmpty()
                                            where subContrato.Status == "Inativo"
                                            select new
                                            {
                                                CodContratoRemuneracao = contratoRemuneracao.Id,
                                                contratoRemuneracao.PercentualAdm,
                                                contratoRemuneracao.PercentualPfee,
                                                CodContrato = contrato.Id,
                                                contrato.CodTipoContrato,
                                                contrato.Parceiro,
                                                contrato.CodDistribuidor,
                                                CodSubContrato = subContrato.Id,
                                                subContrato.Versao,
                                                subContrato.Status,
                                                subContrato.ClausulaRetroatividade,
                                                subContrato.DataRetroatividade,
                                                subContrato.DataVigenciaInicio,
                                                subContrato.DataVigenciaFim,
                                                CodInvestidor = contratoAlocador == null ? (int?)null : contratoAlocador.CodInvestidor,
                                                CodContratoFundo = contratoFundo.Id,
                                                contratoFundo.CodFundo,
                                                contratoFundo.CodTipoCondicao,
                                                CodigoInvestidorDistribuidor = investidorDistribuidor == null ? String.Empty : investidorDistribuidor.CodInvestAdministrador,
                                                AdministradorCodigoInvestidor = investidorDistribuidor == null ? (int?)null : investidorDistribuidor.CodAdministrador,
                                                DistribuidorCodigoInvestidor = investidorDistribuidor == null ? (int?)null : investidorDistribuidor.CodDistribuidor
                                            }).AsNoTracking().ToListAsync();

            // Verificando a possibilidade de utilizar funções em paralelo para aumentar a velocidade de processamento.
            ConcurrentBag<EstruturaContratoModel> estruturaContratoValidoModel = new ConcurrentBag<EstruturaContratoModel>();
            Parallel.ForEach(
                estruturaContrato,
                x =>
                {
                    EstruturaContratoModel c = new EstruturaContratoModel
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
                        CodTipoContrato = x.CodTipoContrato,
                        Versao = x.Versao,
                        AdministradorCodigoInvestidor = x.AdministradorCodigoInvestidor,
                        CodigoInvestidorDistribuidor = x.CodigoInvestidorDistribuidor,
                        DistribuidorCodigoInvestidor = x.DistribuidorCodigoInvestidor,
                        CodContrato = x.CodContrato,
                        CodContratoFundo = x.CodContratoFundo,
                        CodSubContrato = x.CodSubContrato,
                        CodContratoRemuneracao = x.CodContratoRemuneracao
                    };
                    estruturaContratoValidoModel.Add(c);
                }
            );

            try
            {
                if (estruturaContratoValidoModel.IsEmpty)
                {
                    return NotFound();
                }

                return Ok(estruturaContratoValidoModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion
    }
}
