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

                if (administradores == null)
                {
                    return NotFound();
                }

                return Ok(administradores);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Administrador/GetAdministradorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAdministrador>> GetAdministradorById(int id)
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
                return BadRequest(e.InnerException.Message);
            }
        }

        //// GET: api/Administrador/GetAdministradorExistsBase/cnpj/nome
        //[HttpGet("{cnpj}/{nome}")]
        //public async Task<ActionResult<TblAdministrador>> GetAdministradorExistsBase(string cnpj, string nome)
        //{
        //    TblAdministrador tblAdministrador = new TblAdministrador();

        //    try
        //    {
        //        tblAdministrador = await _context.TblAdministrador.Where(c => c.Ativo == false && (c.Cnpj == cnpj || c.NomeAdministrador == nome)).FirstOrDefaultAsync();

        //        if (tblAdministrador != null)
        //        {
        //            return Ok(tblAdministrador);
        //        }

        //        tblAdministrador = await _context.TblAdministrador.Where(c => (c.Cnpj == cnpj || c.NomeAdministrador == nome)).FirstOrDefaultAsync();

        //        if (tblAdministrador != null)
        //        {
        //            return Ok(tblAdministrador);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.InnerException.Message);
        //    }
        //}

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
                return BadRequest(e.InnerException.Message);
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
                        return BadRequest(e.InnerException.Message);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DESATIVA: api/Administrador/DisableAdministrador/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableAdministrador(int id)
        {
            var registroAdministrador = await _administradorService.GetAdministradorById(id);

            if (registroAdministrador != null)
            {
                try
                {
                    await _administradorService.DisableAdministrador(id);
                    return Ok(registroAdministrador);
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
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
