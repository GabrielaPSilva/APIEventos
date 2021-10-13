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
        private readonly ITipoContaService _tipoContaService;
        private readonly ITipoContratoService _tipoContratoService;
        private readonly ITipoEstrategiaService _tipoEstrategiaService;
        private readonly IConfiguracaoService _configService;

        public TiposController(IConfiguracaoService configService,
                               ITipoClassificacaoService tipoClassificacaoService,
                               ITipoCondicaoService tipoCondicaoService,
                               ITipoContaService tipoContaService,
                               ITipoContratoService tipoContratoService,
                               ITipoEstrategiaService tipoEstrategiaService)
        {
            _tipoClassificacaoService = tipoClassificacaoService;
            _tipoCondicaoService = tipoCondicaoService;
            _tipoContaService = tipoContaService;
            _tipoContratoService = tipoContratoService;
            _tipoEstrategiaService = tipoEstrategiaService;
            _configService = configService;
        }

        #region Tipo Classificação
        // GET: api/Tipos/GetTipoClassificacao
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

        // GET: api/Tipos/GetTipoClassificacaoById/id
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

        // GET: api/Tipos/GetTipoClassificacaoExistsBase/classificacao
        [HttpGet("{classificacao}")]
        public async Task<ActionResult<AdministradorModel>> GetTipoClassificacaoExistsBase(string classificacao)
        {
            try
            {
                var tblTipoClassificacao = await _tipoClassificacaoService.GetTipoClassificacaoExistsBase(classificacao);

                if (tblTipoClassificacao != null)
                {
                    return Ok(tblTipoClassificacao);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/Tipos/AddTipoClassificacao/TipoClassificacaoModel
        [HttpPost]
        public async Task<ActionResult<TipoClassificacaoModel>> AddTipoClassificacao(TipoClassificacaoModel tipoClassificacao)
        {
            try
            {
                var retorno = await _tipoClassificacaoService.AddAsync(tipoClassificacao);

                return CreatedAtAction(
                    nameof(GetTipoClassificacaoById),
                    new { id = tipoClassificacao.Id }, tipoClassificacao);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //PUT: api/Tipos/UpdateTipoClassificacao/id
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
                return BadRequest();
            }
        }

        // DELETE: api/Tipos/DeleteTipoClassificacao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoClassificacao(int id)
        {
            try
            {
                var registroTipoClassificacao = await _tipoClassificacaoService.DisableAsync(id);

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

        // ATIVAR: api/Tipos/ActivateTipoClassificacao/id
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
        // GET: api/Tipos/GetTipoCondicao
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

        // GET: api/Tipos/GetTipoCondicaoById/id
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

        // GET: api/Tipos/GetTipoCondicaoExistsBase/tipoCondicao
        [HttpGet("{tipoCondicao}")]
        public async Task<ActionResult<AdministradorModel>> GetTipoCondicaoExistsBase(string tipoCondicao)
        {
            try
            {
                var tblTipoCondicao = await _tipoCondicaoService.GetTipoCondicaoExistsBase(tipoCondicao);

                if (tblTipoCondicao != null)
                {
                    return Ok(tblTipoCondicao);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/Tipos/AddTipoCondicao/TipoCondicaoModel
        [HttpPost]
        public async Task<ActionResult<TipoCondicaoModel>> AddTipoCondicao(TipoCondicaoModel tipoCondicao)
        {
            try
            {
                var retorno = await _tipoCondicaoService.AddAsync(tipoCondicao);

                return CreatedAtAction(
                    nameof(GetTipoCondicaoById),
                    new { id = tipoCondicao.Id }, tipoCondicao);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //PUT: api/Tipos/UpdateTipoCondicao/id
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
                return BadRequest();
            }
        }

        // DELETE: api/Tipos/DeleteTipoCondicao/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoCondicao(int id)
        {
            try
            {
                var registroTipoCondicao = await _tipoCondicaoService.DisableAsync(id);

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

        // ATIVAR: api/Tipos/ActivateTipoCondicao/id
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
        // GET: api/Tipos/GetTipoContas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoContaModel>>> GetTipoConta()
        {
            try
            {
                var tipoConta = await _tipoContaService.GetAllAsync();

                if (tipoConta.Any())
                {
                    return Ok(tipoConta);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Tipos/GetTipoContasById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoContaModel>> GetTipoContaById(int id)
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

        // GET: api/Tipos/GetTipoContaExistsBase/tipoConta/descricaoConta
        [HttpGet("{tipoConta}/{descricaoConta}")]
        public async Task<ActionResult<AdministradorModel>> GetTipoContaExistsBase(string tipoConta, string descricaoConta)
        {
            try
            {
                var tblTipoConta = await _tipoContaService.GetTipoContaExistsBase(tipoConta, descricaoConta);

                if (tblTipoConta != null)
                {
                    return Ok(tblTipoConta);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/Tipos/AddTipoConta/TipoContaModel
        [HttpPost]
        public async Task<ActionResult<TipoContaModel>> AddTipoConta(TipoContaModel tipoContaModel)
        {
            try
            {
                var retorno = await _tipoContaService.AddAsync(tipoContaModel);

                return CreatedAtAction(
                    nameof(GetTipoContaById),
                    new { id = tipoContaModel.Id }, tipoContaModel);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //PUT: api/Tipos/UpdateTipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoConta(int id, TipoContaModel tipoConta)
        {
            try
            {
                var retornoTipoConta = await _tipoContaService.GetByIdAsync(id);

                if (retornoTipoConta == null)
                {
                    return NotFound();
                }

                tipoConta.Id = id;
                bool retorno = await _tipoContaService.UpdateAsync(tipoConta);

                if (retorno)
                {
                    return Ok(tipoConta);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Tipos/DeleteTipoConta/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoConta(int id)
        {
            try
            {
                var registroTipoConta = await _tipoContaService.DisableAsync(id);

                if (registroTipoConta)
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

        // ATIVAR: api/Tipos/ActivateTipoConta/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateTipoConta(int id)
        {
            var registroTipoConta = await _tipoContaService.GetByIdAsync(id);

            if (registroTipoConta != null)
            {
                try
                {
                    await _tipoContaService.ActivateAsync(id);
                    return Ok(registroTipoConta);
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

        #region Tipo Contrato
        // GET: api/Tipos/GetTipoContrato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoContratoModel>>> GetTipoContrato()
        {
            try
            {
                var tipoContrato = await _tipoContratoService.GetAllAsync();

                if (tipoContrato.Any())
                {
                    return Ok(tipoContrato);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Tipos/GetTipoContratoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoContratoModel>> GetTipoContratoById(int id)
        {
            try
            {
                var tblTipoContrato = await _tipoContratoService.GetByIdAsync(id);

                if (tblTipoContrato == null)
                {
                    return NotFound();
                }

                return Ok(tblTipoContrato);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/Tipos/AddTipoContrato/TipoContratoModel
        [HttpPost]
        public async Task<ActionResult<TipoContratoModel>> AddTipoContrato(TipoContratoModel tipoContratoModel)
        {
            try
            {
                var retorno = await _tipoContratoService.AddAsync(tipoContratoModel);

                return CreatedAtAction(
                    nameof(GetTipoContratoById),
                    new { id = tipoContratoModel.Id }, tipoContratoModel);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //PUT: api/Tipos/UpdateTipoContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoContrato(int id, TipoContratoModel tipoContrato)
        {
            try
            {
                var retornoTipoContrato = await _tipoContratoService.GetByIdAsync(id);

                if (retornoTipoContrato == null)
                {
                    return NotFound();
                }

                tipoContrato.Id = id;
                bool retorno = await _tipoContratoService.UpdateAsync(tipoContrato);

                if (retorno)
                {
                    return Ok(tipoContrato);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Tipos/DeleteTipoContrato/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoContrato(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_tipo_contrato");

            if (!existeRegistro)
            {
                try
                {
                    var registroTipoContrato = await _tipoContratoService.DisableAsync(id);

                    if (registroTipoContrato)
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
            else
            {
                return NotFound();
            }
        }

        // ATIVAR: api/Tipos/ActivateTipoContrato/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateTipoContrato(int id)
        {
            var registroTipoContrato = await _tipoContratoService.GetByIdAsync(id);

            if (registroTipoContrato != null)
            {
                try
                {
                    await _tipoContratoService.ActivateAsync(id);
                    return Ok(registroTipoContrato);
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

        #region Tipo Estrategia
        // GET: api/Tipos/GetTipoEstrategia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoEstrategiaModel>>> GetTipoEstrategia()
        {
            try
            {
                var tipoEstrategia = await _tipoEstrategiaService.GetAllAsync();

                if (tipoEstrategia.Any())
                {
                    return Ok(tipoEstrategia);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Tipos/GetTipoEstrategiaById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoEstrategiaModel>> GetTipoEstrategiaById(int id)
        {
            try
            {
                var tblTipoEstrategia = await _tipoEstrategiaService.GetByIdAsync(id);

                if (tblTipoEstrategia == null)
                {
                    return NotFound();
                }

                return Ok(tblTipoEstrategia);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/Tipos/AddTipoEstrategia/TipoContaModel
        [HttpPost]
        public async Task<ActionResult<TipoEstrategiaModel>> AddTipoEstrategia(TipoEstrategiaModel tipoEstrategiaModel)
        {
            try
            {
                var retorno = await _tipoEstrategiaService.AddAsync(tipoEstrategiaModel);

                return CreatedAtAction(
                    nameof(GetTipoEstrategiaById),
                    new { id = tipoEstrategiaModel.Id }, tipoEstrategiaModel);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //PUT: api/Tipos/UpdateTipoEstrategia/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoEstrategia(int id, TipoEstrategiaModel tipoEstrategia)
        {
            try
            {
                var retornoTipoEstrategia = await _tipoEstrategiaService.GetByIdAsync(id);

                if (retornoTipoEstrategia == null)
                {
                    return NotFound();
                }

                tipoEstrategia.Id = id;
                bool retorno = await _tipoEstrategiaService.UpdateAsync(tipoEstrategia);

                if (retorno)
                {
                    return Ok(tipoEstrategia);
                }
                return NotFound();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Tipos/DeleteTipoEstrategia/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoEstrategia(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_tipo_estrategia");

            if (!existeRegistro)
            {
                try
                {
                    var registroTipoEstrategia = await _tipoEstrategiaService.DisableAsync(id);

                    if (registroTipoEstrategia)
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
            else
            {
                return NotFound();
            }
        }

        // ATIVAR: api/Tipos/ActivateTipoEstrategia/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateTipoEstrategia(int id)
        {
            var registroTipoEstrategia = await _tipoEstrategiaService.GetByIdAsync(id);

            if (registroTipoEstrategia != null)
            {
                try
                {
                    await _tipoEstrategiaService.ActivateAsync(id);
                    return Ok(registroTipoEstrategia);
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
    }
}
