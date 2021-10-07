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
        public async Task<ActionResult<IEnumerable<CustodianteModel>>> GetCustodiante()
        {
            try
            {
                var custodiantes = await _custodianteService.GetCustodiante();

                if (custodiantes.Any())
                {
                    return Ok(custodiantes); 
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Custodiante/GetCustodianteById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<CustodianteModel>> GetCustodianteById(int id)
        {
            try
            {
                var custodiante = await _custodianteService.GetCustodianteById(id);
                if (custodiante == null)
                {
                    return NotFound();
                }
                return Ok(custodiante);
            }
            catch (Exception e)
            {
                return BadRequest();
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
                return BadRequest();
            }
        }

        //POST: api/Custodiante/AddCustodiante/CustodianteModel
        [HttpPost]
        public async Task<ActionResult<CustodianteModel>> AddCustodiante(CustodianteModel custodiante)
        {
            try
            {
                bool retorno = await _custodianteService.AddCustodiante(custodiante);
                return CreatedAtAction(nameof(GetCustodianteById), new { id = custodiante.Id }, custodiante);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //PUT: api/Custodiante/UpdateCustodiante/id
        [HttpPut("{id}")]
        public async Task<ActionResult<CustodianteModel>> UpdateCustodiante(int id, CustodianteModel custodiante)
        {
            try
            {
                CustodianteModel retornoGestor = await _custodianteService.GetCustodianteById(custodiante.Id);
                if (retornoGestor == null)
                {
                    return NotFound();
                }
                bool retorno = await _custodianteService.UpdateCustodiante(custodiante);
                if (retorno)
                {
                    return Ok(custodiante);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Custodiante/DeleteCustodiante/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustodiante(int id)
        {
            try
            {
                bool retorno = await _custodianteService.DisableCustodiante(id);
                if (retorno)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // ATIVAR: api/Custodiante/ActivateCustodiante/id
        [HttpPut("{id}")]
        public async Task<ActionResult<CustodianteModel>> ActivateCustodiante(int id)
        {
            try
            {
                bool retorno = await _custodianteService.ActivateCustodiante(id);
                if (retorno)
                {
                    CustodianteModel gestor = await _custodianteService.GetCustodianteById(id);
                    return Ok(gestor);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
