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

        #region Fundo
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
                    return NotFound();
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
                UsuarioModificacao = tblFundoModel.UsuarioModificacao,
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
                Ativo = tblFundoModel.Ativo
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
        public async Task<IActionResult> DeletarFundo(int id)
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

        // DESATIVA: api/Fundo/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarFundo(int id)
        {
            var registroFundo = _context.TblFundo.Find(id);

            if (registroFundo != null)
            {
                registroFundo.Ativo = false;

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        //PUT: api/Fundo/id
        [HttpPut("{Fundo}")]
        public async Task<IActionResult> EditarFundo([FromBody] FundoModel fundo)
        {
            try
            {
                var registroFundo = _context.TblFundo.Find(fundo.Id);

                if (registroFundo != null)
                {
                    registroFundo.NomeFundo = fundo.NomeFundo;
                    registroFundo.NomeReduzido = fundo.NomeReduzido;
                    registroFundo.Cnpj = fundo.Cnpj;
                    registroFundo.PerformanceFee = fundo.PerformanceFee;
                    registroFundo.AdmFee = fundo.AdmFee;
                    registroFundo.TipoFundo = fundo.TipoFundo;
                    registroFundo.MasterId = fundo.MasterId;
                    registroFundo.TipoCota = fundo.TipoCota;
                    registroFundo.CodAdministrador = fundo.CodAdministrador;
                    registroFundo.CodCustodiante = fundo.CodCustodiante;
                    registroFundo.CodGestor = fundo.CodGestor;
                    registroFundo.MoedaFundo = fundo.MoedaFundo;
                    registroFundo.Estrategia = fundo.Estrategia;
                    registroFundo.DiasCotizacaoAplicacao = fundo.DiasCotizacaoAplicacao;
                    registroFundo.ContagemDiasCotizacaoAplicacao = fundo.ContagemDiasCotizacaoAplicacao;
                    registroFundo.DiasCotizacaoResgate = fundo.DiasCotizacaoResgate;
                    registroFundo.ContagemDiasCotizacaoResgate = fundo.ContagemDiasCotizacaoResgate;
                    registroFundo.DiasLiquidacaoAplicacao = fundo.DiasLiquidacaoAplicacao;
                    registroFundo.ContagemDiasLiquidacaoAplicacao = fundo.ContagemDiasLiquidacaoAplicacao;
                    registroFundo.DiasLiquidacaoResgate = fundo.DiasLiquidacaoResgate;
                    registroFundo.ContagemDiasLiquidacaoResgate = fundo.ContagemDiasLiquidacaoResgate;
                    registroFundo.Mnemonico = fundo.Mnemonico;
                    registroFundo.UsuarioModificacao = fundo.UsuarioModificacao;
                    registroFundo.NomeReduzido = fundo.NomeReduzido;
                    registroFundo.ClassificacaoAnbima = fundo.ClassificacaoAnbima;
                    registroFundo.ClassificacaoCvm = fundo.ClassificacaoCvm;
                    registroFundo.DataCotaInicial = fundo.DataCotaInicial;
                    registroFundo.ValorCotaInicial = fundo.ValorCotaInicial;
                    registroFundo.CodAnbima = fundo.CodAnbima;
                    registroFundo.CodCvm = fundo.CodCvm;
                    registroFundo.AtivoCetip = fundo.AtivoCetip;
                    registroFundo.Isin = fundo.Isin;
                    registroFundo.NumeroGiin = fundo.NumeroGiin;
                    registroFundo.CdFundoAdm = fundo.CdFundoAdm;
                    registroFundo.DataEncerramento = fundo.DataEncerramento;

                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException) when (!FundoExists(fundo.Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool FundoExists(int id)
        {
            return _context.TblFundo.Any(e => e.Id == id);
        }
        #endregion
    }
}
