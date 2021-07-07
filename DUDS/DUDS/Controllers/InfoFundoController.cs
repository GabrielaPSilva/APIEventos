using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using Microsoft.AspNetCore.Authorization;

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class InfoFundoController : Controller
    {
        private readonly DataContext _context;
        private static readonly Dictionary<int, TblFundo> tblFundoStore = new Dictionary<int, TblFundo>();

        public InfoFundoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDeparaFundoproduto>>> DeparaFundoproduto()
        {
            return await _context.TblDeparaFundoproduto.ToListAsync();
        }

        // GET: api/Fundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblFundo>>> Fundo()
        {
            try
            {
                var fundos = await _context.TblFundo.OrderBy(c => c.NomeFundo).AsNoTracking().ToListAsync();

                if (fundos != null)
                {
                    return Ok(fundos);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (InvalidOperationException e)
            {
                return BadRequest();
            }
        }

        // GET: api/Fundo/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblFundo>> GetFundo(int id)
        {
            var tblFundo = await _context.TblFundo.FindAsync(id);

            if (tblFundo == null)
            {
                return NotFound();
            }

            return Ok(tblFundo);
        }

        [HttpPost]
        public async Task<ActionResult<FundoModel>> CadastrarFundo(FundoModel tblFundoModel)
        {
            var itensFundo = new TblFundo
            {
                NomeFundo = tblFundoModel.NomeFundo,
                Cnpj = tblFundoModel.Cnpj,
                PerformanceFee = tblFundoModel.PerformanceFee,
                AdmFee = tblFundoModel.AdmFee,
                TipoFundo = tblFundoModel.TipoFundo,
                MasterId = tblFundoModel.MasterId,
                TipoCota = tblFundoModel.TipoCota,
                CodAdministrador = tblFundoModel.CodAdministrador,
                CodCustodiante = tblFundoModel.CodCustodiante,
                CodGestor = tblFundoModel.CodGestor,
                MoedaFundo = tblFundoModel.MoedaFundo,
                Estrategia = tblFundoModel.Estrategia,
                DiasCotizacaoAplicacao = tblFundoModel.DiasCotizacaoAplicacao,
                ContagemDiasCotizacaoAplicacao = tblFundoModel.ContagemDiasCotizacaoAplicacao,
                DiasCotizacaoResgate = tblFundoModel.DiasCotizacaoResgate,
                ContagemDiasCotizacaoResgate = tblFundoModel.ContagemDiasCotizacaoResgate,
                DiasLiquidacaoAplicacao = tblFundoModel.DiasLiquidacaoAplicacao,
                ContagemDiasLiquidacaoAplicacao = tblFundoModel.ContagemDiasLiquidacaoAplicacao,
                DiasLiquidacaoResgate = tblFundoModel.DiasLiquidacaoResgate,
                ContagemDiasLiquidacaoResgate = tblFundoModel.ContagemDiasLiquidacaoResgate,
                Mnemonico = tblFundoModel.Mnemonico,

                NomeReduzido = tblFundoModel.NomeReduzido,
                ClassificacaoAnbima = tblFundoModel.ClassificacaoAnbima,
                ClassificacaoCvm = tblFundoModel.ClassificacaoCvm,
                DataCotaInicial = tblFundoModel.DataCotaInicial,
                ValorCotaInicial = tblFundoModel.ValorCotaInicial,
                CodAnbima = tblFundoModel.CodAnbima,
                CodCvm = tblFundoModel.CodCvm,
                AtivoCetip = tblFundoModel.AtivoCetip,
                Isin = tblFundoModel.Isin,
                NumeroGiin = tblFundoModel.NumeroGiin,
                CdFundoAdm = tblFundoModel.CdFundoAdm,
                DataEncerramento = tblFundoModel.DataEncerramento
            };

            _context.TblFundo.Add(itensFundo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetFundo),
                new { id = itensFundo.Id },
                Ok(itensFundo));
        }

        // DELETE: api/Fundo/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFundo(int id)
        {
            var tblFundo = await _context.TblFundo.FindAsync(id);

            if (tblFundo == null)
            {
                return NotFound();
            }

            _context.TblFundo.Remove(tblFundo);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
