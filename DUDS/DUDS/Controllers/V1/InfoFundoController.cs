using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DUDS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using DUDS.Service.Interface;

namespace DUDS.Controllers.V1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]/[action]")]
    [ApiController]
    //[Authorize]

    //[ApiExplorerSettings(GroupName ="common")]
    public class InfoFundoController : Controller
    {
        private readonly IConfiguracaoService _configService;
        private readonly IFundoService _fundoService;

        public InfoFundoController(IConfiguracaoService configService, IFundoService fundoService)
        {
            _configService = configService;
            _fundoService = fundoService;
        }

        #region Fundo
        // GET: api/InfoFundo/GetFundo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FundoModel>>> GetFundo()
        {
            try
            {
                var fundos = await _fundoService.GetAllAsync();

                if (fundos.Any())
                {
                    return Ok(fundos);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/InfoFundo/GetFundoById/id
        [HttpGet("{id}")]
        public async Task<ActionResult<FundoModel>> GetFundoById(int id)
        {
            try
            {
                var tblFundo = await _fundoService.GetByIdAsync(id);

                if (tblFundo == null)
                {
                    return NotFound();
                }

                return Ok(tblFundo);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET: api/Fundo/GetFundoExistsBase/cnpj
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<FundoModel>> GetFundoExistsBase(string cnpj)
        {
            try
            {
                var tblFundo = await _fundoService.GetFundoExistsBase(cnpj);

                if (tblFundo != null)
                {
                    return Ok(tblFundo);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //POST: api/InfoFundo/AddFundo/FundoModel
        [HttpPost]
        public async Task<ActionResult<FundoModel>> AddFundo(FundoModel fundoModel)
        {
            try
            {
                var retorno = await _fundoService.AddAsync(fundoModel);

                return CreatedAtAction(
                    nameof(GetFundoById),
                    new { id = fundoModel.Id }, fundoModel);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //PUT: api/InfoFundo/UpdateFundo/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFundo(int id, FundoModel fundoModel)
        {
            try
            {
                FundoModel retornoFundo = await _fundoService.GetByIdAsync(fundoModel.Id);

                if (retornoFundo == null)
                {
                    return NotFound();
                }

                fundoModel.Id = id;
                bool retorno = await _fundoService.UpdateAsync(fundoModel);

                if (retorno)
                {
                    return Ok(fundoModel);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/InfoFundo/DeleteFundo/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFundo(int id)
        {
            bool existeRegistro = await _configService.GetValidacaoExisteIdOutrasTabelas(id, "tbl_fundo");

            if (!existeRegistro)
            {
                try
                {
                    bool retorno = await _fundoService.DeleteAsync(id);

                    if (retorno)
                    {
                        return Ok();
                    }

                    return NotFound();
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

        // ATIVAR: api/InfoFundo/ActivateFundo/id
        [HttpPut("{id}")]
        public async Task<IActionResult> ActivateFundo(int id)
        {
            var registroFundo = await _fundoService.GetByIdAsync(id);

            if (registroFundo != null)
            {
                try
                {
                    await _fundoService.ActivateAsync(id);
                    return Ok(registroFundo);
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
