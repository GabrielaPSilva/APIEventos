using DUDS.Models.LogErros;
using DUDS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class LoggerController : Controller
    {
        private readonly ILogErrosService _logErrosService;

        public LoggerController(ILogErrosService logErrosService)
        {
            _logErrosService = logErrosService;
        }

        // GET: api/Logger/GetLogErroById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<LogErrosModel>> GetLogErroById(int id)
        {
            try
            {
                var logErro = await _logErrosService.GetLogErroById(id);

                if (logErro == null)
                {
                    return NotFound();
                }
                return Ok(logErro);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/Logger/AddLogErro/LogErrosModel
        [HttpPost]
        public async Task<ActionResult<LogErrosModel>> AddLogErro(LogErrosModel tblLogErros)
        {
            try
            {
                bool retorno = await _logErrosService.AddLogErro(tblLogErros);
                return CreatedAtAction(nameof(GetLogErroById), new { id = tblLogErros.Id }, tblLogErros);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
