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
        private readonly IContratoService _contratoService;
        private readonly ISubContratoService _subContratoService;
        private readonly IContratoAlocadorService _contratoAlocadorService;
        private readonly IContratoFundoService _contratoFundoService;
        private readonly IContratoRemuneracaoService _contratoRemuneracao;

        public ContratoController(DataContext context, IConfiguracaoService configService, 
            IContratoService contratoService, 
            ISubContratoService subContratoService, 
            IContratoAlocadorService contratoAlocadorService,
            IContratoFundoService contratoFundoService,
            IContratoRemuneracaoService contratoRemuneracao)
        {
            _context = context;
            _configService = configService;
            _contratoService = contratoService;
            _subContratoService = subContratoService;
            _contratoAlocadorService = contratoAlocadorService;
            _contratoFundoService = contratoFundoService;
            _contratoRemuneracao = contratoRemuneracao;
        }

        #region Contrato
        // GET: api/Contrato/GetContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoModel>>> GetContratos()
        {
            try
            {
                var contratos = await _contratoService.GetAllAsync();

                if (contratos.Any())
                {
                    return Ok(contratos);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetContratoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ContratoModel>> GetContratoById(int id)
        {
            try
            {
                var contrato = await _contratoService.GetByIdAsync(id);
                if (contrato == null)
                {
                    return NotFound();
                }
                return Ok(contrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/AddContrato/ContratoModel
        [HttpPost]
        public async Task<ActionResult<ContratoModel>> AddContrato(ContratoModel contrato)
        {
            try
            {
                bool retorno = await _contratoService.AddAsync(contrato);
                return CreatedAtAction(nameof(GetContratoById), new { id = contrato.Id }, contrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateContrato/id
        [HttpPut("{id}")]
        public async Task<ActionResult<ContratoModel>> UpdateContrato(int id, ContratoModel contrato)
        {
            try
            {
                ContratoModel retornoContrato = await _contratoService.GetByIdAsync(contrato.Id);
                if (retornoContrato == null)
                {
                    return NotFound();
                }
                contrato.Id = id;
                bool retorno = await _contratoService.UpdateAsync(contrato);
                if (retorno)
                {
                    return Ok(contrato);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Contrato/DeleteContrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            try
            {
                bool retorno = await _contratoService.DisableAsync(id);
                if (retorno)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // ATIVAR: api/Contrato/ActivateContrato/id
        [HttpPut("{id}")]
        public async Task<ActionResult<ContratoModel>> ActivateContrato(int id)
        {
            try
            {
                bool retorno = await _contratoService.ActivateAsync(id);
                if (retorno)
                {
                    ContratoModel contrato = await _contratoService.GetByIdAsync(id);
                    return Ok(contrato);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        
        #endregion

        #region Sub Contrato

        // GET: api/Contrato/GetSubContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubContratoModel>>> GetSubContratos()
        {
            try
            {
                var subContratos = await _contratoAlocadorService.GetAllAsync();

                if (subContratos.Any())
                {
                    return Ok(subContratos);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetSubContratoById/
        [HttpGet("{id}")]
        public async Task<ActionResult<SubContratoModel>> GetSubContratoById(int id)
        {
            try
            {
                var subContrato = await _subContratoService.GetByIdAsync(id);
                if (subContrato == null)
                {
                    return NotFound();
                }
                return Ok(subContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/AddSubContrato/SubContratoModel
        [HttpPost]
        public async Task<ActionResult<SubContratoModel>> AddSubContrato(SubContratoModel subContrato)
        {
            try
            {
                bool retorno = await _subContratoService.AddAsync(subContrato);
                return CreatedAtAction(nameof(GetSubContratoById), new { id = subContrato.Id }, subContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateSubContrato/id
        [HttpPut("{id}")]
        public async Task<ActionResult<SubContratoModel>> UpdateSubContrato(int id, SubContratoModel subContrato)
        {
            try
            {
                SubContratoModel retornoSubContrato = await _subContratoService.GetByIdAsync(subContrato.Id);
                if (retornoSubContrato == null)
                {
                    return NotFound();
                }
                subContrato.Id = id;
                bool retorno = await _subContratoService.UpdateAsync(subContrato);
                if (retorno)
                {
                    return Ok(subContrato);
                }
                return NotFound();
            }
            catch (Exception e)
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
                try
                {
                    bool retorno = await _subContratoService.DeleteAsync(id);
                    if (retorno)
                    {
                        return Ok();
                    }
                    return NotFound();
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

        #endregion

        #region Contrato Alocador
        // GET: api/Contrato/GetContratosAlocadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoAlocadorModel>>> GetContratosAlocadores()
        {
            try
            {
                var contratoAlocador = await _contratoAlocadorService.GetAllAsync();

                if (contratoAlocador.Any())
                {
                    return Ok(contratoAlocador);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetContratoAlocadorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ContratoAlocadorModel>> GetContratoAlocadorById(int id)
        {
            try
            {
                var contratoAlocador = await _contratoAlocadorService.GetByIdAsync(id);
                if (contratoAlocador == null)
                {
                    return NotFound();
                }
                return Ok(contratoAlocador);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/AddContratoAlocador/ContratoAlocadorModel
        [HttpPost]
        public async Task<ActionResult<ContratoAlocadorModel>> AddContratoAlocador(ContratoAlocadorModel contratoAlocador)
        {
            try
            {
                bool retorno = await _contratoAlocadorService.AddAsync(contratoAlocador);
                return CreatedAtAction(nameof(GetContratoAlocadorById), new { id = contratoAlocador.Id }, contratoAlocador);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateContratoAlocador/id
        [HttpPut("{id}")]
        public async Task<ActionResult<ContratoAlocadorModel>> UpdateContratoAlocador(int id, ContratoAlocadorModel contratoAlocador)
        {
            try
            {
                ContratoAlocadorModel retornoContratoAlocador = await _contratoAlocadorService.GetByIdAsync(contratoAlocador.Id);
                if (retornoContratoAlocador == null)
                {
                    return NotFound();
                }
                contratoAlocador.Id = id;
                bool retorno = await _contratoAlocadorService.UpdateAsync(contratoAlocador);
                if (retorno)
                {
                    return Ok(contratoAlocador);
                }
                return NotFound();
            }
            catch (Exception e)
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
                try
                {
                    bool retorno = await _contratoAlocadorService.DeleteAsync(id);
                    if (retorno)
                    {
                        return Ok();
                    }
                    return NotFound();
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

        #endregion

        #region Contrato Fundo
        // GET: api/Contrato/GetContratoFundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoFundoModel>>> GetContratoFundo()
        {
            try
            {
                var contratoFundo = await _contratoFundoService.GetAllAsync();

                if (contratoFundo.Any())
                {
                    return Ok(contratoFundo);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetContratoFundoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ContratoFundoModel>> GetContratoFundoById(int id)
        {
            try
            {
                var contratoFundo = await _contratoFundoService.GetByIdAsync(id);
                if (contratoFundo == null)
                {
                    return NotFound();
                }
                return Ok(contratoFundo);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/AddContratoFundo/ContratoFundoModel
        [HttpPost]
        public async Task<ActionResult<ContratoFundoModel>> AddContratoFundo(ContratoFundoModel contratoFundo)
        {
            try
            {
                bool retorno = await _contratoFundoService.AddAsync(contratoFundo);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetContratoFundoById), new { id = contratoFundo.Id }, contratoFundo);
                }
                return NotFound();
                
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateContratoFundo/id
        [HttpPut("{id}")]
        public async Task<ActionResult<ContratoFundoModel>> UpdateContratoFundo(int id, ContratoFundoModel contratoFundo)
        {
            try
            {
                ContratoFundoModel retornoContratoFundo = await _contratoFundoService.GetByIdAsync(contratoFundo.Id);
                if (retornoContratoFundo == null)
                {
                    return NotFound();
                }
                contratoFundo.Id = id;
                bool retorno = await _contratoFundoService.UpdateAsync(contratoFundo);
                if (retorno)
                {
                    return Ok(contratoFundo);
                }
                return NotFound();
            }
            catch (Exception e)
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
                try
                {
                    bool retorno = await _contratoFundoService.DeleteAsync(id);
                    if (retorno)
                    {
                        return Ok();
                    }
                    return NotFound();
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

        #endregion

        #region Contrato Remuneração
        // GET: api/Contrato/GetContratoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoRemuneracaoModel>>> GetContratoRemuneracao()
        {
            try
            {
                var contratoRemuneracao = await _contratoRemuneracao.GetAllAsync();

                if (contratoRemuneracao.Any())
                {
                    return Ok(contratoRemuneracao);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetContratoRemuneracaoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ContratoRemuneracaoModel>> GetContratoRemuneracaoById(int id)
        {
            try
            {
                var contratoRemuneracao = await _contratoRemuneracao.GetByIdAsync(id);
                if (contratoRemuneracao == null)
                {
                    return NotFound();
                }
                return Ok(contratoRemuneracao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Contrato/AddContratoRemuneracao/ContratoRemuneracaoModel
        [HttpPost]
        public async Task<ActionResult<ContratoRemuneracaoModel>> AddContratoRemuneracao(ContratoRemuneracaoModel contratoRemuneracao)
        {
            try
            {
                bool retorno = await _contratoRemuneracao.AddAsync(contratoRemuneracao);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetContratoFundoById), new { id = contratoRemuneracao.Id }, contratoRemuneracao);
                }
                return NotFound();

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
                ContratoRemuneracaoModel retornoContratoRemuneracao = await _contratoRemuneracao.GetByIdAsync(contratoRemuneracao.Id);
                if (retornoContratoRemuneracao == null)
                {
                    return NotFound();
                }
                contratoRemuneracao.Id = id;
                bool retorno = await _contratoRemuneracao.UpdateAsync(contratoRemuneracao);
                if (retorno)
                {
                    return Ok(contratoRemuneracao);
                }
                return NotFound();
            }
            catch (Exception e)
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
                try
                {
                    bool retorno = await _contratoRemuneracao.DeleteAsync(id);
                    if (retorno)
                    {
                        return Ok();
                    }
                    return NotFound();
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

        #endregion 

        #region Condição Remuneração
        // GET: api/Condicao/GetCondicaoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCondicaoRemuneracao>>> GetCondicaoRemuneracao()
        {
            try
            {
                List<TblCondicaoRemuneracao> condicoesRemuneracao = await _context.TblCondicaoRemuneracao.Where(c => c.Ativo == true).OrderBy(c => c.CodContratoRemuneracao).AsNoTracking().ToListAsync();
                if (condicoesRemuneracao.Count == 0)
                {
                    return NotFound();
                }

                return Ok(condicoesRemuneracao);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Condicao/GetCondicaoRemuneracaoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCondicaoRemuneracao>> GetCondicaoRemuneracaoById(int id)
        {
            try
            {
                TblCondicaoRemuneracao tblCondicaoRemuneracao = await _context.TblCondicaoRemuneracao.FindAsync(id);
                if (tblCondicaoRemuneracao != null)
                {
                    return NotFound();
                }

                return Ok(tblCondicaoRemuneracao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Condicao/AddCondicaoRemuneracao/CondicaoRemuneracaoModel
        [HttpPost]
        public async Task<ActionResult<CondicaoRemuneracaoModel>> AddCondicaoRemuneracao(CondicaoRemuneracaoModel tblCondicaoRemuneracaoModel)
        {
            TblCondicaoRemuneracao itensCondicaoRemuneracao = new TblCondicaoRemuneracao
            {
                CodContratoRemuneracao = tblCondicaoRemuneracaoModel.CodContratoRemuneracao,
                CodFundo = tblCondicaoRemuneracaoModel.CodFundo,
                DataInicio = tblCondicaoRemuneracaoModel.DataInicio,
                DataFim = tblCondicaoRemuneracaoModel.DataFim,
                ValorPosicaoInicio = tblCondicaoRemuneracaoModel.ValorPosicaoInicio,
                ValorPosicaoFim = tblCondicaoRemuneracaoModel.ValorPosicaoFim,
                ValorPgtoFixo = tblCondicaoRemuneracaoModel.ValorPgtoFixo,
                UsuarioModificacao = tblCondicaoRemuneracaoModel.UsuarioModificacao
            };

            try
            {
                _context.TblCondicaoRemuneracao.Add(itensCondicaoRemuneracao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetCondicaoRemuneracao),
                    new { id = itensCondicaoRemuneracao.Id }, itensCondicaoRemuneracao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Condicao/UpdateCondicaoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCondicaoRemuneracao(int id, CondicaoRemuneracaoModel condicaoRemuneracao)
        {
            try
            {
                TblCondicaoRemuneracao registroCondicaoRemuneracao = _context.TblCondicaoRemuneracao.Find(id);

                if (registroCondicaoRemuneracao != null)
                {
                    registroCondicaoRemuneracao.CodContratoRemuneracao = condicaoRemuneracao.CodContratoRemuneracao == 0 ? registroCondicaoRemuneracao.CodContratoRemuneracao : condicaoRemuneracao.CodContratoRemuneracao;
                    registroCondicaoRemuneracao.CodFundo = condicaoRemuneracao.CodFundo == 0 ? registroCondicaoRemuneracao.CodFundo : condicaoRemuneracao.CodFundo;
                    registroCondicaoRemuneracao.DataInicio = condicaoRemuneracao.DataInicio == null ? registroCondicaoRemuneracao.DataInicio : condicaoRemuneracao.DataInicio;
                    registroCondicaoRemuneracao.DataFim = condicaoRemuneracao.DataFim == null ? registroCondicaoRemuneracao.DataFim : condicaoRemuneracao.DataFim;
                    registroCondicaoRemuneracao.ValorPosicaoInicio = (condicaoRemuneracao.ValorPosicaoInicio == null || condicaoRemuneracao.ValorPosicaoInicio == 0) ? registroCondicaoRemuneracao.ValorPosicaoInicio : condicaoRemuneracao.ValorPosicaoInicio;
                    registroCondicaoRemuneracao.ValorPosicaoFim = (condicaoRemuneracao.ValorPosicaoFim == null || condicaoRemuneracao.ValorPosicaoFim == 0) ? registroCondicaoRemuneracao.ValorPosicaoFim : condicaoRemuneracao.ValorPosicaoFim;
                    registroCondicaoRemuneracao.ValorPgtoFixo = (condicaoRemuneracao.ValorPgtoFixo == null || condicaoRemuneracao.ValorPgtoFixo == 0) ? registroCondicaoRemuneracao.ValorPgtoFixo : condicaoRemuneracao.ValorPgtoFixo;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroCondicaoRemuneracao);
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
            catch (DbUpdateConcurrencyException e) when (!CondicaoRemuneracaoExists(condicaoRemuneracao.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/Condicao/DeleteCondicaoRemuneracao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCondicaoRemuneracao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_condicao_remuneracao");

            if (!existeRegistro)
            {
                TblCondicaoRemuneracao tblCondicaoRemuneracao = await _context.TblCondicaoRemuneracao.FindAsync(id);

                if (tblCondicaoRemuneracao == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblCondicaoRemuneracao.Remove(tblCondicaoRemuneracao);
                    await _context.SaveChangesAsync();
                    return Ok(tblCondicaoRemuneracao);
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

        // DESATIVA: api/Condicao/DisableCondicaoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableCondicaoRemuneracao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_condicao_remuneracao");

            if (!existeRegistro)
            {
                TblCondicaoRemuneracao registroCondicaoRemuneracao = _context.TblCondicaoRemuneracao.Find(id);

                if (registroCondicaoRemuneracao != null)
                {
                    registroCondicaoRemuneracao.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroCondicaoRemuneracao);
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

        // ATIVAR: api/Condicao/ActivateCondicaoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateCondicaoRemuneracao(int id)
        {
            TblCondicaoRemuneracao registroCondicaoRemuneracao = await _context.TblCondicaoRemuneracao.FindAsync(id);

            if (registroCondicaoRemuneracao != null)
            {
                registroCondicaoRemuneracao.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroCondicaoRemuneracao);
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

        private bool CondicaoRemuneracaoExists(int id)
        {
            return _context.TblCondicaoRemuneracao.Any(e => e.Id == id);
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
