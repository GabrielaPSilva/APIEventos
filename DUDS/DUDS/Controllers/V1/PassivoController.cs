using DUDS.Models.Passivo;
using DUDS.Service.Interface;
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
            _ordemPassivoService = ordemPassivoService;
            _movimentacaoPassivoService = movimentacaoPassivoService;
        }

        #region Posicao Passivo
        // GET: api/PosicaoCliente/GetControleRebate

        [HttpGet("{dataRef}")]
        public async Task<ActionResult<IEnumerable<PosicaoClienteViewModel>>> GetAllPosicaoClienteByDate(DateTime dataRef)
        {
            try
            {
                var listaControleRebate = await _posicaoClientePassivoService.GetByParametersAsync(dataInicio: dataRef, dataFim: dataRef, codDistribuidor: null, codGestor: null, codInvestidorDistribuidor: null);

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

        [HttpGet("{dataRef}")]
        public async Task<ActionResult<int>> GetCountPosicaoClienteByDataRef(DateTime dataRef)
        {
            try
            {
                var listaControleRebate = await _posicaoClientePassivoService.GetCountByDataRefAsync(dataRef);

                if (listaControleRebate != 0)
                {
                    return Ok(listaControleRebate);
                }
                return NotFound("Data de " + dataRef + " não possui posição.");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        // GET: api/PosicaoCliente/GetPosicaoClientePassivo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PosicaoClienteViewModel>>> GetPosicaoCliente([FromQuery] DateTime? dataInicio, [FromQuery] DateTime? dataFim, [FromQuery] int? codDistribuidor, [FromQuery] int? codGestor, [FromQuery] int? codInvestidorDistribuidor)
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
        [HttpGet("{dataRef}")]
        public async Task<ActionResult<double>> GetMaiorValorBrutoPosicaoCliente(DateTime dataRef, [FromQuery] int? codDistribuidor, [FromQuery] int? codGestor, [FromQuery] int? codInvestidorDistribuidor, [FromQuery] int? codFundo)
        {
            try
            {
                var maxValorBruto = await _posicaoClientePassivoService.GetMaxValorBrutoAsync(dataRef, codDistribuidor, codGestor, codInvestidorDistribuidor, codFundo);
                return Ok(maxValorBruto);
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
                if (retorno.Any())
                {
                    return CreatedAtAction(nameof(GetPosicaoCliente),
                        new { dataInicio = posicaoClientePassivo.FirstOrDefault().DataRef }, posicaoClientePassivo);
                }
                return BadRequest(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{dataRef}")]
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
        public async Task<ActionResult<IEnumerable<OrdemPassivoViewModel>>> GetOrdemPassivo([FromQuery] DateTime? dataEntrada)
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
                if (retorno.Any())
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

        [HttpDelete("{dataEntrada}")]
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
        public async Task<ActionResult<IEnumerable<MovimentacaoPassivoViewModel>>> GetMovimentacaoPassivo([FromQuery] DateTime? dataMovimentacao)
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
                if (retorno.Any())
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

        [HttpDelete("{dataMovimentacao}")]
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
