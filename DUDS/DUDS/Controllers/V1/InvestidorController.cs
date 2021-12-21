using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DUDS.Models;
using DUDS.Service.Interface;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class InvestidorController : ControllerBase
    {
        private readonly IConfiguracaoService _configService;
        private readonly IInvestidorService _investidorService;
        private readonly IInvestidorDistribuidorService _investidorDistribuidorService;

        public InvestidorController(IConfiguracaoService configService, IInvestidorService investidorService, IInvestidorDistribuidorService investidorDistribuidorService)
        {
            _configService = configService;
            _investidorService = investidorService;
            _investidorDistribuidorService = investidorDistribuidorService;
        }

        #region Investidor
        // GET: api/Investidor/GetInvestidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestidorModel>>> GetInvestidor()
        {
            try
            {
                var investidores = await _investidorService.GetAllAsync();

                if (investidores.Any())
                {
                    return Ok(investidores);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Investidor/GetInvestidorById/id
        [HttpGet("{id}")]
        
        public async Task<ActionResult<InvestidorModel>> GetInvestidorById(int id)
        {
            try
            {
                var tblInvestidor = await _investidorService.GetByIdAsync(id);

                if (tblInvestidor == null)
                {
                    return NotFound("Gabriela tercerizou suas metas.");
                }

                return Ok(tblInvestidor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Investidor/GetInvestidorExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<InvestidorModel>> GetInvestidorExistsBase(string cnpj)
        {
            try
            {
                var tblInvestidor = await _investidorService.GetInvestidorExistsBase(cnpj);

                if (tblInvestidor != null)
                {
                    return Ok(tblInvestidor);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Investidor/GetInvestidorByDataCriacao/dataCriacao
        [HttpGet("{dataCriacao}")]
        public async Task<ActionResult<IEnumerable<ErrosPagamentoModel>>> GetInvestidorByDataCriacao(DateTime dataCriacao)
        {
            try
            {
                var investidores = await _investidorService.GetInvestidorByDataCriacao(dataCriacao);

                if (investidores.Any())
                {
                    return Ok(investidores);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: api/Investidor/AddInvestidor/InvestidorModel
        [HttpPost]
        public async Task<ActionResult<InvestidorModel>> AddInvestidor(InvestidorModel investidorModel)
        {
            try
            {
                var retorno = await _investidorService.AddAsync(investidorModel);

                return CreatedAtAction(
                    nameof(GetInvestidorById),
                    new { id = investidorModel.Id }, investidorModel);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Investidor/AddInvestidores/List<InvestidorModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<InvestidorModel>>> AddInvestidores(List<InvestidorModel> investidores)
        {
            try
            {
                var retorno = await _investidorService.AddInvestidores(investidores);
                if (!retorno.Any())
                {
                    return CreatedAtAction(nameof(GetInvestidorByDataCriacao),
                        new { data_criacao = investidores.FirstOrDefault().DataCriacao }, investidores);
                }
                return BadRequest(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        //PUT: api/Investidor/UpdateInvestidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvestidor(int id, InvestidorModel investidorModel)
        {
            try
            {
                InvestidorModel retornoInvestidor = await _investidorService.GetByIdAsync(investidorModel.Id);

                if (retornoInvestidor == null)
                {
                    return NotFound();
                }

                investidorModel.Id = id;
                bool retorno = await _investidorService.UpdateAsync(investidorModel);

                if (retorno)
                {
                    return Ok(investidorModel);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Investidor/DeleteInvestidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestidor(int id)
        {
            try
            {
                var registroInvestidor = await _investidorService.DisableAsync(id);

                if (registroInvestidor)
                {
                    return Ok();
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

        // ATIVAR: api/Investidor/ActivateInvestidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateInvestidor(int id)
        {
            var registroInvestidor = await _investidorService.GetByIdAsync(id);

            if (registroInvestidor != null)
            {
                try
                {
                    await _investidorService.ActivateAsync(id);
                    return Ok(registroInvestidor);
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

        #region Investidor Distribuidor
        // GET: api/Investidor/GetInvestidorDistribuidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestidorDistribuidorModel>>> GetInvestidorDistribuidor()
        {
            try
            {
                var InvestidorDistribuidor = await _investidorDistribuidorService.GetAllAsync();

                if (InvestidorDistribuidor == null)
                {
                    return NotFound();
                }
                return Ok(InvestidorDistribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Investidor/GetInvestidorDistribuidorByDataCriacao/dataCriacao
        [HttpGet("{dataCriacao}")]
        public async Task<ActionResult<IEnumerable<ErrosPagamentoModel>>> GetInvestidorDistribuidorByDataCriacao(DateTime dataCriacao)
        {
            try
            {
                var investidores = await _investidorDistribuidorService.GetInvestidorDistribuidorByDataCriacao(dataCriacao);

                if (investidores.Any())
                {
                    return Ok(investidores);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Investidor/GetInvestidorDistribuidorByIds/codInvestidor/codDistribuidor/codAdministrador
        [HttpGet("{codInvestidor}/{codDistribuidor}/{codAdministrador}")]
        public async Task<ActionResult<InvestidorDistribuidorModel>> GetInvestidorDistribuidorByIds(int codInvestidor, int codDistribuidor, int codAdministrador)
        {
            try
            {
                var InvestidorDistribuidor = await _investidorDistribuidorService.GetByIdsAsync(codInvestidor, codDistribuidor, codAdministrador);

                if (InvestidorDistribuidor == null)
                {
                    return NotFound();
                }
                return Ok(InvestidorDistribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Investidor/AddInvestidorDistribuidor/InvestidorDistribuidorModel
        [HttpPost]
        public async Task<ActionResult<InvestidorDistribuidorModel>> AddInvestidorDistribuidor(InvestidorDistribuidorModel investidorDistribuidorModel)
        {
            try
            {
                bool retorno = await _investidorDistribuidorService.AddAsync(investidorDistribuidorModel);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetInvestidorDistribuidorByIds), new
                    {
                        codInvestidor = investidorDistribuidorModel.CodInvestidor,
                        codAdministrador = investidorDistribuidorModel.CodAdministrador,
                        codDistribuidor = investidorDistribuidorModel.CodDistribuidor
                    }, investidorDistribuidorModel);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //POST: api/Investidor/AddInvestidorDistribuidores/List<InvestidorDistribuidorModel>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<InvestidorDistribuidorModel>>> AddInvestidorDistribuidores(List<InvestidorDistribuidorModel> investidorDistribuidor)
        {
            try
            {
                var retorno = await _investidorDistribuidorService.AddInvestidorDistribuidores(investidorDistribuidor);
                if (!retorno.Any())
                {
                    return CreatedAtAction(nameof(GetInvestidorDistribuidorByDataCriacao), new { data_criacao = investidorDistribuidor.FirstOrDefault().DataCriacao }, investidorDistribuidor);
                }
                return BadRequest(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Investidor/UpdateInvestidorDistribuidor/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvestidorDistribuidor(int id, InvestidorDistribuidorModel investidorDistribuidor)
        {
            try
            {
                InvestidorDistribuidorModel retornoInvestidorDistribuidor = await _investidorDistribuidorService.GetByIdAsync(investidorDistribuidor.Id);

                if (retornoInvestidorDistribuidor == null)
                {
                    return NotFound();
                }

                investidorDistribuidor.Id = id;
                bool retorno = await _investidorDistribuidorService.UpdateAsync(investidorDistribuidor);

                if (retorno)
                {
                    return Ok(investidorDistribuidor);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Investidor/DeleteInvestidorDistribuidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestidorDistribuidor(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_investidor_distribuidor");

            if (!existeRegistro)
            {
                try
                {
                    bool retorno = await _investidorDistribuidorService.DeleteAsync(id);

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
    }
}
