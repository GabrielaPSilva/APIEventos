using DUDS.Data;
using DUDS.Models;
using DUDS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class TiposController : ControllerBase
    {
        private readonly ITipoClassificacaoService _tipoClassificacaoService;
        private readonly ITipoCondicaoService _tipoCondicaoService;
        private readonly DataContext _context;
        private readonly IConfiguracaoService _configService;

        public TiposController(DataContext context, IConfiguracaoService configService, ITipoClassificacaoService tipoClassificacaoService, ITipoCondicaoService tipoCondicaoService)
        {
            _tipoClassificacaoService = tipoClassificacaoService;
            _tipoCondicaoService = tipoCondicaoService;
            _context = context;
            _configService = configService;
        }

        #region Tipo Classificação
        // GET: api/TipoClassificacao/GetTipoClassificacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoClassificacaoModel>>> GetTipoClassificacao()
        {
            try
            {
                var tipoClassificacaoes = await _tipoClassificacaoService.GetAllAsync();

                if (tipoClassificacaoes.Any())
                {
                    return Ok(tipoClassificacaoes);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/TipoClassificacao/GetTipoClassificacaoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoClassificacaoModel>> GetTipoClassificacaoById(int id)
        {
            try
            {
                var tblTipoClassificacao = await _tipoClassificacaoService.GetByIdAsync(id);

                if (tblTipoClassificacao == null)
                {
                    return NotFound();
                }

                return Ok(tblTipoClassificacao);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/TipoClassificacao/AddTipoClassificacao/TipoClassificacaoModel
        [HttpPost]
        public async Task<ActionResult<TipoClassificacaoModel>> AddTipoClassificacao(TipoClassificacaoModel tipoClassificacao)
        {
            try
            {
                var retorno = await _tipoClassificacaoService.AddAsync(tipoClassificacao);

                return CreatedAtAction(
                    nameof(GetTipoClassificacao),
                    new { id = tipoClassificacao.Id }, tipoClassificacao);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //PUT: api/TipoClassificacao/UpdateTipoClassificacao/id
        [HttpPut("{id}")]
        public async Task<ActionResult<TipoClassificacaoModel>> UpdateTipoClassificacao(int id, TipoClassificacaoModel tipoClassificacao)
        {
            try
            {
                var retornoTipoClassificacao = await _tipoClassificacaoService.GetByIdAsync(id);

                if (retornoTipoClassificacao == null)
                {
                    return NotFound();
                }

                tipoClassificacao.Id = id;
                bool retorno = await _tipoClassificacaoService.UpdateAsync(tipoClassificacao);

                if (retorno)
                {
                    return Ok(tipoClassificacao);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/TipoClassificacao/DeleteTipoClassificacao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoClassificacao(int id)
        {
            try
            {
                var registroTipoClassificacao = await _tipoClassificacaoService.Disable(id);

                if (registroTipoClassificacao)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // ATIVAR: api/TipoClassificacao/ActivateTipoClassificacao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateTipoClassificacao(int id)
        {
            var registroTipoClassificacao = await _tipoClassificacaoService.GetByIdAsync(id);

            if (registroTipoClassificacao != null)
            {
                try
                {
                    await _tipoClassificacaoService.ActivateAsync(id);
                    return Ok(registroTipoClassificacao);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        #endregion

        #region Tipo Condição
        // GET: api/TipoCondicao/GetTipoCondicao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCondicaoModel>>> GetTipoCondicao()
        {
            try
            {
                var tipoCondicoes = await _tipoCondicaoService.GetAllAsync();

                if (tipoCondicoes.Any())
                {
                    return Ok(tipoCondicoes);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/TipoCondicao/GetTipoCondicaoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCondicaoModel>> GetTipoCondicaoById(int id)
        {
            try
            {
                var tblTipoCondicao = await _tipoCondicaoService.GetByIdAsync(id);

                if (tblTipoCondicao == null)
                {
                    return NotFound();
                }

                return Ok(tblTipoCondicao);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/TipoCondicao/AddTipoCondicao/TipoCondicaoModel
        [HttpPost]
        public async Task<ActionResult<TipoCondicaoModel>> AddTipoCondicao(TipoCondicaoModel tipoCondicao)
        {
            try
            {
                var retorno = await _tipoCondicaoService.AddAsync(tipoCondicao);

                return CreatedAtAction(
                    nameof(GetTipoCondicao),
                    new { id = tipoCondicao.Id }, tipoCondicao);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //PUT: api/TipoCondicao/UpdateTipoCondicao/id
        [HttpPut("{id}")]
        public async Task<ActionResult<TipoCondicaoModel>> UpdateTipoCondicao(int id, TipoCondicaoModel tipoCondicao)
        {
            try
            {
                var retornoTipoCondicao = await _tipoCondicaoService.GetByIdAsync(id);

                if (retornoTipoCondicao == null)
                {
                    return NotFound();
                }

                tipoCondicao.Id = id;
                bool retorno = await _tipoCondicaoService.UpdateAsync(tipoCondicao);

                if (retorno)
                {
                    return Ok(tipoCondicao);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/TipoCondicao/DeleteTipoCondicao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoCondicao(int id)
        {
            try
            {
                var registroTipoCondicao = await _tipoCondicaoService.Disable(id);

                if (registroTipoCondicao)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // ATIVAR: api/TipoCondicao/ActivateTipoCondicao/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateTipoCondicao(int id)
        {
            var registroTipoCondicao = await _tipoCondicaoService.GetByIdAsync(id);

            if (registroTipoCondicao != null)
            {
                try
                {
                    await _tipoCondicaoService.ActivateAsync(id);
                    return Ok(registroTipoCondicao);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }
        #endregion

        #region Tipo Conta
        // GET: api/Contas/GetTipoContas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoConta>>> GetTipoContas()
        {
            try
            {
                var tiposConta = await _context.TblTipoConta.Where(c => c.Ativo == true).OrderBy(c => c.TipoConta).ToListAsync();

                if (tiposConta.Count == 0)
                {
                    return NotFound();
                }

                return Ok(tiposConta);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Contas/GetTipoContasById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTipoConta>> GetTipoContasById(int id)
        {
            try
            {
                TblTipoConta tblContas = await _context.TblTipoConta.FindAsync(id);
                if (tblContas == null)
                {
                    NotFound();
                }

                return Ok(tblContas);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Contas/GetTipoContaExistsBase/tipoConta/descricaoConta
        [HttpGet("{tipoConta}/{descricaoConta}")]
        public async Task<ActionResult<TblTipoConta>> GetTipoContaExistsBase(string tipoConta, string descricaoConta)
        {
            TblTipoConta tblTipoConta = new TblTipoConta();

            try
            {
                tblTipoConta = await _context.TblTipoConta.Where(c => c.Ativo == false && c.TipoConta == tipoConta && c.DescricaoConta == descricaoConta).FirstOrDefaultAsync();

                if (tblTipoConta != null)
                {
                    return Ok(tblTipoConta);
                }

                tblTipoConta = await _context.TblTipoConta.Where(c => c.TipoConta == tipoConta && c.DescricaoConta == descricaoConta).FirstOrDefaultAsync();

                if (tblTipoConta != null)
                {
                    return Ok(tblTipoConta);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //POST: api/Contas/AddTipoConta/TipoContaModel
        [HttpPost]
        public async Task<ActionResult<TipoContaModel>> AddTipoConta(TipoContaModel tblTipoContaModel)
        {
            TblTipoConta itensTipoConta = new TblTipoConta
            {
                Id = tblTipoContaModel.Id,
                TipoConta = tblTipoContaModel.TipoConta,
                DescricaoConta = tblTipoContaModel.DescricaoConta,
                UsuarioModificacao = tblTipoContaModel.UsuarioModificacao
            };

            try
            {
                _context.TblTipoConta.Add(itensTipoConta);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetTipoContas),
                    new
                    {
                        Id = itensTipoConta.Id,
                    }, itensTipoConta);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //PUT: api/Contas/UpdateTipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoConta(int id, TipoContaModel tipoConta)
        {
            try
            {
                TblTipoConta registroTipoConta = _context.TblTipoConta.Find(id);

                if (registroTipoConta != null)
                {
                    registroTipoConta.TipoConta = tipoConta.TipoConta == null ? registroTipoConta.TipoConta : tipoConta.TipoConta;
                    registroTipoConta.DescricaoConta = tipoConta.DescricaoConta == null ? registroTipoConta.DescricaoConta : tipoConta.DescricaoConta;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroTipoConta);
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
            catch (DbUpdateConcurrencyException e) when (!TipoContasExists(tipoConta.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/Contas/DeleteTipoConta/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoConta(int id)
        {
            TblTipoConta tblTipoConta = await _context.TblTipoConta.FindAsync(id);

            if (tblTipoConta == null)
            {
                return NotFound();
            }

            try
            {
                _context.TblTipoConta.Remove(tblTipoConta);
                await _context.SaveChangesAsync();
                return Ok(tblTipoConta);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DESATIVA: api/Conta/DisableTipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableTipoConta(int id)
        {
            TblTipoConta registroTipoConta = _context.TblTipoConta.Find(id);

            if (registroTipoConta != null)
            {
                registroTipoConta.Ativo = false;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroTipoConta);
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

        // ATIVAR: api/Conta/ActivateTipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateTipoConta(int id)
        {
            TblTipoConta registroTipoConta = await _context.TblTipoConta.FindAsync(id);

            if (registroTipoConta != null)
            {
                registroTipoConta.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroTipoConta);
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

        private bool TipoContasExists(int id)
        {
            return _context.TblTipoConta.Any(e => e.Id == id);
        }

        #endregion

        #region Tipo Contrato
        // GET: api/Contrato/GetTipoContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoContrato>>> GetTipoContrato()
        {
            try
            {
                List<TblTipoContrato> tipoContratos = await _context.TblTipoContrato.Where(c => c.Ativo == true).OrderBy(c => c.TipoContrato).AsNoTracking().ToListAsync();

                if (tipoContratos.Count == 0)
                {
                    return NotFound();
                }

                return Ok(tipoContratos);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // GET: api/Contrato/GetTipoContratoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTipoContrato>> GetTipoContratoById(int id)
        {
            try
            {
                TblTipoContrato tblTipoContrato = await _context.TblTipoContrato.FindAsync(id);
                if (tblTipoContrato == null)
                {
                    return NotFound();
                }
                return Ok(tblTipoContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        //POST: api/Contrato/AddTipoContrato/TipoContratoModel
        [HttpPost]
        public async Task<ActionResult<TipoContratoModel>> AddTipoContrato(TipoContratoModel tblTipoContratoModel)
        {
            TblTipoContrato itensTipoContrato = new TblTipoContrato
            {
                TipoContrato = tblTipoContratoModel.TipoContrato,
                UsuarioModificacao = tblTipoContratoModel.UsuarioModificacao
            };

            try
            {
                _context.TblTipoContrato.Add(itensTipoContrato);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                   nameof(GetTipoContrato),
                   new { id = itensTipoContrato.Id }, itensTipoContrato);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT: api/Contrato/UpdateTipoContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoContrato(int id, TipoContratoModel tipoContrato)
        {
            try
            {
                TblTipoContrato registroTipoContrato = await _context.TblTipoContrato.FindAsync(id);

                if (registroTipoContrato != null)
                {
                    registroTipoContrato.TipoContrato = tipoContrato.TipoContrato == null ? registroTipoContrato.TipoContrato : tipoContrato.TipoContrato;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroTipoContrato);
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
            catch (DbUpdateConcurrencyException e) when (!TipoContratoExists(tipoContrato.Id))
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        // DELETE: api/Contrato/DeleteTipoContrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_tipo_contrato");

            if (!existeRegistro)
            {
                TblTipoContrato tblTipoContrato = await _context.TblTipoContrato.FindAsync(id);

                if (tblTipoContrato == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.TblTipoContrato.Remove(tblTipoContrato);
                    await _context.SaveChangesAsync();
                    return Ok(tblTipoContrato);
                }
                catch (Exception e)
                {
                    return BadRequest(e.InnerException.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // DESATIVA: api/Contrato/DisableTipoContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> DisableTipoContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_tipo_contrato");

            if (!existeRegistro)
            {
                TblTipoContrato registroTipoContrato = await _context.TblTipoContrato.FindAsync(id);

                if (registroTipoContrato != null)
                {
                    registroTipoContrato.Ativo = false;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(registroTipoContrato);
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
            else
            {
                return BadRequest();
            }
        }

        // ATIVAR: api/Contrato/ActivateTipoContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateTipoContrato(int id)
        {
            TblTipoContrato registroTipoContrato = await _context.TblTipoContrato.FindAsync(id);

            if (registroTipoContrato != null)
            {
                registroTipoContrato.Ativo = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(registroTipoContrato);
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

        private bool TipoContratoExists(int id)
        {
            return _context.TblTipoContrato.Any(e => e.Id == id);
        }
        #endregion

        #region Tipo Estrategia
        // GET: api/InfoFundo/GetTipoEstrategia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTipoEstrategia>>> GetTipoEstrategia()
        {
            try
            {
                List<TblTipoEstrategia> tiposEstrategia = await _context.TblTipoEstrategia.Where(c => c.Ativo == true).OrderBy(c => c.Estrategia).ToListAsync();

                if (tiposEstrategia.Count == 0)
                {
                    return NotFound();
                }

                return Ok(tiposEstrategia);
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
                if (tblTipoEstrategia == null)
                {
                    return NotFound();
                }
                return Ok(tblTipoEstrategia);
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
                    }, itensTipoEstrategia);
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
