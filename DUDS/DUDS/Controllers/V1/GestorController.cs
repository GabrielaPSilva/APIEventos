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
    public class GestorController : ControllerBase
    {
        private readonly IGestorService _gestorService;

        public GestorController(IGestorService gestorService)
        {
            _gestorService = gestorService;
        }

        #region Gestor

        // GET: api/Gestor/GetGestor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GestorModel>>> GetGestores()
        {
            try
            {
                var listaGestores = await _gestorService.GetAllAsync();

                if (listaGestores.Any())
                {
                    return Ok(listaGestores);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Gestor/GetGestorById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<GestorModel>> GetGestorById(int id)
        {
            try
            {
                var gestor = await _gestorService.GetByIdAsync(id);

                if (gestor == null)
                {
                    return NotFound();
                }
                return Ok(gestor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Gestor/GetGestorExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<GestorModel>> GetGestorExistsBase(string cnpj)
        {
            try
            {
                var gestor = await _gestorService.GetGestorExistsBase(cnpj);
                if (gestor == null)
                {
                    NotFound();
                }
                return Ok(gestor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Gestor/AddGestor/GestorModel
        [HttpPost]
        public async Task<ActionResult<GestorModel>> AddGestor(GestorModel gestor)
        {

            try
            {
                bool retorno = await _gestorService.AddAsync(gestor);
                return CreatedAtAction(nameof(GetGestorById), new { id = gestor.Id }, gestor);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Gestor/UpdateGestor/id
        [HttpPut("{id}")]
        public async Task<ActionResult<GestorModel>> UpdateGestor(int id, GestorModel gestor)
        {
            try
            {
                GestorModel retornoGestor = await _gestorService.GetByIdAsync(id);
                if (retornoGestor == null)
                {
                    return NotFound();
                }
                gestor.Id = id;
                bool retorno = await _gestorService.UpdateAsync(gestor);
                if (retorno)
                {
                    return Ok(gestor);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Gestor/DeleteGestor/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGestor(int id)
        {
            try
            {
                bool retorno = await _gestorService.Disable(id);
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

        // ATIVAR: api/Gestor/ActivateGestor/id
        [HttpPut("{id}")]
        public async Task<ActionResult<GestorModel>> ActivateGestor(int id)
        {
            try
            {
                bool retorno = await _gestorService.ActivateAsync(id);
                if (retorno)
                {
                    GestorModel gestor = await _gestorService.GetByIdAsync(id);
                    return Ok(gestor);
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
