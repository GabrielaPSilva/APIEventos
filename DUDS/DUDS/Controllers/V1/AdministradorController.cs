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
using Newtonsoft.Json;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly IConfiguracaoService _configService;
        private readonly IAdministradorService _administradorService;

        public AdministradorController(IConfiguracaoService configService, IAdministradorService administradorService)
        {
            _configService = configService;
            _administradorService = administradorService;
        }

        // GET: api/Administrador/GetAdministrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministradorModel>>> GetAdministrador()
        {
            try
            {
                var administradores = await _administradorService.GetAdministrador();

                if (administradores.Any())
                {
                    return Ok(administradores);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Administrador/GetAdministradorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<AdministradorModel>> GetAdministradorById(int id)
        {
            try
            {
                var tblAdministrador = await _administradorService.GetAdministradorById(id);

                if (tblAdministrador == null)
                {
                    return NotFound();
                }

                return Ok(tblAdministrador);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //// GET: api/Administrador/GetAdministradorExistsBase/cnpj/nome
        [HttpGet()]
        public async Task<ActionResult<AdministradorModel>> GetAdministradorExistsBase(string cnpj, string nome)
        {
            try
            {
                var tblAdministrador = await _administradorService.GetAdministradorExistsBase(cnpj, nome);

                if (tblAdministrador != null)
                {
                    return Ok(tblAdministrador);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // POST: api/Administrador/AddAdministrador/AdministradorModel
        [HttpPost]
        public async Task<ActionResult<AdministradorModel>> AddAdministrador(AdministradorModel administradorModel)
        {
            try
            {
                var retorno = await _administradorService.AddAdministrador(administradorModel);

                return CreatedAtAction(
                    nameof(GetAdministrador),
                    new { id = administradorModel.Id }, administradorModel);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // PUT: api/Administrador/UpdateAdministrador/id
        [HttpPut()]
        public async Task<IActionResult> UpdateAdministrador(AdministradorModel administradorModel)
        {
            try
            {
                var registroAdministrador = await _administradorService.GetAdministradorById(administradorModel.Id);

                if (registroAdministrador != null)
                {
                    try
                    {
                        await _administradorService.UpdateAdministrador(administradorModel);
                        return Ok(registroAdministrador);
                    }
                    catch (Exception e)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // DESATIVA: api/Administrador/DisableAdministrador/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrador(int id)
        {
            try
            {
                var registroAdministrador = await _administradorService.DisableAdministrador(id);

                if (registroAdministrador)
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
                return BadRequest();
            }
        }

        // ATIVAR: api/Administrador/ActivateAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateAdministrador(int id)
        {
            var registroAdministrador = await _administradorService.GetAdministradorById(id);

            if (registroAdministrador != null)
            {
                try
                {
                    await _administradorService.ActivateAdministrador(id);
                    return Ok(registroAdministrador);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
