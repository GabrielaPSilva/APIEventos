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
    public class DistribuidorController : ControllerBase
    {
        private readonly IDistribuidorService _distribuidorService;
        private readonly IDistribuidorAdministradorService _distribuidorAdministradorService;
        private readonly IConfiguracaoService _configService;

        public DistribuidorController(IDistribuidorService distribuidorService, IDistribuidorAdministradorService distribuidorAdministradorService, IConfiguracaoService configService)
        {
            _distribuidorService = distribuidorService;
            _configService = configService;
            _distribuidorAdministradorService = distribuidorAdministradorService;
        }

        #region Distribuidor
        // GET: api/Distribuidor/GetDistribuidor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistribuidorModel>>> GetDistribuidor()
        {
            try
            {
                var distribuidores = await _distribuidorService.GetAllAsync();

                if (distribuidores.Any())
                {
                    return Ok(distribuidores);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Distribuidor/GetDistribuidorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<DistribuidorModel>> GetDistribuidorById(int id)
        {
            try
            {
                var distribuidor = await _distribuidorService.GetByIdAsync(id);
                if (distribuidor == null)
                {
                    return NotFound();
                }
                return Ok(distribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Distribuidor/GetDistribuidorExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<DistribuidorModel>> GetDistribuidorExistsBase(string cnpj)
        {
            try
            {
                var distribuidor = await _distribuidorService.GetDistribuidorExistsBase(cnpj);
                if (distribuidor == null)
                {
                    NotFound();
                }
                return Ok(distribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Distribuidor/AddDistribuidor/DistribuidorModel
        [HttpPost]
        public async Task<ActionResult<DistribuidorModel>> AddDistribuidor(DistribuidorModel distribuidor)
        {
            try
            {
                bool retorno = await _distribuidorService.AddAsync(distribuidor);
                return CreatedAtAction(nameof(GetDistribuidorById), new { id = distribuidor.Id }, distribuidor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Distribuidor/UpdateDistribuidor/id
        [HttpPut("{id}")]
        public async Task<ActionResult<DistribuidorModel>> UpdateDistribuidor(int id, DistribuidorModel distribuidor)
        {
            try
            {
                DistribuidorModel retornoDistribuidor = await _distribuidorService.GetByIdAsync(distribuidor.Id);
                if (retornoDistribuidor == null)
                {
                    return NotFound();
                }
                distribuidor.Id = id;
                bool retorno = await _distribuidorService.UpdateAsync(distribuidor);
                if (retorno)
                {
                    return Ok(distribuidor);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Distribuidor/DeleteDistribuidor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistribuidor(int id)
        {
            try
            {
                bool retorno = await _distribuidorService.DisableAsync(id);
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

        // ATIVAR: api/Distribuidor/ActivateDistribuidor/id
        [HttpPut("{id}")]
        public async Task<ActionResult<DistribuidorModel>> ActivateDistribuidor(int id)
        {
            try
            {
                bool retorno = await _distribuidorService.ActivateAsync(id);
                if (retorno)
                {
                    DistribuidorModel distribuidor = await _distribuidorService.GetByIdAsync(id);
                    return Ok(distribuidor);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion

        #region Distribuidor Administrador
        // GET: api/Distribuidor/GetDistribuidorAdministrador
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistribuidorAdministradorModel>>> GetDistribuidorAdministrador()
        {
            try
            {
                var distribuidorAdministrador = await _distribuidorAdministradorService.GetAllAsync();

                if (distribuidorAdministrador.Any())
                {
                    return Ok(distribuidorAdministrador);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        

        // GET: api/Distribuidor/GetDistribuidorAdministradorByIds/id
        [HttpGet("{id}")]
        public async Task<ActionResult<DistribuidorAdministradorModel>> GetDistribuidorAdministradorById(int id)
        {
            try
            {
                var distribuidorAdministrador = await _distribuidorAdministradorService.GetByIdAsync(id);
                if (distribuidorAdministrador == null)
                {
                    return NotFound();
                }
                return Ok(distribuidorAdministrador);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Distribuidor/AddDistribuidorAdministrador/DistribuidorAdministradorModel
        [HttpPost]
        public async Task<ActionResult<DistribuidorAdministradorModel>> AddDistribuidorAdministrador(DistribuidorAdministradorModel distribuidorAdministrador)
        {
            try
            {
                bool retorno = await _distribuidorAdministradorService.AddAsync(distribuidorAdministrador);
                return CreatedAtAction(nameof(GetDistribuidorAdministradorById), new { id = distribuidorAdministrador.Id }, distribuidorAdministrador);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Distribuidor/UpdateDistribuidorAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistribuidorAdministrador(int id, DistribuidorAdministradorModel distribuidorAdministrador)
        {
            try
            {
                DistribuidorAdministradorModel retornoDistribuidorAdministrador = await _distribuidorAdministradorService.GetByIdAsync(distribuidorAdministrador.Id);
                if (retornoDistribuidorAdministrador == null)
                {
                    return NotFound();
                }
                distribuidorAdministrador.Id = id;
                bool retorno = await _distribuidorAdministradorService.UpdateAsync(distribuidorAdministrador);
                if (retorno)
                {
                    return Ok(distribuidorAdministrador);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Distribuidor/DeleteDistribuidorAdministrador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistribuidorAdministrador(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_distribuidor_administrador");

            if (!existeRegistro)
            {
                try
                {
                    bool retorno = await _distribuidorAdministradorService.DeleteAsync(id);
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
