using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DUDS.Service.Interface;
using DUDS.Models.Administrador;
using DUDS.Service;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly IEventosService _eventosService;

        public EventosController(IEventosService eventosService)
        {
            _eventosService = eventosService;
        }

        // GET: api/Eventos/GetEventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventosModel>>> GetEventos()
        {
            try
            {
                var eventos = await _eventosService.GetAllAsync();

                if (eventos.Any())
                {
                    return Ok(eventos);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Eventos/GetEventoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<EventosModel>> GetEventoById(int id)
        {
            try
            {
                var evento = await _eventosService.GetByIdAsync(id);

                if (evento == null)
                {
                    return NotFound();
                }

                return Ok(evento);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        // GET: api/Eventos/GetEventosByPais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventosModel>>> GetEventosByPais(int idPais)
        {
            try
            {
                var eventos = await _eventosService.GetByIdPaisAsync(idPais);

                if (eventos.Any())
                {
                    return Ok(eventos);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: api/Eventos/AddEventos/EventosModel
        [HttpPost]
        public async Task<ActionResult<EventosModel>> AddEventos(EventosModel eventosModel)
        {
            try
            {
                var retorno = await _eventosService.AddAsync(eventosModel);

                return CreatedAtAction(
                    nameof(GetEventoById),
                    new { id = eventosModel.Id }, eventosModel);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/Eventos/UpdateEventos/id
        [HttpPut()]
        public async Task<ActionResult<EventosModel>> UpdateEventos(int id, EventosModel eventosModel)
        {
            try
            {
                var retornaEvento = await _eventosService.GetByIdAsync(id);

                if (retornaEvento == null)
                {
                    return NotFound();
                }

                eventosModel.Id = id;
                bool retorno = await _eventosService.UpdateAsync(eventosModel);

                if (retorno)
                {
                    return Ok(eventosModel);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Eventos/DeleteEventos/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventos(int id)
        {
            try
            {
                var eventos = await _eventosService.DeleteAsync(id);

                if (eventos)
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
    }
}
