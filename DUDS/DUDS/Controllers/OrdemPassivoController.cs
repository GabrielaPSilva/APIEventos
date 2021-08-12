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

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class OrdemPassivoController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdemPassivoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/OrdemPassivo/OrdemPassivo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblOrdemPassivo>>> OrdemPassivo()
        {
            try
            {
                List<TblOrdemPassivo> ordemPassivos = await _context.TblOrdemPassivo.OrderByDescending(c => c.DtEntrada).AsNoTracking().ToListAsync();

                if (ordemPassivos.Count() == 0)
                {
                    return NotFound();
                }

                if (ordemPassivos != null)
                {
                    return Ok(ordemPassivos);
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

        // GET: api/OrdemPassivo/GetOrdemPassivo/cod_investidor/num_ordem
        [HttpGet("{cod_investidor}/{num_ordem}")]
        public async Task<ActionResult<TblOrdemPassivo>> GetOrdemPassivo(int cod_investidor, string num_ordem)
        {
            TblOrdemPassivo tblOrdemPassivo = await _context.TblOrdemPassivo.FindAsync(cod_investidor, num_ordem);

            try
            {
                if (tblOrdemPassivo != null)
                {
                    return Ok(tblOrdemPassivo);
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

        //POST: api/OrdemPassivo/CadastrarOrdemPassivo/List<OrdemPassivoModel
        [HttpPost]
        public async Task<ActionResult<OrdemPassivoModel>> CadastrarOrdemPassivo(List<OrdemPassivoModel> tblOrdemPassivoModel)
        {
            List<TblOrdemPassivo> listaOrdemPassivo = new List<TblOrdemPassivo>();
            TblOrdemPassivo itensOrdemPassivo = new TblOrdemPassivo();

            try
            {
                foreach (var line in tblOrdemPassivoModel)
                {
                    itensOrdemPassivo = new TblOrdemPassivo
                    {
                        NumOrdem = line.NumOrdem,
                        CodInvestidor = line.CodInvestidor,
                        CodFundo = line.CodFundo,
                        DsOperacao = line.DsOperacao,
                        VlValor = line.VlValor,
                        IdNota = line.IdNota,
                        DtEnvio = line.DtEnvio,
                        DtEntrada = line.DtEntrada,
                        DtProcessamento = line.DtProcessamento,
                        DtCompensacao = line.DtCompensacao,
                        DtAgendamento = line.DtAgendamento,
                        DtCotizacao = line.DtCotizacao,
                        CodDistribuidor = line.CodDistribuidor,
                        OrdemMae = line.OrdemMae,
                        CodAdministrador = line.CodAdministrador
                    };

                    listaOrdemPassivo.Add(itensOrdemPassivo);
                }

                await _context.BulkInsertAsync(listaOrdemPassivo);
                await _context.SaveChangesAsync();

                return Ok(itensOrdemPassivo);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/OrdemPassivo/DeletarOrdemPassivo/dt_entrada
        [HttpDelete("{dt_entrada}")]
        public async Task<ActionResult<IEnumerable<TblPagamentoServico>>> DeletarOrdemPassivo(DateTime dt_entrada)
        {
            IList<TblOrdemPassivo> tblOrdemPassivo = await _context.TblOrdemPassivo.Where(c => c.DtEntrada == dt_entrada).ToListAsync();

            if (tblOrdemPassivo == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblOrdemPassivo.RemoveRange(tblOrdemPassivo);
                await _context.SaveChangesAsync();
                return Ok(tblOrdemPassivo);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
