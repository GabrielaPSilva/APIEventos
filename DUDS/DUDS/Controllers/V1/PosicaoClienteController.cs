using DUDS.Models;
using DUDS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class PosicaoClienteController : ControllerBase
    {
        private readonly IPosicaoClientePassivoService _posicaoClientePassivoService;

        public PosicaoClienteController(IPosicaoClientePassivoService posicaoClientePassivoService)
        {
            _posicaoClientePassivoService = posicaoClientePassivoService;
        }

        // GET: api/PosicaoCliente/GetControleRebate
        /*
        [HttpGet("{data_ref}")]
        public async Task<ActionResult<IEnumerable<PosicaoClientePassivoModel>>> GetAllByDate(DateTime dataRef)
        {
            try
            {
                var listaControleRebate = await _posicaoClientePassivoService.GetByDataRefAsync(dataRef);

                if (listaControleRebate.Any())
                {
                    return Ok(listaControleRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        */

        // GET: api/PosicaoCliente/GetPosicaoClientePassivo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosicaoClientePassivoModel>>> GetPosicaoClientePassivo([FromQuery] DateTime? dataInicio, [FromQuery] DateTime? dataFim, [FromQuery] int? codDistribuidor, [FromQuery] int? codGestor, [FromQuery] int? codInvestidorDistribuidor)
        {
            try
            {
                var listaControleRebate = await _posicaoClientePassivoService.GetByParametersAsync(dataInicio, dataFim, codDistribuidor, codGestor, codInvestidorDistribuidor);

                if (listaControleRebate.Any())
                {
                    return Ok(listaControleRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/PosicaoCliente/GetPosicaoClientePassivo
        [HttpGet]
        public async Task<ActionResult<PosicaoClientePassivoModel>> GetMaiorValorBruto([FromQuery] int? codDistribuidor, [FromQuery] int? codGestor, [FromQuery] int? codInvestidorDistribuidor)
        {
            try
            {
                var listaControleRebate = await _posicaoClientePassivoService.GetMaxValorBrutoAsync(codDistribuidor, codGestor, codInvestidorDistribuidor);

                if (listaControleRebate != null)
                {
                    return Ok(listaControleRebate);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<PosicaoClientePassivoModel>>> AddPosicaoClientePassivo(List<PosicaoClientePassivoModel> posicaoClientePassivo)
        {
            try
            {
                bool retorno = await _posicaoClientePassivoService.AddBulkAsync(posicaoClientePassivo);
                if (retorno)
                {
                    return CreatedAtAction(nameof(GetPosicaoClientePassivo),
                        new { dataInicio = posicaoClientePassivo.FirstOrDefault().DataRef }, posicaoClientePassivo);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{data_ref}")]
        public async Task<IActionResult> DeletePosicaoClientePassivo(DateTime dataRef)
        {
            try
            {
                bool retorno = await _posicaoClientePassivoService.DeleteByDataRefAsync(dataRef);
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
    }
}
