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
        private readonly IAdministradorService _administradorService;

        public AdministradorController(IAdministradorService administradorService)
        {
            _administradorService = administradorService;
        }

        #region Administrador
        // GET: api/Administrador/GetAdministrador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministradorModel>>> GetAdministrador()
        {
            try
            {
                var administradores = await _administradorService.GetAllAsync();

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
                var tblAdministrador = await _administradorService.GetByIdAsync(id);

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

        // GET: api/Administrador/GetAdministradorExistsBase/cnpj/nome
        [HttpGet("{cnpj}")]
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
                var retorno = await _administradorService.AddAsync(administradorModel);

                return CreatedAtAction(
                    nameof(GetAdministradorById),
                    new { id = administradorModel.Id }, administradorModel);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // PUT: api/Administrador/UpdateAdministrador/id
        [HttpPut()]
        public async Task<ActionResult<AdministradorModel>> UpdateAdministrador(int id, AdministradorModel administrador)
        {
            try
            {
                var retornoAdministrador = await _administradorService.GetByIdAsync(id);

                if (retornoAdministrador == null)
                {
                    return NotFound();
                }

                administrador.Id = id;
                bool retorno = await _administradorService.UpdateAsync(administrador);

                if (retorno)
                {
                    return Ok(administrador);
                }
                return NotFound();

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
                var registroAdministrador = await _administradorService.DisableAsync(id);

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
            var registroAdministrador = await _administradorService.GetByIdAsync(id);

            if (registroAdministrador != null)
            {
                try
                {
                    await _administradorService.ActivateAsync(id);
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

        #endregion
    }
}
