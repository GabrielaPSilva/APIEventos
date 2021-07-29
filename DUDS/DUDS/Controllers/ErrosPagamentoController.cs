using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;

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

        // GET: api/ErrosPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> ErrosPagamento()
        {
            try
            {
                var errosPagamento = await _context.TblErrosPagamento.OrderBy(c => c.DataAgendamento).AsNoTracking().ToListAsync();

                if (errosPagamento != null)
                {
                    return Ok(errosPagamento);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (InvalidOperationException e)
            {
                return BadRequest();
            }
        }

        // GET: api/ErrosPagamento/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblErrosPagamento>> GetErrosPagamento(int id)
        {
            var tblErrosPagamento = await _context.TblErrosPagamento.FindAsync(id);

            if (tblErrosPagamento == null)
            {
                return NotFound();
            }

            return Ok(tblErrosPagamento);
        }

        [HttpPost]
        public async Task<ActionResult<ErrosPagamentoModel>> CadastrarPagamentoServico(ErrosPagamentoModel errosPagamentoModel)
        {
            var itensErrosPagamento = new TblErrosPagamento
            {
                DataAgendamento = errosPagamentoModel.DataAgendamento,
                CodFundo = errosPagamentoModel.CodFundo,
                TipoDespesa = errosPagamentoModel.TipoDespesa,
                ValorBruto = errosPagamentoModel.ValorBruto,
                CpfCnpjFavorecido = errosPagamentoModel.CpfCnpjFavorecido,
                Favorecido = errosPagamentoModel.Favorecido,
                ContaFavorecida = errosPagamentoModel.ContaFavorecida,
                Competencia = errosPagamentoModel.Competencia,
                Status = errosPagamentoModel.Status,
                CnpjFundoInvestidor = errosPagamentoModel.CnpjFundoInvestidor,
                MensagemErro = errosPagamentoModel.MensagemErro
            };

            _context.TblErrosPagamento.Add(itensErrosPagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetErrosPagamento),
                new { id = itensErrosPagamento.Id },
                Ok(itensErrosPagamento));
        }
    }
}
