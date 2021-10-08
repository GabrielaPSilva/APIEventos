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
    public class CustodianteController : ControllerBase
    {
        private readonly IConfiguracaoService _configService;
        private readonly ICustodianteService _custodianteService;

        public CustodianteController(IConfiguracaoService configService, ICustodianteService custodianteService)
        {
            _configService = configService;
            _custodianteService = custodianteService;
        }

        // GET: api/Custodiante/GetCustodiante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustodianteModel>>> GetCustodiantes()
        {
            try
            {
                var custodiantes = await _custodianteService.GetAllAsync();

                if (custodiantes.Any())
                {
                    return Ok(custodiantes); 
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Custodiante/GetCustodianteById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<CustodianteModel>> GetCustodianteById(int id)
        {
            try
            {
                var custodiante = await _custodianteService.GetByIdAsync(id);
                if (custodiante == null)
                {
                    return NotFound();
                }
                return Ok(custodiante);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Custodiante/GetCustodianteExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<CustodianteModel>> GetCustodianteExistsBase(string cnpj)
        {
            try
            {
                var custodiante = await _custodianteService.GetCustodianteExistsBase(cnpj);
                if (custodiante == null)
                {
                    NotFound();
                }
                return Ok(custodiante);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Custodiante/AddCustodiante/CustodianteModel
        [HttpPost]
        public async Task<ActionResult<CustodianteModel>> AddCustodiante(CustodianteModel custodiante)
        {
            try
            {
                bool retorno = await _custodianteService.AddAsync(custodiante);
                return CreatedAtAction(nameof(GetCustodianteById), new { id = custodiante.Id }, custodiante);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Custodiante/UpdateCustodiante/id
        [HttpPut("{id}")]
        public async Task<ActionResult<CustodianteModel>> UpdateCustodiante(int id, CustodianteModel custodiante)
        {
            try
            {
                CustodianteModel retornoCustodiante = await _custodianteService.GetByIdAsync(custodiante.Id);
                if (retornoCustodiante == null)
                {
                    return NotFound();
                }
                custodiante.Id = id;
                bool retorno = await _custodianteService.UpdateAsync(custodiante);
                if (retorno)
                {
                    return Ok(custodiante);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Custodiante/DeleteCustodiante/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustodiante(int id)
        {
            try
            {
                bool retorno = await _custodianteService.DisableAsync(id);
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

        // ATIVAR: api/Custodiante/ActivateCustodiante/id
        [HttpPut("{id}")]
        public async Task<ActionResult<CustodianteModel>> ActivateCustodiante(int id)
        {
            try
            {
                bool retorno = await _custodianteService.ActivateAsync(id);
                if (retorno)
                {
                    CustodianteModel gestor = await _custodianteService.GetByIdAsync(id);
                    return Ok(gestor);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
