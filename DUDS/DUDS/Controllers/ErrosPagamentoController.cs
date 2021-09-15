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

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class ErrosPagamentoController : ControllerBase
    {
        private readonly DataContext _context;

        public ErrosPagamentoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ErrosPagamento/ErrosPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> ErrosPagamento()
        {
            try
            {
                List<TblErrosPagamento> errosPagamento = await _context.TblErrosPagamento.OrderBy(c => c.DataAgendamento).AsNoTracking().ToListAsync();

                if (errosPagamento.Count == 0)
                {
                    return NotFound();
                }

                return Ok(errosPagamento);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/ErrosPagamento/GetErrosPagamento/cod_fundo/data_agendamento
        [HttpGet("{id}")]
        public async Task<ActionResult<TblErrosPagamento>> GetErrosPagamento(int id)
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

        //POST: api/ErrosPagamento/CadastrarPagamentoServico/List<ErrosPagamentoModel>
        [HttpPost]
        public async Task<ActionResult<ErrosPagamentoModel>> CadastrarPagamentoServico(List<ErrosPagamentoModel> errosPagamentoModel)
        {
            List<TblErrosPagamento> listaErros = new List<TblErrosPagamento>();
            TblErrosPagamento itensErrosPagamento = new TblErrosPagamento();

            try
            {
                foreach (var line in errosPagamentoModel)
                {
                    itensErrosPagamento = new TblErrosPagamento
                    {
                        DataAgendamento = line.DataAgendamento,
                        CodFundo = line.CodFundo,
                        TipoDespesa = line.TipoDespesa,
                        ValorBruto = line.ValorBruto,
                        CpfCnpjFavorecido = line.CpfCnpjFavorecido,
                        Favorecido = line.Favorecido,
                        ContaFavorecida = line.ContaFavorecida,
                        Competencia = line.Competencia,
                        Status = line.Status,
                        CnpjFundoInvestidor = line.CnpjFundoInvestidor,
                        MensagemErro = line.MensagemErro
                    };

                    listaErros.Add(itensErrosPagamento);
                }

                await _context.BulkInsertAsync(listaErros);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                 nameof(GetErrosPagamento), listaErros);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

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
    }
}
