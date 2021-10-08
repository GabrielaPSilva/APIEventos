using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUDS.Data;
using DUDS.Models;
using DUDS.Service.Interface;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    public class TipoClassificacaoController : ControllerBase
    {
        private readonly ITipoClassificacaoService _tipoClassificacaoService;

        public TipoClassificacaoController(ITipoClassificacaoService tipoClassificacaoService)
        {
            _tipoClassificacaoService = tipoClassificacaoService;
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

        //POST: api/TipoClassificacao/AddTipoClassificacao/TipoContaModel
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
    }
}
