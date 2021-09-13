using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using DUDS.Service.Interface;

namespace DUDS.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    [ApiController]
    //[Authorize]
    
    //[ApiExplorerSettings(GroupName ="common")]
    public class InfoFundoController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;
        //private static readonly Dictionary<int, TblFundo> tblFundoStore = new Dictionary<int, TblFundo>();
        string Sistema = "DUDS";

        public InfoFundoController(DataContext context, IConfiguracaoService configService)
        {
            _context = context;
            _configService = configService;
        }

        #region Fundo
        // GET: api/InfoFundo/GetFundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblFundo>>> GetFundo()
        {
            try
            {
                List<TblFundo> fundos = await _context.TblFundo.Where(c => c.Ativo == true).OrderBy(c => c.NomeFundo).AsNoTracking().ToListAsync();

                if (fundos.Count() == 0)
                {
                    return NotFound();
                }

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
                return NotFound(e);
            }
        }

        // GET: api/InfoFundo/GetFundoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblFundo>> GetFundoById(int id)
        {
            TblFundo tblFundo = await _context.TblFundo.FindAsync(id);

            try
            {
                if (tblFundo != null)
                {
                    return Ok(tblFundo);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/Fundo/GetFundoExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<TblFundo>> GetFundoExistsBase(string cnpj)
        {
            TblFundo tblFundo = await _context.TblFundo.Where(c => c.Ativo == false && c.Cnpj == cnpj).FirstOrDefaultAsync();

            try
            {
                if (tblFundo != null)
                {
                    return Ok(tblFundo);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //POST: api/InfoFundo/AddFundo/FundoModel
        [HttpPost]
        public async Task<ActionResult<FundoModel>> AddFundo(FundoModel tblFundoModel)
        {
            TblFundo itensFundo = new TblFundo
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
                CodTipoEstrategia = tblFundoModel.CodTipoEstrategia,
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
                UsuarioModificacao = tblFundoModel.UsuarioModificacao
            };

            try
            {
                _context.TblFundo.Add(itensFundo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                  nameof(GetFundo),
                  new { id = itensFundo.Id },
                  Ok(itensFundo));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/InfoFundo/UpdateFundo/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFundo(int id, FundoModel fundo)
        {
            try
            {
                TblFundo registroFundo = _context.TblFundo.Find(id);

                if (registroFundo != null)
                {
                    registroFundo.NomeFundo = fundo.NomeFundo ?? registroFundo.NomeFundo;
                    registroFundo.NomeReduzido = fundo.NomeReduzido ?? registroFundo.NomeReduzido;
                    registroFundo.Cnpj = fundo.Cnpj ?? registroFundo.Cnpj;
                    registroFundo.PerformanceFee = (fundo.PerformanceFee == null || fundo.PerformanceFee == 0) ? registroFundo.PerformanceFee : fundo.PerformanceFee;
                    registroFundo.AdmFee = (fundo.AdmFee == null || fundo.AdmFee == 0) ? registroFundo.AdmFee : fundo.AdmFee;
                    registroFundo.TipoFundo = fundo.TipoFundo ?? registroFundo.TipoFundo;
                    registroFundo.MasterId = fundo.MasterId == 0 ? registroFundo.MasterId : fundo.MasterId;
                    registroFundo.TipoCota = fundo.TipoCota ?? registroFundo.TipoCota;
                    registroFundo.CodAdministrador = fundo.CodAdministrador == 0 ? registroFundo.CodAdministrador : fundo.CodAdministrador;
                    registroFundo.CodCustodiante = fundo.CodCustodiante == 0 ? registroFundo.CodCustodiante : fundo.CodCustodiante;
                    registroFundo.CodGestor = fundo.CodGestor == 0 ? registroFundo.CodGestor : fundo.CodGestor;
                    registroFundo.MoedaFundo = fundo.MoedaFundo ?? registroFundo.MoedaFundo;
                    registroFundo.CodTipoEstrategia = fundo.CodTipoEstrategia == 0 ? registroFundo.CodTipoEstrategia : fundo.CodTipoEstrategia;
                    registroFundo.DiasCotizacaoAplicacao = fundo.DiasCotizacaoAplicacao == 0 ? registroFundo.DiasCotizacaoAplicacao : fundo.DiasCotizacaoAplicacao;
                    registroFundo.ContagemDiasCotizacaoAplicacao = fundo.ContagemDiasCotizacaoAplicacao ?? registroFundo.ContagemDiasCotizacaoAplicacao;
                    registroFundo.DiasCotizacaoResgate = fundo.DiasCotizacaoResgate == 0 ? registroFundo.DiasCotizacaoResgate : fundo.DiasCotizacaoResgate;
                    registroFundo.ContagemDiasCotizacaoResgate = fundo.ContagemDiasCotizacaoResgate ?? registroFundo.ContagemDiasCotizacaoResgate;
                    registroFundo.DiasLiquidacaoAplicacao = fundo.DiasLiquidacaoAplicacao == 0 ? registroFundo.DiasLiquidacaoAplicacao : fundo.DiasLiquidacaoAplicacao;
                    registroFundo.ContagemDiasLiquidacaoAplicacao = fundo.ContagemDiasLiquidacaoAplicacao ?? registroFundo.ContagemDiasLiquidacaoAplicacao;
                    registroFundo.DiasLiquidacaoResgate = fundo.DiasLiquidacaoResgate == 0 ? registroFundo.DiasLiquidacaoResgate : fundo.DiasLiquidacaoResgate;
                    registroFundo.ContagemDiasLiquidacaoResgate = fundo.ContagemDiasLiquidacaoResgate ?? registroFundo.ContagemDiasLiquidacaoResgate;
                    registroFundo.Mnemonico = fundo.Mnemonico ?? registroFundo.Mnemonico;
                    registroFundo.NomeReduzido = fundo.NomeReduzido ?? registroFundo.NomeReduzido;
                    registroFundo.ClassificacaoAnbima = fundo.ClassificacaoAnbima ?? registroFundo.ClassificacaoAnbima;
                    registroFundo.ClassificacaoCvm = fundo.ClassificacaoCvm ?? registroFundo.ClassificacaoCvm;
                    registroFundo.DataCotaInicial = fundo.DataCotaInicial ?? registroFundo.DataCotaInicial;
                    registroFundo.ValorCotaInicial = (fundo.ValorCotaInicial == null || fundo.ValorCotaInicial == 0) ? registroFundo.ValorCotaInicial : fundo.ValorCotaInicial;
                    registroFundo.CodAnbima = fundo.CodAnbima ?? registroFundo.CodAnbima;
                    registroFundo.CodCvm = fundo.CodCvm ?? registroFundo.CodCvm;
                    registroFundo.AtivoCetip = fundo.AtivoCetip ?? registroFundo.AtivoCetip;
                    registroFundo.Isin = fundo.Isin ?? registroFundo.Isin;
                    registroFundo.NumeroGiin = fundo.NumeroGiin ?? registroFundo.NumeroGiin;
                    registroFundo.CdFundoAdm = fundo.CdFundoAdm == 0 ? registroFundo.CdFundoAdm : fundo.CdFundoAdm;
                    registroFundo.DataEncerramento = fundo.DataEncerramento ?? registroFundo.DataEncerramento;

                    try
                    {
                        //_context.Entry(registroFundo).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return Ok(registroFundo);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException e) when (!FundoExists(fundo.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/InfoFundo/DeleteFundo/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFundo(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblFundo tblFundo = await _context.TblFundo.FindAsync(id);

                if (tblFundo == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblFundo.Remove(tblFundo);
                    await _context.SaveChangesAsync();
                    return Ok(tblFundo);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // DESATIVA: api/InfoFundo/DisableFundo/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableFundo(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblFundo registroFundo = _context.TblFundo.Find(id);

                if (registroFundo != null)
                {
                    registroFundo.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroFundo);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // ATIVAR: api/InfoFundo/ActivateFundo/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateFundo(int id)
        {
            TblFundo registroFundo = await _context.TblFundo.FindAsync(id);

            if (registroFundo != null)
            {
                registroFundo.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroFundo);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }


        private bool FundoExists(int id)
        {
            return _context.TblFundo.Any(e => e.Id == id);
        }
        #endregion

        #region Tipo Estrategia
        // GET: api/InfoFundo/GetTipoEstrategia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoEstrategia>>> GetTipoEstrategia()
        {
            try
            {
                var tiposEstrategia = await _context.TblTipoEstrategia.Where(c => c.Ativo == true).OrderBy(c => c.Estrategia).ToListAsync();

                if (tiposEstrategia.Count() == 0)
                {
                    return NotFound();
                }

                if (tiposEstrategia != null)
                {
                    return Ok(tiposEstrategia);
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

        // GET: api/InfoFundo/GetTipoEstrategiaById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTipoEstrategia>> GetTipoEstrategiaById(int id)
        {
            TblTipoEstrategia tblTipoEstrategia = await _context.TblTipoEstrategia.FindAsync(id);

            try
            {
                if (tblTipoEstrategia != null)
                {
                    return Ok(tblTipoEstrategia);
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

        //POST: api/InfoFundo/AddTipoEstrategia/TipoContaModel
        [HttpPost]
        public async Task<ActionResult<TipoEstrategiaModel>> AddTipoEstrategia(TipoEstrategiaModel tblTipoEstrategiaModel)
        {
            TblTipoEstrategia itensTipoEstrategia = new TblTipoEstrategia
            {
                Id = tblTipoEstrategiaModel.Id,
                Estrategia = tblTipoEstrategiaModel.Estrategia,
                UsuarioModificacao = tblTipoEstrategiaModel.UsuarioModificacao
            };

            try
            {
                _context.TblTipoEstrategia.Add(itensTipoEstrategia);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetTipoEstrategia),
                    new
                    {
                        Id = itensTipoEstrategia.Id,
                    },
                     Ok(itensTipoEstrategia));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/InfoFundo/UpdateTipoEstrategia/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoEstrategia(int id, TipoEstrategiaModel tipoEstrategia)
        {
            try
            {
                TblTipoEstrategia registroTipoEstrategia = _context.TblTipoEstrategia.Find(id);

                if (registroTipoEstrategia != null)
                {
                    registroTipoEstrategia.Estrategia = tipoEstrategia.Estrategia == null ? registroTipoEstrategia.Estrategia : tipoEstrategia.Estrategia;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroTipoEstrategia);
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException e) when (!TipoEstrategiaExists(tipoEstrategia.Id))
            {
                return NotFound(e);
            }
        }

        // DELETE: api/InfoFundo/DeleteTipoEstrategia/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoEstrategia(int id)
        {
            TblTipoEstrategia tblTipoEstrategia = await _context.TblTipoEstrategia.FindAsync(id);

            if (tblTipoEstrategia == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblTipoEstrategia.Remove(tblTipoEstrategia);
                await _context.SaveChangesAsync();
                return Ok(tblTipoEstrategia);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DESATIVA: api/InfoFundo/DisableTipoEstrategia/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableTipoEstrategia(int id)
        {
            TblTipoEstrategia registroTipoEstrategia = _context.TblTipoEstrategia.Find(id);

            if (registroTipoEstrategia != null)
            {
                registroTipoEstrategia.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroTipoEstrategia);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // ATIVAR: api/InfoFundo/ActivateTipoEstrategia/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateTipoEstrategia(int id)
        {
            TblTipoEstrategia registroTipoEstrategia = await _context.TblTipoEstrategia.FindAsync(id);

            if (registroTipoEstrategia != null)
            {
                registroTipoEstrategia.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroTipoEstrategia);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        private bool TipoEstrategiaExists(int id)
        {
            return _context.TblTipoEstrategia.Any(e => e.Id == id);
        }
        #endregion
    }
}
