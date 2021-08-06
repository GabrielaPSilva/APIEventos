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

        // GET: api/ErrosPagamento/ErrosPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> ErrosPagamento()
        {
            try
            {
                List<TblErrosPagamento> errosPagamento = await _context.TblErrosPagamento.OrderBy(c => c.DataAgendamento).AsNoTracking().ToListAsync();

                if (errosPagamento != null)
                {
                    return Ok(new { errosPagamento, Mensagem.SucessoListar });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (InvalidOperationException e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        // GET: api/ErrosPagamento/GetErrosPagamento/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblErrosPagamento>> GetErrosPagamento(int id)
        {
            TblErrosPagamento tblErrosPagamento = await _context.TblErrosPagamento.FindAsync(id);

            try
            {
                if (tblErrosPagamento != null)
                {
                    return Ok(new { tblErrosPagamento, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return NotFound(new { Erro = true, Mensagem.ErroTipoInvalido });
                }
            }
            catch (Exception e)
            {
                //await new Logger.Logger().SalvarAsync(Mensagem.LogDesativarRelatorio, e, Sistema);
                return BadRequest(new { Erro = true, Mensagem.ErroPadrao });
            }
        }

        //POST: api/ErrosPagamento/CadastrarPagamentoServico/ErrosPagamentoModel
        [HttpPost]
        public async Task<ActionResult<ErrosPagamentoModel>> CadastrarPagamentoServico(ErrosPagamentoModel errosPagamentoModel)
        {
            TblErrosPagamento itensErrosPagamento = new TblErrosPagamento
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

            try
            {
                _context.TblErrosPagamento.Add(itensErrosPagamento);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetErrosPagamento),
                    new { id = itensErrosPagamento.Id },
                    Ok(new { itensErrosPagamento, Mensagem.SucessoCadastrado }));
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = true, Mensagem.ErroCadastrar });
            }
        }
    }
}
