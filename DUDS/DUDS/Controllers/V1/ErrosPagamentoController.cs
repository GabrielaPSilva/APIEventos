using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using EFCore.BulkExtensions;
using Microsoft.OData.Edm;
using DUDS.Service.Interface;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class ErrosPagamentoController : ControllerBase
    {
        private readonly IErrosPagamentoService _errosPagamento;
        private readonly IConfiguracaoService _configService;

        public ErrosPagamentoController(IConfiguracaoService configService, IErrosPagamentoService errosPagamento)
        {
            _errosPagamento = errosPagamento;
            _configService = configService;
        }

        // GET: api/ErrosPagamento/ErrosPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrosPagamentoModel>>> GetErrosPagamento()
        {
            try
            {
                var errosPagamentos = await _errosPagamento.GetErrosPagamento();
                if (errosPagamentos.Any())
                {
                    return Ok(errosPagamentos);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/ErrosPagamento/ErrosPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrosPagamentoModel>>> GetErrosPagamentoByCompetenciaDataAgendamento([FromQuery] string competencia,[FromQuery] DateTime? data_agendamento)
        {
            try
            {
                var errosPagamentos = await _errosPagamento.GetErrosPagamentoByCompetenciaDataAgendamento(competencia,data_agendamento);
                if (errosPagamentos.Any())
                {
                    return Ok(errosPagamentos);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        // GET: api/ErrosPagamento/GetErrosPagamento/cod_fundo/data_agendamento
        [HttpGet("{id}")]
        public async Task<ActionResult<TblErrosPagamento>> GetErrosPagamentoById(int id)
        {
            try
            {
                var errosPagamento = await _errosPagamento.GetErrosPagamentoById(id);
                if (errosPagamento == null)
                {
                    return NotFound();
                }
                return Ok(errosPagamento);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        

        //POST: api/ErrosPagamento/CadastrarPagamentoServico/List<ErrosPagamentoModel>
        [HttpPost]
        public async Task<ActionResult<ErrosPagamentoModel>> AddErrosPagamento(List<ErrosPagamentoModel> errosPagamento)
        {
            try
            {
                bool retorno = await _errosPagamento.AddErrosPagamento(errosPagamento);
                return CreatedAtAction(nameof(GetErrosPagamento), errosPagamento);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        // DELETE: api/ErrosPagamento/DeletarErrosPagamento/data_agendamento
        [HttpDelete("{data_agendamento}")]
        public async Task<IActionResult> DeletarErrosPagamento(DateTime data_agendamento)
        {
            try
            {
                bool retorno = await _errosPagamento.DeleteErrosPagamentoByDataAgendamento(data_agendamento);
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

        // DELETE: api/ErrosPagamento/DeletarErrosPagamento/id
        [HttpDelete("{data_agendamento}")]
        public async Task<IActionResult> DeletarErrosPagamentoById(int id)
        {
            try
            {
                bool retorno = await _errosPagamento.DeleteErrosPagamentoById(id);
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
