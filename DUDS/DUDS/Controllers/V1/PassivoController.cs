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
    public class PassivoController : ControllerBase
    {
        private readonly IPosicaoClienteService _posicaoClientePassivoService;
        private readonly IOrdemPassivoService _ordemPassivoService;
        private readonly IMovimentacaoPassivoService _movimentacaoPassivoService;

        public PassivoController(IPosicaoClienteService posicaoClientePassivoService, IOrdemPassivoService ordemPassivoService, IMovimentacaoPassivoService movimentacaoPassivoService)
        {
            _posicaoClientePassivoService = posicaoClientePassivoService;
            _ordemPassivoService= ordemPassivoService;
            _movimentacaoPassivoService = movimentacaoPassivoService;
        }

        #region Posicao Passivo
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
        public async Task<ActionResult<IEnumerable<PosicaoClienteModel>>> GetPosicaoClientePassivo([FromQuery] DateTime? dataInicio, [FromQuery] DateTime? dataFim, [FromQuery] int? codDistribuidor, [FromQuery] int? codGestor, [FromQuery] int? codInvestidorDistribuidor)
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
        public async Task<ActionResult<PosicaoClienteModel>> GetMaiorValorBruto([FromQuery] int? codDistribuidor, [FromQuery] int? codGestor, [FromQuery] int? codInvestidorDistribuidor)
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
        public async Task<ActionResult<IEnumerable<PosicaoClienteModel>>> AddPosicaoClientePassivo(List<PosicaoClienteModel> posicaoClientePassivo)
        {
            try
            {
                var retorno = await _posicaoClientePassivoService.AddBulkAsync(posicaoClientePassivo);
                if (!retorno.Any())
                {
                    return CreatedAtAction(nameof(GetPosicaoClientePassivo),
                        new { dataInicio = posicaoClientePassivo.FirstOrDefault().DataRef }, posicaoClientePassivo);
                }
                return BadRequest(retorno);
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
        #endregion

        #region Ordem Passivo
        // GET: api/PosicaoCliente/GetPosicaoClientePassivo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemPassivoModel>>> GetOrdemPassivo([FromQuery] DateTime? dataEntrada)
        {
            try
            {
                var listaControleRebate = await _ordemPassivoService.GetByDataEntradaAsync(dataEntrada);

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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<OrdemPassivoModel>>> AddOrdemPassivo(List<OrdemPassivoModel> ordemPassivo)
        {
            try
            {
                var retorno = await _ordemPassivoService.AddBulkAsync(ordemPassivo);
                if (!retorno.Any())
                {
                    return CreatedAtAction(nameof(GetOrdemPassivo),
                        new { dataEntrada = DateTime.Today }, ordemPassivo);
                }
                return BadRequest(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{data_entrada}")]
        public async Task<IActionResult> DeleteOrdemPassivo(DateTime dataEntrada)
        {
            try
            {
                bool retorno = await _ordemPassivoService.DeleteByDataRefAsync(dataEntrada);
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
        #endregion

        #region Movimentacao Passivo
        // GET: api/PosicaoCliente/GetPosicaoClientePassivo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimentacaoPassivoModel>>> GetMovimentacaoPassivo([FromQuery] DateTime? dataMovimentacao)
        {
            try
            {
                var listaControleRebate = await _movimentacaoPassivoService.GetByDataEntradaAsync(dataMovimentacao);

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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<MovimentacaoPassivoModel>>> AddMovimentacaoPassivo(List<MovimentacaoPassivoModel> movimentacaoPassico)
        {
            try
            {
                var retorno = await _movimentacaoPassivoService.AddBulkAsync(movimentacaoPassico);
                if (!retorno.Any())
                {
                    return CreatedAtAction(nameof(GetMovimentacaoPassivo),
                        new { dataMovimentacao = movimentacaoPassico.FirstOrDefault().DataMovimentacao }, movimentacaoPassico);
                }
                return BadRequest(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{data_movimentacao}")]
        public async Task<IActionResult> DeleteMovimentacaoPassivo(DateTime dataMovimentacao)
        {
            try
            {
                bool retorno = await _movimentacaoPassivoService.DeleteByDataRefAsync(dataMovimentacao);
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
        #endregion
    }
}
