using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DUDS.Service.Interface;
using DUDS.Models.Contrato;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IConfiguracaoService _configService;
        private readonly IContratoService _contratoService;
        private readonly ISubContratoService _subContratoService;
        private readonly IContratoAlocadorService _contratoAlocadorService;
        private readonly IContratoFundoService _contratoFundoService;
        private readonly IContratoRemuneracaoService _contratoRemuneracao;
        private readonly ICondicaoRemuneracaoService _condicaoRemuneracao;
        private readonly IExcecaoContratoService _excecaoContratoService;

        public ContratoController(IConfiguracaoService configService,
            IContratoService contratoService,
            ISubContratoService subContratoService,
            IContratoAlocadorService contratoAlocadorService,
            IContratoFundoService contratoFundoService,
            IContratoRemuneracaoService contratoRemuneracao,
            ICondicaoRemuneracaoService condicaoRemuneracao,
            IExcecaoContratoService excecaoContratoService)
        {
            _configService = configService;
            _contratoService = contratoService;
            _subContratoService = subContratoService;
            _contratoAlocadorService = contratoAlocadorService;
            _contratoFundoService = contratoFundoService;
            _contratoRemuneracao = contratoRemuneracao;
            _condicaoRemuneracao = condicaoRemuneracao;
            _excecaoContratoService = excecaoContratoService;
        }

        #region Contrato
        // GET: api/Contrato/GetContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoViewModel>>> GetContratos()
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
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_contrato");

            if (!existeRegistro)
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
            else
            {
                return NotFound();
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
                return NotFound();
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
                return NotFound();
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
                return NotFound();
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
        public async Task<ActionResult<ContratoRemuneracaoModel>> UpdateContratoRemuneracao(int id, ContratoRemuneracaoModel contratoRemuneracao)
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
                return NotFound();
            }
        }

        #endregion 

        #region Condição Remuneração
        // GET: api/Condicao/GetCondicaoRemuneracao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CondicaoRemuneracaoViewModel>>> GetCondicaoRemuneracao()
        {
            try
            {
                var condicaoRemuneracao = await _condicaoRemuneracao.GetAllAsync();

                if (condicaoRemuneracao.Any())
                {
                    return Ok(condicaoRemuneracao);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Condicao/GetCondicaoRemuneracaoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<CondicaoRemuneracaoModel>> GetCondicaoRemuneracaoById(int id)
        {
            try
            {
                var condincaoRemuneracao = await _condicaoRemuneracao.GetByIdAsync(id);
                if (condincaoRemuneracao == null)
                {
                    return NotFound();
                }
                return Ok(condincaoRemuneracao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Condicao/AddCondicaoRemuneracao/CondicaoRemuneracaoModel
        [HttpPost]
        public async Task<ActionResult<CondicaoRemuneracaoModel>> AddCondicaoRemuneracao(CondicaoRemuneracaoModel condicaoRemuneracao)
        {
            try
            {
                bool retorno = await _condicaoRemuneracao.AddAsync(condicaoRemuneracao);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetCondicaoRemuneracaoById), new { id = condicaoRemuneracao.Id }, condicaoRemuneracao);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Condicao/UpdateCondicaoRemuneracao/id
        [HttpPut("{id}")]
        public async Task<ActionResult<CondicaoRemuneracaoModel>> UpdateCondicaoRemuneracao(int id, CondicaoRemuneracaoModel condicaoRemuneracao)
        {
            try
            {
                CondicaoRemuneracaoModel retornoContratoRemuneracao = await _condicaoRemuneracao.GetByIdAsync(condicaoRemuneracao.Id);
                if (retornoContratoRemuneracao == null)
                {
                    return NotFound();
                }
                condicaoRemuneracao.Id = id;
                bool retorno = await _condicaoRemuneracao.UpdateAsync(condicaoRemuneracao);
                if (retorno)
                {
                    return Ok(condicaoRemuneracao);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Condicao/DeleteCondicaoRemuneracao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCondicaoRemuneracao(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_condicao_remuneracao");

            if (!existeRegistro)
            {
                try
                {
                    bool retorno = await _condicaoRemuneracao.DeleteAsync(id);
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
                return NotFound();
            }
        }

        #endregion

        #region Estrutura de Contratos Ativos e Inativos
        // GET: api/Contrato/GetEstruturaContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstruturaContratoViewModel>>> GetEstruturaContratoValidos()
        {
            try
            {
                var contratosRebate = await _contratoService.GetContratosRebateAsync("Ativo");

                if (contratosRebate.Any())
                {
                    return Ok(contratosRebate);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Contrato/GetEstruturaContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstruturaContratoViewModel>>> GetEstruturaContratoInativos()
        {
            try
            {
                var contratosRebate = await _contratoService.GetContratosRebateAsync("Inativo");

                if (contratosRebate.Any())
                {
                    return Ok(contratosRebate);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion

        #region Excecao Contrato

        [HttpGet("{id}")]
        public async Task<ActionResult<ExcecaoContratoViewModel>> GetExcecaoContratoById(int id)
        {
            try
            {
                var excecaoContrato = await _excecaoContratoService.GetByIdAsync(id);
                if (excecaoContrato == null)
                {
                    return NotFound();
                }
                return Ok(excecaoContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // [HttpGet("{codContrato}/{codFundo}/{codInvestidor}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExcecaoContratoViewModel>>> GetExcecaoContratoByIds([FromQuery] int? codSubContrato, [FromQuery] int? codFundo, [FromQuery]  int? codInvestidorDistribuidor)
        {
            try
            {
                var excecaoContrato = await _excecaoContratoService.GetExcecaoContratoAsync(codSubContrato, codFundo, codInvestidorDistribuidor);
                if (!excecaoContrato.Any())
                {
                    return NotFound();
                }
                return Ok(excecaoContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExcecaoContratoModel>> AddExcecaoContrato(ExcecaoContratoModel excecaoContrato)
        {
            try
            {
                bool retorno = await _excecaoContratoService.AddAsync(excecaoContrato);
                return CreatedAtAction(nameof(GetExcecaoContratoById), new { id = excecaoContrato.Id }, excecaoContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateContrato/id
        [HttpPut("{id}")]
        public async Task<ActionResult<ExcecaoContratoModel>> UpdateExcecaoContrato(int id, ExcecaoContratoModel excecaoContrato)
        {
            try
            {
                ExcecaoContratoModel retornoExcecaoContrato = await _excecaoContratoService.GetByIdAsync(excecaoContrato.Id);
                if (retornoExcecaoContrato == null)
                {
                    return NotFound();
                }
                excecaoContrato.Id = id;
                bool retorno = await _excecaoContratoService.UpdateAsync(excecaoContrato);
                if (retorno)
                {
                    return Ok(excecaoContrato);
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
        public async Task<IActionResult> DeleteExcecaoContrato(int id)
        {
            try
            {
                bool retorno = await _excecaoContratoService.DeleteAsync(id);
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
        public async Task<ActionResult<ExcecaoContratoViewModel>> ActivateExcecaoContrato(int id)
        {
            try
            {
                bool retorno = await _excecaoContratoService.ActivateAsync(id);
                if (retorno)
                {
                    ExcecaoContratoViewModel excecaoContrato = await _excecaoContratoService.GetByIdAsync(id);
                    return Ok(excecaoContrato);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        #endregion
    }
}
