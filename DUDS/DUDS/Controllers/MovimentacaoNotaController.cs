using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using Microsoft.Data.SqlClient;
using EFCore.BulkExtensions;

namespace DUDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoNotaController : ControllerBase
    {
        private readonly DataContext _context;

        public MovimentacaoNotaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/MovimentacaoNota/MovimentacaoNota
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblMovimentacaoNota>>> MovimentacaoNota()
        {
            try
            {
                List<TblMovimentacaoNota> movimentacaoNotas = await _context.TblMovimentacaoNota.OrderByDescending(c => c.DataMovimentacao).AsNoTracking().ToListAsync();

                if (movimentacaoNotas.Count() == 0)
                {
                    return NotFound();
                }

                if (movimentacaoNotas != null)
                {
                    return Ok(movimentacaoNotas);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e);
            }
        }

        // GET: api/MovimentacaoNota/GetMovimentacaoNota/cod_movimentacao/nota_aplicacao/num_ordem/cod_ordem_mae
        [HttpGet("{cod_movimentacao}/{nota_aplicacao}/{num_ordem}/{cod_ordem_mae}")]
        public async Task<ActionResult<TblMovimentacaoNota>> GetMovimentacaoNota(int cod_movimentacao, int nota_aplicacao, int num_ordem, int cod_ordem_mae)
        {
            TblMovimentacaoNota tblMovimentacaoNota = await _context.TblMovimentacaoNota.FindAsync(cod_movimentacao, nota_aplicacao, num_ordem, cod_ordem_mae);

            try
            {
                if (tblMovimentacaoNota != null)
                {
                    return Ok(tblMovimentacaoNota);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/MovimentacaoNota/CadastrarMovimentacaoNota/FundoModel
        [HttpPost]
        public async Task<ActionResult<List<MovimentacaoNotaModel>>> CadastrarMovimentacaoNota(List<MovimentacaoNotaModel> tblListMovimentacaoNotaModel)
        {
            try
            {
                List<TblMovimentacaoNota> listaMovimentacoes = new List<TblMovimentacaoNota>();
                TblMovimentacaoNota itensMovimentacao = new TblMovimentacaoNota();

                foreach (var line in tblListMovimentacaoNotaModel)
                {
                    itensMovimentacao = new TblMovimentacaoNota
                    {
                        CodFundo = line.CodFundo,
                        DataMovimentacao = line.DataMovimentacao,
                        DataCotizacao = line.DataCotizacao,
                        CodInvestidor = line.CodInvestidor,
                        CodMovimentacao = line.CodMovimentacao,
                        TipoMovimentacao = line.TipoMovimentacao,
                        QtdeCotas = line.QtdeCotas,
                        ValorCota = line.ValorCota,
                        ValorBruto = line.ValorBruto,
                        Irrf = line.Irrf,
                        Iof = line.Iof,
                        ValorLiquido = line.ValorLiquido,
                        NotaAplicacao = line.NotaAplicacao,
                        RendimentoBruto = line.RendimentoBruto,
                        ValorPerformance = line.ValorPerformance,
                        NumOrdem = line.NumOrdem,
                        TipoTransferencia = line.TipoTransferencia,
                        CodDistribuidor = line.CodDistribuidor,
                        Operador = line.Operador,
                        CodGestor = line.CodGestor,
                        CodOrdemMae = line.CodOrdemMae,
                        Penalty = line.Penalty,
                        CodAdm = line.CodAdm,
                        ClassTributaria = line.ClassTributaria,
                        CodCustodiante = line.CodCustodiante
                    };

                    listaMovimentacoes.Add(itensMovimentacao);
                }

                await _context.BulkInsertAsync(listaMovimentacoes);
                await _context.SaveChangesAsync();

                return Ok(itensMovimentacao);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/MovimentacaoNota/DeletarMovimentacaoNota/data_movimentacao
        [HttpDelete("{data_movimentacao}")]
        public async Task<ActionResult<IEnumerable<TblMovimentacaoNota>>> DeletarMovimentacaoNota(DateTime data_movimentacao)
        {
            IList<TblMovimentacaoNota> tblMovimentacaoNota = await _context.TblMovimentacaoNota.Where(c => c.DataMovimentacao == data_movimentacao).ToListAsync();

            if (tblMovimentacaoNota == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblMovimentacaoNota.RemoveRange(tblMovimentacaoNota);
                await _context.SaveChangesAsync();
                return Ok(tblMovimentacaoNota);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}