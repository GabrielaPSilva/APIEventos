using DUDS.Controllers.V1;
using DUDS.Models.Administrador;
using DUDS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APIEventosTest
{
    public class EventosControllerTest
    {
        private readonly EventosController _eventosController;
        private readonly IEventosService _eventosService;

        public EventosControllerTest(IEventosService eventosService)
        {
            _eventosService = eventosService;
            _eventosController = new EventosController(_eventosService);
        }

        [Fact]
        public async Task<ActionResult<IEnumerable<EventosModel>>> GetEventos()
        {
            // Act
            var okResult = _eventosController.GetEventos();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);

            return okResult.Result;
        }

        //[Theory]
        //public async Task<ActionResult<EventosModel>> GetEventoById(int id)
        //{
        //    await GetEventoById(new EventosModel() { Id = 1 });
        //    await InsertVinstaInLocoCursoAndGetItsID(new VistaInLocoCursoMOD() { CodigoCurso = 0 });

        //    var result = await _VistaInLocoCursoDAL.ListarVistaInLocoAsync(0, 0, 10);

        //    Assert.That(result.Count, Is.EqualTo(2));
        //}

        //[Theory]
        //public async Task<ActionResult<IEnumerable<EventosModel>>> GetEventosByPais(int idPais)
        //{
        //    try
        //    {
        //        var eventos = await _eventosService.GetByIdPaisAsync(idPais);

        //        if (eventos.Any())
        //        {
        //            return Ok(eventos);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

        //[Theory]
        //public async Task<ActionResult<EventosModel>> AddEventos(EventosModel eventosModel)
        //{
        //    try
        //    {
        //        var retorno = await _eventosService.AddAsync(eventosModel);

        //        return CreatedAtAction(
        //            nameof(GetEventoById),
        //            new { id = eventosModel.Id }, eventosModel);

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

        //[Theory]
        //public async Task<ActionResult<EventosModel>> UpdateEventos(int id, EventosModel eventosModel)
        //{
        //    try
        //    {
        //        var retornaEvento = await _eventosService.GetByIdAsync(id);

        //        if (retornaEvento == null)
        //        {
        //            return NotFound();
        //        }

        //        eventosModel.Id = id;
        //        bool retorno = await _eventosService.UpdateAsync(eventosModel);

        //        if (retorno)
        //        {
        //            return Ok(eventosModel);
        //        }

        //        return NotFound();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

        //[Theory]
        //public async Task<IActionResult> DeleteEventos(int id)
        //{
        //    try
        //    {
        //        var eventos = await _eventosService.DeleteAsync(id);

        //        if (eventos)
        //        {
        //            return Ok();
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}
    }
}
