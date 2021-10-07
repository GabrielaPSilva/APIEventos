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

        public ErrosPagamentoController(IErrosPagamentoService errosPagamento)
        {
            _errosPagamento = errosPagamento;
        }

        // GET: api/ErrosPagamento/ErrosPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrosPagamentoModel>>> GetErrosPagamento()
        {
            try
            {
                var errosPagamentoa = await _errosPagamento.GetErrosPagamento();

                if (errosPagamentoa.Any())
                {
                    return Ok(errosPagamentoa);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /*
        // GET: api/ErrosPagamento/GetErrosPagamento/cod_fundo/data_agendamento
        [HttpGet("{id}")]
        public async Task<ActionResult<TblErrosPagamento>> GetErrosPagamentoById(int id)
        {
            try
            {
                TblErrosPagamento tblErrosPagamento = await _context.TblErrosPagamento.FindAsync(id);
                if (tblErrosPagamento == null)
                {
                    return NotFound();
                }
                return Ok(tblErrosPagamento);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        */

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
                return BadRequest();
            }
        }
        /*
        // DELETE: api/ErrosPagamento/DeletarErrosPagamento/data_agendamento
        [HttpDelete("{data_agendamento}")]
        public async Task<ActionResult<IEnumerable<TblErrosPagamento>>> DeletarErrosPagamento(DateTime data_agendamento)
        {
            IList<TblErrosPagamento> tblErrosPagamento = await _context.TblErrosPagamento.Where(c => c.DataAgendamento == data_agendamento).ToListAsync();

            if (tblErrosPagamento == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblErrosPagamento.RemoveRange(tblErrosPagamento);
                await _context.SaveChangesAsync();
                return Ok(tblErrosPagamento);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        */
    }
}
