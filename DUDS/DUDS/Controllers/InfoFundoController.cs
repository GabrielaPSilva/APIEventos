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

        #region De Para Fundo
        // GET: api/InfoFundo/DeparaFundoProduto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblDeparaFundoproduto>>> DeparaFundoProduto()
        {
            return await _context.TblDeparaFundoproduto.ToListAsync();
        }
        #endregion

        #region Fundo
        // GET: api/InfoFundo/Fundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblFundo>>> Fundo()
        {
            try
            {
                List<TblFundo> fundos = await _context.TblFundo.Where(c => c.Ativo == true).OrderBy(c => c.NomeFundo).AsNoTracking().ToListAsync();

                if (fundos.Count() == 0)
                {
                    return BadRequest(Mensagem.ErroListar);
                }

                if (fundos != null)
                {
                    return Ok(new { fundos, Mensagem.SucessoListar });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (InvalidOperationException e)
            {
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // GET: api/InfoFundo/GetFundo/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblFundo>> GetFundo(int id)
        {
            TblFundo tblFundo = await _context.TblFundo.FindAsync(id);

            try
            {
                if (tblFundo != null)
                {
                    return Ok(new { tblFundo, Mensagem.SucessoCadastrado });
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        //POST: api/InfoFundo/CadastrarFundo/FundoModel
        [HttpPost]
        public async Task<ActionResult<FundoModel>> CadastrarFundo(FundoModel tblFundoModel)
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

            try
            {
                _context.TblFundo.Add(itensFundo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                  nameof(GetFundo),
                  new { id = itensFundo.Id },
                  Ok(new { itensFundo, Mensagem.SucessoCadastrado }));
            }
            catch (Exception e)
            {
                return BadRequest(new { Erro = e, Mensagem.ErroCadastrar });
            }
        }

        //PUT: api/InfoFundo/EditarFundo/id
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarFundo(int id, FundoModel fundo)
        {
            try
            {
                TblFundo registroFundo = _context.TblFundo.Find(id);

                if (registroFundo != null)
                {
                    registroFundo.NomeFundo = fundo.NomeFundo == null ? registroFundo.NomeFundo : fundo.NomeFundo;
                    registroFundo.NomeReduzido = fundo.NomeReduzido == null ? registroFundo.NomeReduzido : fundo.NomeReduzido;
                    registroFundo.Cnpj = fundo.Cnpj == null ? registroFundo.Cnpj : fundo.Cnpj;
                    registroFundo.PerformanceFee = (fundo.PerformanceFee == null || fundo.PerformanceFee == 0) ? registroFundo.PerformanceFee : fundo.PerformanceFee;
                    registroFundo.AdmFee = (fundo.AdmFee == null || fundo.AdmFee == 0) ? registroFundo.AdmFee : fundo.AdmFee;
                    registroFundo.TipoFundo = fundo.TipoFundo == null ? registroFundo.TipoFundo : fundo.TipoFundo;
                    registroFundo.MasterId = (fundo.MasterId == null || fundo.MasterId == 0) ? registroFundo.MasterId : fundo.MasterId;
                    registroFundo.TipoCota = fundo.TipoCota == null ? registroFundo.TipoCota : fundo.TipoCota;
                    registroFundo.CodAdministrador = fundo.CodAdministrador == 0 ? registroFundo.CodAdministrador : fundo.CodAdministrador;
                    registroFundo.CodCustodiante = fundo.CodCustodiante == 0 ? registroFundo.CodCustodiante : fundo.CodCustodiante;
                    registroFundo.CodGestor = (fundo.CodGestor == 0 || fundo.CodGestor == null) ? registroFundo.CodGestor : fundo.CodGestor;
                    registroFundo.MoedaFundo = fundo.MoedaFundo == null ? registroFundo.MoedaFundo : fundo.MoedaFundo;
                    registroFundo.Estrategia = fundo.Estrategia == null ? registroFundo.Estrategia : fundo.Estrategia;
                    registroFundo.DiasCotizacaoAplicacao = (fundo.DiasCotizacaoAplicacao == null || fundo.DiasCotizacaoAplicacao == 0) ? registroFundo.DiasCotizacaoAplicacao : fundo.DiasCotizacaoAplicacao;
                    registroFundo.ContagemDiasCotizacaoAplicacao = fundo.ContagemDiasCotizacaoAplicacao == null ? registroFundo.ContagemDiasCotizacaoAplicacao : fundo.ContagemDiasCotizacaoAplicacao;
                    registroFundo.DiasCotizacaoResgate = (fundo.DiasCotizacaoResgate == null || fundo.DiasCotizacaoResgate == 0) ? registroFundo.DiasCotizacaoResgate : fundo.DiasCotizacaoResgate;
                    registroFundo.ContagemDiasCotizacaoResgate = fundo.ContagemDiasCotizacaoResgate == null ? registroFundo.ContagemDiasCotizacaoResgate : fundo.ContagemDiasCotizacaoResgate;
                    registroFundo.DiasLiquidacaoAplicacao = (fundo.DiasLiquidacaoAplicacao == null || fundo.DiasLiquidacaoAplicacao == 0) ? registroFundo.DiasLiquidacaoAplicacao : fundo.DiasLiquidacaoAplicacao;
                    registroFundo.ContagemDiasLiquidacaoAplicacao = fundo.ContagemDiasLiquidacaoAplicacao == null ? registroFundo.ContagemDiasLiquidacaoAplicacao : fundo.ContagemDiasLiquidacaoAplicacao;
                    registroFundo.DiasLiquidacaoResgate = (fundo.DiasLiquidacaoResgate == null || fundo.DiasLiquidacaoResgate == 0) ? registroFundo.DiasLiquidacaoResgate : fundo.DiasLiquidacaoResgate;
                    registroFundo.ContagemDiasLiquidacaoResgate = fundo.ContagemDiasLiquidacaoResgate == null ? registroFundo.ContagemDiasLiquidacaoResgate : fundo.ContagemDiasLiquidacaoResgate;
                    registroFundo.Mnemonico = fundo.Mnemonico == null ? registroFundo.Mnemonico : fundo.Mnemonico;
                    registroFundo.UsuarioModificacao = fundo.UsuarioModificacao == null ? registroFundo.UsuarioModificacao : fundo.UsuarioModificacao;
                    registroFundo.NomeReduzido = fundo.NomeReduzido == null ? registroFundo.NomeReduzido : fundo.NomeReduzido;
                    registroFundo.ClassificacaoAnbima = fundo.ClassificacaoAnbima == null ? registroFundo.ClassificacaoAnbima : fundo.ClassificacaoAnbima;
                    registroFundo.ClassificacaoCvm = fundo.ClassificacaoCvm == null ? registroFundo.ClassificacaoCvm : fundo.ClassificacaoCvm;
                    registroFundo.DataCotaInicial = fundo.DataCotaInicial == null ? registroFundo.DataCotaInicial : fundo.DataCotaInicial;
                    registroFundo.ValorCotaInicial = (fundo.ValorCotaInicial == null || fundo.ValorCotaInicial == 0) ? registroFundo.ValorCotaInicial : fundo.ValorCotaInicial;
                    registroFundo.CodAnbima = fundo.CodAnbima == null ? registroFundo.CodAnbima : fundo.CodAnbima;
                    registroFundo.CodCvm = fundo.CodCvm == null ? registroFundo.CodCvm : fundo.CodCvm;
                    registroFundo.AtivoCetip = fundo.AtivoCetip == null ? registroFundo.AtivoCetip : fundo.AtivoCetip;
                    registroFundo.Isin = fundo.Isin == null ? registroFundo.Isin : fundo.Isin;
                    registroFundo.NumeroGiin = fundo.NumeroGiin == null ? registroFundo.NumeroGiin : fundo.NumeroGiin;
                    registroFundo.CdFundoAdm = (fundo.CdFundoAdm == null || fundo.CdFundoAdm == 0) ? registroFundo.CdFundoAdm : fundo.CdFundoAdm;
                    registroFundo.DataEncerramento = fundo.DataEncerramento == null ? registroFundo.DataEncerramento : fundo.DataEncerramento;

                    try
                    {
                        //_context.Entry(registroFundo).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return Ok(new { Mensagem.SucessoAtualizado });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Erro = e, Mensagem.ErroAtualizar });
                    }
                }
                else
                {
                    return BadRequest(Mensagem.ErroTipoInvalido);
                }
            }
            catch (DbUpdateConcurrencyException e) when (!FundoExists(fundo.Id))
            {
                return NotFound(new { Erro = e, Mensagem.ErroPadrao });
            }
        }

        // DELETE: api/InfoFundo/DeletarFundo/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarFundo(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                TblFundo tblFundo = await _context.TblFundo.FindAsync(id);

                if (tblFundo == null)
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }

                try
                {
                    _context.TblFundo.Remove(tblFundo);
                    await _context.SaveChangesAsync();
                    return Ok(new { Mensagem.SucessoExcluido });
                }
                catch (Exception e)
                {
                    return BadRequest(new { Erro = e, Mensagem.ErroExcluir });
                }
            }
            else
            {
                return BadRequest(Mensagem.ExisteRegistroDesativar);
            }
        }

        // DESATIVA: api/InfoFundo/DesativarFundo/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DesativarFundo(int id)
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
                        return Ok(new { Mensagem.SucessoDesativado });
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Erro = e, Mensagem.ErroDesativar });
                    }
                }
                else
                {
                    return NotFound(Mensagem.ErroTipoInvalido);
                }
            }
            else
            {
                return BadRequest(Mensagem.ExisteRegistroDesativar);
            }
        }

        private bool FundoExists(int id)
        {
            return _context.TblFundo.Any(e => e.Id == id);
        }
        #endregion
    }
}
